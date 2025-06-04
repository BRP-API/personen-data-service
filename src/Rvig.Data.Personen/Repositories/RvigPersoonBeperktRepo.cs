using Microsoft.Extensions.Options;
using Npgsql;
using Rvig.HaalCentraalApi.Shared.Exceptions;
using Rvig.HaalCentraalApi.Shared.Options;
using Rvig.HaalCentraalApi.Personen.ApiModels.BRP;
using Rvig.Data.Base.Postgres.DatabaseModels;
using Rvig.Data.Base.Postgres.Repositories;
using Rvig.Data.Personen.Repositories.Queries.Helper;
using Rvig.HaalCentraalApi.Shared.Helpers;
using System.Diagnostics;

namespace Rvig.Data.Personen.Repositories;
public interface IRvigPersoonBeperktRepo
{
	Task<List<DbPersoonActueelWrapper>> SearchPersonen(PersonenQuery model, List<string> fields);
}

public class RvigPersoonBeperktRepo : RvigRepoPostgresBase<DbPersoonActueelWrapper>, IRvigPersoonBeperktRepo
{
	public RvigPersoonBeperktRepo(IOptions<DatabaseOptions> databaseOptions, IOptions<HaalcentraalApiOptions> haalcentraalApiOptions, ILoggingHelper loggingHelper)
		: base(databaseOptions, haalcentraalApiOptions, loggingHelper)
	{
	}

	protected override void SetMappings() => CreateMappingsFromWhereMappings();
	protected override void SetWhereMappings() => WhereMappings = RvIGPersonenWhereMappingsHelper.GetPersoonBeperktMappings();

	public async Task<List<DbPersoonActueelWrapper>> SearchPersonen(PersonenQuery model, List<string> fields)
	{
		return model switch
		{
			ZoekMetGeslachtsnaamEnGeboortedatum geslachtsnaamGeboortedatum => await SearchPersonenBase(PersonenQueryHelper.CreateGeslachtsnaamGeboortedatumWhere(geslachtsnaamGeboortedatum), geslachtsnaamGeboortedatum.InclusiefOverledenPersonen ?? false, geslachtsnaamGeboortedatum.maxItems, fields),
			ZoekMetNaamEnGemeenteVanInschrijving naamGemeenteVanInschrijving => await SearchPersonenBase(PersonenQueryHelper.CreateNaamGemeenteVanInschrijvingWhere(naamGemeenteVanInschrijving), naamGemeenteVanInschrijving.InclusiefOverledenPersonen ?? false, naamGemeenteVanInschrijving.maxItems, fields),
			ZoekMetNummeraanduidingIdentificatie nummeraanduidingIdentificatie => await SearchPersonenBase(PersonenQueryHelper.CreateNummeraanduidingIdentificatieWhere(nummeraanduidingIdentificatie), nummeraanduidingIdentificatie.InclusiefOverledenPersonen ?? false, nummeraanduidingIdentificatie.maxItems, fields),
			ZoekMetPostcodeEnHuisnummer postcodeHuisnummer => await SearchPersonenBase(PersonenQueryHelper.CreatePostcodeHuisnummerWhere(postcodeHuisnummer), postcodeHuisnummer.InclusiefOverledenPersonen ?? false, postcodeHuisnummer.maxItems, fields),
			ZoekMetStraatHuisnummerEnGemeenteVanInschrijving staatHuisnummerGemeenteVanInschrijving => await SearchPersonenBase(PersonenQueryHelper.CreateStraatHuisnummerGemeenteVanInschrijvingWhere(staatHuisnummerGemeenteVanInschrijving), staatHuisnummerGemeenteVanInschrijving.InclusiefOverledenPersonen ?? false, staatHuisnummerGemeenteVanInschrijving.maxItems, fields),
			ZoekMetAdresseerbaarObjectIdentificatie adresseerbaarObjectIdentificatie => await SearchPersonenBase(PersonenQueryHelper.CreateAdresseerbaarObjectIdentificatieWhere(adresseerbaarObjectIdentificatie), adresseerbaarObjectIdentificatie.InclusiefOverledenPersonen ?? false, adresseerbaarObjectIdentificatie.maxItems, fields),
			_ => throw new CustomInvalidOperationException($"Onbekend type query: {model}"),
		};
	}

