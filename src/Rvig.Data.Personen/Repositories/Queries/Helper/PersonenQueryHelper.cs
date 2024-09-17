using Npgsql;
using Rvig.Data.Base.Postgres.Repositories.Queries;
using Rvig.HaalCentraalApi.Personen.RequestModels.BRP;
using System.Text.RegularExpressions;

namespace Rvig.Data.Personen.Repositories.Queries.Helper
{
	public class PersonenQueryHelper : QueryBaseHelper
	{
		public static (string where, IEnumerable<NpgsqlParameter> parameters) CreateBurgerservicenummerGemeenteVanInschrijvingWhere(string bsn, string? gemeenteVanInschrijving)
		{
			(string where, List<NpgsqlParameter> parameters) whereClause = ("", new List<NpgsqlParameter>());
			(string where, NpgsqlParameter pgsqlParam) = CreateBurgerservicenummerWhere(bsn);
			whereClause.where = where;
			whereClause.parameters.Add(pgsqlParam);
			if (string.IsNullOrWhiteSpace(gemeenteVanInschrijving))
			{
				return whereClause;
			}

			var gemCodeWhereParam = CreateGemeenteVanInschrijvingPart(int.Parse(gemeenteVanInschrijving));
			if (gemCodeWhereParam != default)
			{
				whereClause.where += $" and {gemCodeWhereParam.where}";
				whereClause.parameters.Add(gemCodeWhereParam.pgsqlParam);
			}

			return whereClause;
		}

		private static (string where, IEnumerable<NpgsqlParameter> parameters) CreatePersoonSearchBase(List<string?> whereParts, List<NpgsqlParameter> parameters)
		{
			whereParts = whereParts.Where(x => x != null).ToList();
			parameters = parameters.Where(x => x != null).ToList();

			if (whereParts.Count == 0)
			{
				return default;
			}
			var allOptionalWherePartsJoined = string.Join(" and ", whereParts);

			return ($"where {allOptionalWherePartsJoined} and persoon_type = 'P' and pers.stapel_nr = 0 and pers.volg_nr = 0 and ((pl.bijhouding_opschort_reden is not null and pl.bijhouding_opschort_reden != 'F' and pl.bijhouding_opschort_reden != 'W') or pl.bijhouding_opschort_reden is null)", parameters);
		}

		public static (string where, IEnumerable<NpgsqlParameter> parameters) CreateGeslachtsnaamGeboortedatumWhere(ZoekMetGeslachtsnaamEnGeboortedatum model)
		{
			var parameters = new List<NpgsqlParameter>();
			var whereParts = new List<string?>();

			(string where, NpgsqlParameter pgsqlParam) = CreateGeslachtsnaamPart(model.geslachtsnaam);
			whereParts.Add(where);
			parameters.Add(pgsqlParam);

			(string where, NpgsqlParameter pgsqlParam) voornamenPart = CreateVoornamenPart(model.voornamen);
			whereParts.Add(voornamenPart.where);
			parameters.Add(voornamenPart.pgsqlParam);

			(string where, NpgsqlParameter pgsqlParam) voorvoegselPart = CreateVoorvoegselPart(model.voorvoegsel);
			whereParts.Add(voorvoegselPart.where);
			parameters.Add(voorvoegselPart.pgsqlParam);

			(string where, NpgsqlParameter pgsqlParam) geslachtsaanduidingPart = CreateGeslachtsaanduidingPart(model.geslacht);
			whereParts.Add(geslachtsaanduidingPart.where);
			parameters.Add(geslachtsaanduidingPart.pgsqlParam);

			// Null is not allowed for geboortedatum.
			if (!string.IsNullOrWhiteSpace(model.geboortedatum))
			{
				(string where, NpgsqlParameter pgsqlParam) geboorteDatumPart = CreateGeboorteDatumPart(int.Parse(model.geboortedatum.Replace("-", "")));
				whereParts.Add(geboorteDatumPart.where);
				parameters.Add(geboorteDatumPart.pgsqlParam);
			}

			(string where, NpgsqlParameter pgsqlParam) gemeenteVanInschrijvingPart = CreateGemeenteVanInschrijvingPart(!string.IsNullOrWhiteSpace(model.gemeenteVanInschrijving) ? int.Parse(model.gemeenteVanInschrijving) : null);
			whereParts.Add(gemeenteVanInschrijvingPart.where);
			parameters.Add(gemeenteVanInschrijvingPart.pgsqlParam);

			return CreatePersoonSearchBase(whereParts, parameters);
		}

