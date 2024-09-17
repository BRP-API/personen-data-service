using Microsoft.Extensions.Options;
using Npgsql;
using Rvig.Data.Base.Postgres.DatabaseModels;
using Rvig.Data.Base.Postgres.Repositories;
using Rvig.Data.Personen.Repositories.Queries.Helper;
using Rvig.HaalCentraalApi.Personen.ApiModels.BRP;
using Rvig.HaalCentraalApi.Shared.ApiModels.Universal;
using Rvig.HaalCentraalApi.Shared.Exceptions;
using Rvig.HaalCentraalApi.Shared.Helpers;
using Rvig.HaalCentraalApi.Shared.Options;
using static Npgsql.PostgresTypes.PostgresCompositeType;

namespace Rvig.Data.Personen.Repositories;

public interface IRvigPersoonRepo
{
	Task<IEnumerable<DbPersoonActueelWrapper?>> GetPersoonByBsns(IEnumerable<string> bsns, string? gemeenteVanInschrijving, List<string> fields);
}

public class RvigPersoonRepo : RvigRepoPostgresBase<DbPersoonActueelWrapper>, IRvigPersoonRepo
{
	public RvigPersoonRepo(IOptions<DatabaseOptions> databaseOptions, IOptions<HaalcentraalApiOptions> haalcentraalApiOptions, ILoggingHelper loggingHelper)
		: base(databaseOptions, haalcentraalApiOptions, loggingHelper)
	{
	}

	protected override void SetMappings() => CreateMappingsFromWhereMappings();
	protected override void SetWhereMappings() => WhereMappings = RvIGPersonenWhereMappingsHelper.GetPersoonMappings();

	public async Task<IEnumerable<DbPersoonActueelWrapper?>> GetPersoonByBsns(IEnumerable<string> bsns, string? gemeenteVanInschrijving, List<string> fields)
	{
		(string where, IEnumerable<NpgsqlParameter> parameters) = PersonenQueryHelper.CreateBurgerservicenummerGemeenteVanInschrijvingWhere(bsns, gemeenteVanInschrijving);
		var query = string.Format(PersonenQueryHelper.PersoonBaseQuery, WhereMappings.Select(o => o.Key).Aggregate((i, j) => i + "," + j), where);
		var command = new NpgsqlCommand(query);
		command.Parameters.AddRange(parameters.ToArray());

		var dbPersonen = (await GetFilterResultAsync(command)).ToList();
		if (dbPersonen.GroupBy(dbPersoon => dbPersoon.Persoon.burger_service_nr).Any(group => group.Count() > 1))
		{
			var invalidParam = new InvalidParams
			{
				Code = "unique",
				Reason = "De opgegeven persoonidentificatie is niet uniek.",
				Name = nameof(GbaPersoon.Burgerservicenummer).ToLower()
			};

			throw new InvalidParamsException(new List<InvalidParams> { invalidParam });
		}

		if (!dbPersonen.Any(dbPersoon => dbPersoon?.Persoon?.pl_id != null))
		{
			return Enumerable.Empty<DbPersoonActueelWrapper?>();
		}

		var pl_ids = dbPersonen.ConvertAll(dbPersoon => dbPersoon.Persoon.pl_id);

		if (pl_ids.Count > 0)
		{
			var additionalMappings = RvIGPersonenWhereMappingsHelper.GetAdditionalPlRelationMappings();
			var plRelationMappings = RvIGPersonenWhereMappingsHelper.GetPersoonBaseMappings().Concat(additionalMappings).Select(o => o.Key).Aggregate((i, j) => i + "," + j);
			var persoonslijstenWhere = GetPlIdsWhere(pl_ids, "pers");
			var persoonTypeValues = new List<string>();

			if (fields.Any(field => field.Contains("ouders") || (field.Contains("gezag") && !field.StartsWith("indicatieGezagMinderjarige"))))
			{
				persoonTypeValues.Add("'1'");
				persoonTypeValues.Add("'2'");
			}
			if (fields.Any(field => field.Contains("kinderen") || (field.Contains("gezag") && !field.StartsWith("indicatieGezagMinderjarige"))))
			{
				persoonTypeValues.Add("'K'");
			}
			if (fields.Contains("adressering")
				|| fields.Contains("adresseringBinnenland")
				|| fields.Any(field => field.Contains("partners"))
				|| fields.Any(field => field.Contains("adressering.aanhef"))
				|| fields.Any(field => field.Contains("adressering.aanschrijfwijze"))
				|| fields.Any(field => field.Contains("adressering.gebruikInLopendeTekst"))
				|| fields.Any(field => field.Contains("adresseringBinnenland.aanhef"))
				|| fields.Any(field => field.Contains("adresseringBinnenland.aanschrijfwijze"))
				|| fields.Any(field => field.Contains("adresseringBinnenland.gebruikInLopendeTekst"))
				|| fields.Any(field => field.Contains("gezag") && !field.StartsWith("indicatieGezagMinderjarige")))
			{
				persoonTypeValues.Add("'R'");
			}

			var persoonslijsten = new List<lo3_pl_persoon>();
			if (persoonTypeValues.Count > 0)
			{
				persoonslijstenWhere.where += $" and pers.persoon_type in ({string.Join(", ", persoonTypeValues)})";

				persoonslijsten = (await GetFilterResultAsync(CreateFilterCommand(PersonenQueryHelper.PersoonslijstByPlIds, plRelationMappings, persoonslijstenWhere), CreateMappingsFromWhereMappings(additionalMappings)))
					.Select(persoonWrapper => persoonWrapper.Persoon).ToList();
			}

			var nationaliteiten =
				fields.Any(field => field.Contains("nationaliteiten"))
				? await GetPersoonDataByPlIds<lo3_pl_nationaliteit>(pl_ids, PersonenQueryHelper.PersoonNationaliteitenByPlIds, "natnltt")
				: new List<lo3_pl_nationaliteit>();

			dbPersonen.ForEach(dbPersoon =>
			{
				dbPersoon.Nationaliteiten = nationaliteiten.Where(nationaliteit => nationaliteit.pl_id.Equals(dbPersoon.Persoon.pl_id)).ToList();
				// We have no reisdocumenten in GbaPersoon!
				//dbPersoonWrapper.Reisdocumenten = await GetPersoonDataByPlIds<lo3_pl_reis_doc>(connection, pl_ids, QueryBaseHelper.PersoonReisdocumentenByPlIds, "rdoc");

				if (persoonslijsten.Count > 0)
				{
					var persoonlijstenByDbPersoonPlId = persoonslijsten.Where(persoonslijst => persoonslijst.pl_id.Equals(dbPersoon.Persoon.pl_id)).ToList();

					dbPersoon.Partners = persoonlijstenByDbPersoonPlId.Where(x => x.persoon_type == "R").ToList();

					dbPersoon.Ouder1 = persoonlijstenByDbPersoonPlId.SingleOrDefault(x => x.volg_nr == 0 && x.persoon_type == "1") ?? new lo3_pl_persoon();
					dbPersoon.Ouder2 = persoonlijstenByDbPersoonPlId.SingleOrDefault(x => x.volg_nr == 0 && x.persoon_type == "2") ?? new lo3_pl_persoon();
					dbPersoon.Kinderen = persoonlijstenByDbPersoonPlId.Where(x => x.volg_nr == 0 && x.persoon_type == "K" && string.IsNullOrWhiteSpace(x.registratie_betrekking)).ToList();
				}
			});
		}

		return dbPersonen;
	}
}