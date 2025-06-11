using Rvig.HaalCentraalApi.Personen.ApiModels.BRP;
using Rvig.HaalCentraalApi.Shared.Fields;

namespace Rvig.HaalCentraalApi.Personen.Helpers;

/// <summary>
/// This class is used as a container for all methods that are 'hacks' or specific exceptions to generic code.
/// These methods are a result of the GBA API having to conform to demands that should be solved elsewhere but will not be or there isn't enough time.
/// These solutions should be performed by the RvIG and other organisations.
/// </summary>
public class GbaPersonenApiHelperBase
{
	protected GbaPersonenApiHelperBase()
	{
	}

	/// <summary>
	/// This method will add RNI even though the fields do not contain a request of RNI.
	/// </summary>
	/// <param name="fields"></param>
	/// <param name="rni"></param>
	public static List<RniDeelnemer>? ApplyRniLogic(List<string> fields, List<RniDeelnemer>? rni, FieldsSettingsModel fieldsMapping)
	{
		if (rni?.Any() == true)
		{
			var fieldsToCompare = string.Join("&", fields.Select(field => fieldsMapping.ShortHandMappings.ContainsKey(field)
																? fieldsMapping.ShortHandMappings[field]
																: field)).Split('&');
			var persoonRniFields = new List<string> { "aNummer", "burgerservicenummer", "geheimhoudingPersoonsgegevens", "geslacht", "geslacht.", "naam", "naam.", "geboorte", "geboorte.",
						"adressering", "adressering.aanhef", "adressering.aanschrijfwijze", "adressering.aanschrijfwijze.", "adressering.gebruikInLopendeTekst",
							"adressering.inOnderzoek.aanhef", "adressering.inOnderzoek.aanschrijfwijze", "adressering.inOnderzoek.gebruikInLopendeTekst",
							"adressering.inOnderzoek.datumIngangOnderzoekPersoon", "leeftijd"};
			// options Persoon Nationaliteit Overlijden Verblijfplaats
			if (rni.Any(rni => rni?.Categorie?.Equals("Persoon") == true)
			&& persoonRniFields
					.All(propName => !fieldsToCompare.Any(field => field.Contains(propName) && !field.Contains("naamOpenbareRuimte") && !propName.Equals("naam")
													|| field.Contains(propName) && persoonRniFields.Contains(field))))
			{
				rni.Remove(rni.Single(rni => rni?.Categorie?.Equals("Persoon") == true));
			}
			if (rni.Any(rni => rni?.Categorie?.Equals("Verblijfplaats") == true)
			&& new List<string> { "gemeenteVanInschrijving", "datumInschrijvingInGemeente", "verblijfplaats", "verblijfplaats.", "immigratie", "immigratie." }
					.All(propName => !fieldsToCompare.Any(field => field.Contains(propName))))
			{
				rni.Remove(rni.Single(rni => rni?.Categorie?.Equals("Verblijfplaats") == true));
			}
			if (rni.Any(rni => rni?.Categorie?.Equals("Nationaliteit") == true)
			&& new List<string> { "nationaliteiten", "nationaliteiten." }
					.All(propName => !fieldsToCompare.Any(field => field.Contains(propName))))
			{
				rni.RemoveAll(rni => rni?.Categorie?.Equals("Nationaliteit") == true);
			}
			if (rni.Any(rni => rni?.Categorie?.Equals("Overlijden") == true)
			&& new List<string> { "overlijden", "overlijden." }
					.All(propName => !fieldsToCompare.Any(field => field.Contains(propName))))
			{
				rni.Remove(rni.Single(rni => rni?.Categorie?.Equals("Overlijden") == true));
			}
			//if (rni.Any(rni => rni?.Categorie?.Equals("Inschrijving") == true)
			//&& new List<string> { "rni", "" }
			//		.All(propName => !fields.Any(field => field.Contains(propName))))
			//{
			//	rni.Remove(rni.Single(rni => rni?.Categorie?.Equals("Inschrijving") == true));
			//}
		}
		if (rni?.Any() == true && fields.Any(field => field.Contains("rni")))
		{
			fields.Remove("rni");
			fields.RemoveAll(field => field.Contains("rni."));
		}
		if (rni?.Any() == false)
		{
			rni = null;
		}

		return rni;
	}

	/// <summary>
	/// This method will add overlijden even though the fields do not contain a request of overlijden.
	/// </summary>
	/// <param name="model"></param>
	/// <param name="overlijden"></param>
	public static void ApplyOverlijdenLogic(List<string> fields, GbaOverlijden? overlijden)
	{
		if (overlijden != null && fields.All(field => !field.Contains("overlijden.datum") && !field.Equals("overlijden")))
		{
			fields.Add("overlijden.datum");
		}
	}
}