		public static (string where, IEnumerable<NpgsqlParameter> parameters) CreateNaamGemeenteVanInschrijvingWhere(ZoekMetNaamEnGemeenteVanInschrijving model)
		{
			var parameters = new List<NpgsqlParameter>();
			var whereParts = new List<string?>();

			(string where, NpgsqlParameter pgsqlParam) = CreateGeslachtsnaamPart(model.geslachtsnaam);
			whereParts.Add(where);
			parameters.Add(pgsqlParam);

			(string where, NpgsqlParameter pgsqlParam) voornamenPart = CreateVoornamenPart(model.voornamen);
			whereParts.Add(voornamenPart.where);
			parameters.Add(voornamenPart.pgsqlParam);

			(string where, NpgsqlParameter pgsqlParam) voorvoegselPart = CreateVoorvoegselPart(model.voorvoegsel);
			whereParts.Add(voorvoegselPart.where);
			parameters.Add(voorvoegselPart.pgsqlParam);

			(string where, NpgsqlParameter pgsqlParam) geslachtsaanduidingPart = CreateGeslachtsaanduidingPart(model.geslacht);
			whereParts.Add(geslachtsaanduidingPart.where);
			parameters.Add(geslachtsaanduidingPart.pgsqlParam);

			(string where, NpgsqlParameter pgsqlParam) gemeenteVanInschrijvingPart = CreateGemeenteVanInschrijvingPart(!string.IsNullOrWhiteSpace(model.gemeenteVanInschrijving) ? int.Parse(model.gemeenteVanInschrijving) : null);
			whereParts.Add(gemeenteVanInschrijvingPart.where);
			parameters.Add(gemeenteVanInschrijvingPart.pgsqlParam);

			whereParts = whereParts.Where(x => x != null).ToList();
			parameters = parameters.Where(x => x != null).ToList();

			return CreatePersoonSearchBase(whereParts, parameters);
		}

		public static (string where, IEnumerable<NpgsqlParameter> parameters) CreateNummeraanduidingIdentificatieWhere(ZoekMetNummeraanduidingIdentificatie model)
		{
			var parameters = new List<NpgsqlParameter>();
			var whereParts = new List<string?>();

			(string where, NpgsqlParameter pgsqlParam) = CreateGemeenteVanInschrijvingPart(!string.IsNullOrWhiteSpace(model.gemeenteVanInschrijving) ? int.Parse(model.gemeenteVanInschrijving) : null);
			whereParts.Add(where);
			parameters.Add(pgsqlParam);

			(string where, NpgsqlParameter pgsqlParam) identificatiecodeNummeraanduidingPart = CreateIdentificatiecodeNummeraanduidingPart(model.nummeraanduidingIdentificatie);
			whereParts.Add(identificatiecodeNummeraanduidingPart.where);
			parameters.Add(identificatiecodeNummeraanduidingPart.pgsqlParam);

			whereParts = whereParts.Where(x => x != null).ToList();
			parameters = parameters.Where(x => x != null).ToList();

			return CreatePersoonSearchBase(whereParts, parameters);
		}