	public async Task<List<DbPersoonActueelWrapper>> SearchPersonenBase((string where, IEnumerable<NpgsqlParameter> parameters) whereStringAndParams, bool inclusiefOverledenPersonen, int maxItems, List<string> fields)
	{
		var command = CreateFilterCommand(PersonenQueryHelper.PersoonBeperktBaseQuery, WhereMappings.Select(o => o.Key).Aggregate((i, j) => i + "," + j), whereStringAndParams);

		var dbPersoonWrappers = (await GetFilterResultAsync(command)).ToList();

		if (!dbPersoonWrappers.Any())
		{
			return new List<DbPersoonActueelWrapper>();
		}
	
		if ((!inclusiefOverledenPersonen && dbPersoonWrappers.Count(x => x.Inschrijving.bijhouding_opschort_reden == null || !x.Inschrijving.bijhouding_opschort_reden.Equals("o", StringComparison.CurrentCultureIgnoreCase)) > maxItems)
			|| (inclusiefOverledenPersonen && dbPersoonWrappers.Count() > maxItems))
		{
			throw new TooManyResultsException($"Meer dan maximum van {maxItems} zoekresultaten gevonden. Verfijn de zoekopdracht.");
		}

		var pl_ids = dbPersoonWrappers.ConvertAll(dbPersoon => dbPersoon.Persoon.pl_id);

		if (pl_ids.Count > 0)
		{
			var additionalMappings = RvIGPersonenWhereMappingsHelper.GetAdditionalPlRelationMappings();
			var plRelationMappings = RvIGPersonenWhereMappingsHelper.GetPersoonBaseMappings().Concat(additionalMappings).Select(o => o.Key).Aggregate((i, j) => i + "," + j);
			var persoonslijstenWhere = GetPlIdsWhere(pl_ids, "pers");
			var persoonTypeValues = new List<string>();

			if (fields.Any(field => field.Contains("gezag") && !field.StartsWith("indicatieGezagMinderjarige")))
			{
				persoonTypeValues.Add("'1'");
				persoonTypeValues.Add("'2'");
			}
			if (fields.Any(field => field.Contains("gezag") && !field.StartsWith("indicatieGezagMinderjarige")))
			{
				persoonTypeValues.Add("'K'");
			}
			if (fields.Any(field => field.Contains("gezag") && !field.StartsWith("indicatieGezagMinderjarige")))
			{
				persoonTypeValues.Add("'R'");
			}

			var persoonslijsten = new List<lo3_pl_persoon>();
			if (persoonTypeValues.Count > 0)
			{
				persoonslijstenWhere.where += $" and persoon_type in ({string.Join(", ", persoonTypeValues)})";

				persoonslijsten = (await GetFilterResultAsync(CreateFilterCommand(PersonenQueryHelper.PersoonslijstByPlIds, plRelationMappings, persoonslijstenWhere), CreateMappingsFromWhereMappings(additionalMappings)))
					.Select(persoonWrapper => persoonWrapper.Persoon).ToList();
			}

			dbPersoonWrappers.ForEach(dbPersoon =>
			{
				if (persoonslijsten.Count > 0)
				{
					var persoonlijstenByDbPersoonPlId = persoonslijsten.Where(persoonslijst => persoonslijst.pl_id.Equals(dbPersoon.Persoon.pl_id)).ToList();

					dbPersoon.Partners = persoonlijstenByDbPersoonPlId.Where(x => x.persoon_type == "R").ToList();

					dbPersoon.Ouder1 = persoonlijstenByDbPersoonPlId.SingleOrDefault(x => x.volg_nr == 0 && x.persoon_type == "1") ?? new lo3_pl_persoon();
					dbPersoon.Ouder2 = persoonlijstenByDbPersoonPlId.SingleOrDefault(x => x.volg_nr == 0 && x.persoon_type == "2") ?? new lo3_pl_persoon();
					dbPersoon.Kinderen = persoonlijstenByDbPersoonPlId.Where(x => x.volg_nr == 0 && x.persoon_type == "K").ToList();
				}
			});
		}

		return dbPersoonWrappers.ToList();
	}
}