		public static (string where, IEnumerable<NpgsqlParameter> parameters) CreatePostcodeHuisnummerWhere(ZoekMetPostcodeEnHuisnummer model)
		{
			var parameters = new List<NpgsqlParameter>();
			var whereParts = new List<string?>();

			(string where, NpgsqlParameter pgsqlParam) = CreateGemeenteVanInschrijvingPart(!string.IsNullOrWhiteSpace(model.gemeenteVanInschrijving) ? int.Parse(model.gemeenteVanInschrijving) : null);
			whereParts.Add(where);
			parameters.Add(pgsqlParam);

			(string where, NpgsqlParameter pgsqlParam) postcodePart = CreatePostcodePart(model.postcode);
			whereParts.Add(postcodePart.where);
			parameters.Add(postcodePart.pgsqlParam);

			(string where, NpgsqlParameter pgsqlParam) huisnummerPart = CreateHuisnummerPart(model.huisnummer);
			whereParts.Add(huisnummerPart.where);
			parameters.Add(huisnummerPart.pgsqlParam);

			(string where, NpgsqlParameter pgsqlParam) huisletterPart = CreateHuisletterPart(model.huisletter);
			whereParts.Add(huisletterPart.where);
			parameters.Add(huisletterPart.pgsqlParam);

			(string where, NpgsqlParameter pgsqlParam) huisnummerToevoegingPart = CreateHuisnummerToevoegingPart(model.huisnummertoevoeging);
			whereParts.Add(huisnummerToevoegingPart.where);
			parameters.Add(huisnummerToevoegingPart.pgsqlParam);

			if (!string.IsNullOrWhiteSpace(model.geboortedatum))
			{
				(string where, NpgsqlParameter pgsqlParam) geboorteDatumPart = CreateGeboorteDatumPart(int.Parse(model.geboortedatum.Replace("-", "")));
				whereParts.Add(geboorteDatumPart.where);
				parameters.Add(geboorteDatumPart.pgsqlParam);
			}

			whereParts = whereParts.Where(x => x != null).ToList();
			parameters = parameters.Where(x => x != null).ToList();

			return CreatePersoonSearchBase(whereParts, parameters);
		}

		public static (string where, IEnumerable<NpgsqlParameter> parameters) CreateStraatHuisnummerGemeenteVanInschrijvingWhere(ZoekMetStraatHuisnummerEnGemeenteVanInschrijving model)
		{
			var parameters = new List<NpgsqlParameter>();
			var whereParts = new List<string?>();

			(string where, NpgsqlParameter pgsqlParam) = CreateGemeenteVanInschrijvingPart(!string.IsNullOrWhiteSpace(model.gemeenteVanInschrijving) ? int.Parse(model.gemeenteVanInschrijving) : null);
			whereParts.Add(where);
			parameters.Add(pgsqlParam);

			(string where, NpgsqlParameter pgsqlParam) straatnaamPart = CreateStraatnaamPart(model.straat);
			whereParts.Add(straatnaamPart.where);
			parameters.Add(straatnaamPart.pgsqlParam);

			(string where, NpgsqlParameter pgsqlParam) huisnummerPart = CreateHuisnummerPart(model.huisnummer);
			whereParts.Add(huisnummerPart.where);
			parameters.Add(huisnummerPart.pgsqlParam);

			(string where, NpgsqlParameter pgsqlParam) huisletterPart = CreateHuisletterPart(model.huisletter);
			whereParts.Add(huisletterPart.where);
			parameters.Add(huisletterPart.pgsqlParam);

			(string where, NpgsqlParameter pgsqlParam) huisnummerToevoegingPart = CreateHuisnummerToevoegingPart(model.huisnummertoevoeging);
			whereParts.Add(huisnummerToevoegingPart.where);
			parameters.Add(huisnummerToevoegingPart.pgsqlParam);

			whereParts = whereParts.Where(x => x != null).ToList();
			parameters = parameters.Where(x => x != null).ToList();

			return CreatePersoonSearchBase(whereParts, parameters);
		}

		public static (string where, IEnumerable<NpgsqlParameter> parameters) CreateAdresseerbaarObjectIdentificatieWhere(ZoekMetAdresseerbaarObjectIdentificatie model)
		{
			var parameters = new List<NpgsqlParameter>();
			var whereParts = new List<string?>();

			(string where, NpgsqlParameter pgsqlParam) = CreateGemeenteVanInschrijvingPart(!string.IsNullOrWhiteSpace(model.gemeenteVanInschrijving) ? int.Parse(model.gemeenteVanInschrijving) : null);
			whereParts.Add(where);
			parameters.Add(pgsqlParam);

			(string where, NpgsqlParameter pgsqlParam) identificatiecodeNummeraanduidingPart = CreateIdentificatiecodeAdresseerbaarObjectPart(model.adresseerbaarObjectIdentificatie);
			whereParts.Add(identificatiecodeNummeraanduidingPart.where);
			parameters.Add(identificatiecodeNummeraanduidingPart.pgsqlParam);

			whereParts = whereParts.Where(x => x != null).ToList();
			parameters = parameters.Where(x => x != null).ToList();

			return CreatePersoonSearchBase(whereParts, parameters);
		}

		private static (string where, NpgsqlParameter pgsqlParam) CreateGeslachtsnaamPart(string? geslachtsnaam)
		{
			if (string.IsNullOrEmpty(geslachtsnaam))
			{
				return default;
			}

			var parameter = new NpgsqlParameter
			{
				ParameterName = "GESLACHTSNAAM"
			};

			string? where;
			if (ContainsWildcards(geslachtsnaam))
			{
				parameter.Value = CreatePsqlWildcardInParamValue(geslachtsnaam);
				where = HasDiacritics(geslachtsnaam)
					? "lower(pers.diak_geslachts_naam) like lower(@GESLACHTSNAAM)"
					: "lower(pers.geslachts_naam) like lower(@GESLACHTSNAAM)";
			}
			else
			{
				parameter.Value = geslachtsnaam;
				where = HasDiacritics(geslachtsnaam)
					? "lower(pers.diak_geslachts_naam) = lower(@GESLACHTSNAAM)"
					: "lower(pers.geslachts_naam) = lower(@GESLACHTSNAAM)";
			}

			return (where, parameter);
		}

		private static (string where, NpgsqlParameter pgsqlParam) CreateVoornamenPart(string? voornamen)
		{
			if (string.IsNullOrEmpty(voornamen))
			{
				return default;
			}

			var parameter = new NpgsqlParameter
			{
				ParameterName = "VOORNAMEN"
			};

			string? where;
			if (ContainsWildcards(voornamen))
			{
				parameter.Value = CreatePsqlWildcardInParamValue(voornamen);
				where = HasDiacritics(voornamen)
						? "lower(pers.diak_voor_naam) like lower(@VOORNAMEN)"
						: "lower(pers.voor_naam) like lower(@VOORNAMEN)";
			}
			else
			{
				parameter.Value = voornamen;
				where = HasDiacritics(voornamen)
					? "lower(pers.diak_voor_naam) = lower(@VOORNAMEN)"
					: "lower(pers.voor_naam) = lower(@VOORNAMEN)";
			}

			return (where, parameter);
		}

		private static (string where, NpgsqlParameter pgsqlParam) CreateVoorvoegselPart(string? voorvoegsel)
		{
			if (string.IsNullOrEmpty(voorvoegsel))
			{
				return default;
			}
			var parameter = new NpgsqlParameter("VOORVOEGSEL", voorvoegsel);

			return ("lower(pers.geslachts_naam_voorvoegsel) = lower(@VOORVOEGSEL)", parameter);
		}

		private static (string where, NpgsqlParameter pgsqlParam) CreateGeslachtsaanduidingPart(string? geslachtsaanduiding)
		{
			if (string.IsNullOrEmpty(geslachtsaanduiding))
			{
				return default;
			}
			var parameter = new NpgsqlParameter("GESLACHTSAANDUIDING", geslachtsaanduiding);

			return ("lower(pers.geslachts_aand) = lower(@GESLACHTSAANDUIDING)", parameter);
		}

		private static (string where, NpgsqlParameter pgsqlParam) CreateGeboorteDatumPart(int? geboorteDatum)
		{
			if (!geboorteDatum.HasValue)
			{
				return default;
			}
			var parameter = new NpgsqlParameter("GEBOORTEDATUM", geboorteDatum);

			return ("pers.geboorte_datum = @GEBOORTEDATUM", parameter);
		}

		private static (string where, NpgsqlParameter pgsqlParam) CreateIdentificatiecodeNummeraanduidingPart(string? identificatiecodeNummeraanduiding)
		{
			if (string.IsNullOrEmpty(identificatiecodeNummeraanduiding))
			{
				return default;
			}
			var parameter = new NpgsqlParameter("IDENTIFICATIECODENUMMERAANDUIDING", identificatiecodeNummeraanduiding);

			return ("adres.nummer_aand_ident_code = @IDENTIFICATIECODENUMMERAANDUIDING", parameter);
		}

		private static (string where, NpgsqlParameter pgsqlParam) CreateIdentificatiecodeAdresseerbaarObjectPart(string? identificatiecodeAdresseerbaarObject)
		{
			if (string.IsNullOrEmpty(identificatiecodeAdresseerbaarObject))
			{
				return default;
			}
			var parameter = new NpgsqlParameter("IDENTIFICATIECODEADRESSEERBAAROBJECT", identificatiecodeAdresseerbaarObject);

			return ("adres.verblijf_plaats_ident_code = @IDENTIFICATIECODEADRESSEERBAAROBJECT", parameter);
		}

		private static (string where, NpgsqlParameter pgsqlParam) CreateStraatnaamPart(string? straatnaam)
		{
			if (string.IsNullOrEmpty(straatnaam))
			{
				return default;
			}

			var parameter = new NpgsqlParameter
			{
				ParameterName = "STRAATNAAM"
			};

			string? where;
			if (ContainsWildcards(straatnaam))
			{
				parameter.Value = CreatePsqlWildcardInParamValue(straatnaam);
				where = HasDiacritics(straatnaam)
					? "(lower(adres.diak_straat_naam) like lower(@STRAATNAAM) or lower(adres.diak_open_ruimte_naam) like lower(@STRAATNAAM))"
					: "(lower(adres.straat_naam) like lower(@STRAATNAAM) or lower(adres.open_ruimte_naam) like lower(@STRAATNAAM))";
			}
			else
			{
				parameter.Value = straatnaam;
				where = HasDiacritics(straatnaam)
					? "(lower(adres.diak_straat_naam) = lower(@STRAATNAAM) or lower(adres.diak_open_ruimte_naam) = lower(@STRAATNAAM))"
					: "(lower(adres.straat_naam) = lower(@STRAATNAAM) or lower(adres.open_ruimte_naam) = lower(@STRAATNAAM))";
			}

			return (where, parameter);
		}

		private static (string where, NpgsqlParameter pgsqlParam) CreatePostcodePart(string? postcode)
		{
			if (string.IsNullOrEmpty(postcode))
			{
				return default;
			}
			else if (postcode.Contains(" "))
			{
				postcode = postcode.Replace(" ", "");
			}
			var parameter = new NpgsqlParameter("POSTCODE", postcode);

			return ("lower(adres.postcode) = lower(@POSTCODE)", parameter);
		}

		private static (string where, NpgsqlParameter pgsqlParam) CreateHuisnummerPart(int? huisnummer)
		{
			if (!huisnummer.HasValue)
			{
				return default;
			}
			var parameter = new NpgsqlParameter("HUISNUMMER", huisnummer.Value);

			return ("adres.huis_nr = @HUISNUMMER", parameter);
		}

		private static (string where, NpgsqlParameter pgsqlParam) CreateHuisletterPart(string? huisletter)
		{
			if (string.IsNullOrEmpty(huisletter))
			{
				return default;
			}
			var parameter = new NpgsqlParameter("HUISLETTER", huisletter);

			return ("lower(adres.huis_letter) = lower(@HUISLETTER)", parameter);
		}

		private static (string where, NpgsqlParameter pgsqlParam) CreateHuisnummerToevoegingPart(string? huisnummerToevoeging)
		{
			if (string.IsNullOrEmpty(huisnummerToevoeging))
			{
				return default;
			}
			var parameter = new NpgsqlParameter("HUISNUMMERTOEVOEGING", huisnummerToevoeging);

			return ("lower(adres.huis_nr_toevoeging) = lower(@HUISNUMMERTOEVOEGING)", parameter);
		}

		private static bool ContainsWildcards(string paramValue)
		{
			return paramValue.Contains('*');
		}

		/// <summary>
		/// Haal Centraal views * als a wildcard but in SQL a wildcard is the setup of a LIKE in combination with %.
		/// This function replaces the * with %.
		/// </summary>
		/// <param name="paramValue"></param>
		/// <returns></returns>
		private static string CreatePsqlWildcardInParamValue(string paramValue)
		{
			return paramValue.Replace("*", "%");
		}

		/// <summary>
		/// Does value contain a diacritic.
		/// ZoekMetNaamEnGemeenteVanInschrijvingValidator.cs, ZoekMetGeslachtsnaamEnGeboortedatumValidator.cs and ZoekNaamValidatorBase.cs
		/// all contain diacritic regex patterns. If those or this one change then make sure to change it in the other locations as well.
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		private static bool HasDiacritics(string text)
		{
			return Regex.IsMatch(text, "[À-ž]");
		}
	}
}
