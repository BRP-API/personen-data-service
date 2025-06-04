using Rvig.HaalCentraalApi.Personen.ApiModels.BRP.Deprecated;
using Rvig.HaalCentraalApi.Personen.Fields;

namespace Rvig.HaalCentraalApi.Personen.Helpers;

/// <summary>
/// This class is used as a container for all methods that are 'hacks' or specific exceptions to generic code.
/// These methods are a result of the GBA API having to conform to demands that should be solved elsewhere but will not be or there isn't enough time.
/// These solutions should be performed by the RvIG and other organisations.
/// </summary>
public class GbaPersonenBeperktApiHelper : GbaPersonenApiHelperBase
{
	private readonly PersonenBeperktFieldsSettings _fieldsSettings;

	public GbaPersonenBeperktApiHelper(PersonenBeperktFieldsSettings fieldsSettings)
	{
		_fieldsSettings = fieldsSettings;
	}

	/// <summary>
	/// Hack because business logic require the GBA API to support some fields when given specific cases. This is different from standard logic and therefore a hack.
	/// </summary>
	public void AddVerblijfplaatsBeperktInOnderzoek(List<string> fields, GbaVerblijfplaatsBeperkt verblijfplaats)
	{
		// HACK because of requirement that datumInschrijvingInGemeente, gemeenteVanInschrijving and immigratie.landVanwaarIngeschreven must trigger verblijfplaats.InOnderzoek if any
		// These properties are placed in the GbaPersoon but should be part of GbaVerblijfplaats according to the GBA LO, but Haal Centraal refused to change this.
		// The law states that the data must be as the GBA LO specifies but nothing is said about the structure in which the data is placed.
		if ((fields.Any(field => field.Contains("gemeenteVanInschrijving"))
				|| fields.Any(field => field.Contains("adressering.adresregel"))
				|| fields.Any(field => field.Contains("adressering.land"))
				|| fields.Any(field => field.Contains("adresseringBinnenland.adresregel"))
				|| fields.Any(field => field.Equals("adressering"))
				|| fields.Any(field => field.Equals("adresseringBinnenland")))
			&& !fields.Contains("verblijfplaats") && !fields.Contains("verblijfplaats.inOnderzoek")
			&& verblijfplaats.InOnderzoek != null)
		{
			fields.Add("verblijfplaats.inOnderzoek");
		}
		else if (fields.All(field => !field.Contains("verblijfplaats.") && !field.Equals("verblijfplaats") && !field.Contains("adressering.adresregel") && !field.Contains("adresseringBinnenland.adresregel") && !field.Contains("adressering.land")) && !fields.Contains("verblijfplaats.inOnderzoek"))
		{
			verblijfplaats.InOnderzoek = null;
		}
	}

	/// <summary>
	/// This method removes all inOnderzoek of person if no person attributes are requested in the fields.
	/// Because of generic code, when one requests fields of a different category (in essence an attribute of person, then personInOnderzoek will be automatically revealed.
	/// Object oriented this makes sense, except business logic wise it doesn't and therefore this hack must be added.
	/// </summary>
	/// <param name="model"></param>
	/// <param name="gbaPersoon"></param>
	public void RemoveBeperktInOnderzoekFromPersonCategoryIfNoFieldsAreRequested(List<string> fields, GbaPersoonBeperkt gbaPersoon)
	{
		var fieldsToCompare = string.Join("&", fields.Select(field => _fieldsSettings.GbaFieldsSettings.ShortHandMappings.ContainsKey(field)
															? _fieldsSettings.GbaFieldsSettings.ShortHandMappings[field]
															: field)).Split('&');
		if (gbaPersoon.PersoonInOnderzoek != null
						&& new List<string> { "persoonInOnderzoek", "aNummer", "burgerservicenummer", "geheimhoudingPersoonsgegevens", "geslacht", "naam", "geboorte" }
							.All(propName => !fieldsToCompare.Any(field => field.Contains(propName) && !field.Contains("verblijfplaats") && !field.Contains("partners") && !field.Contains("kinderen") && !field.Contains("ouders"))))
		{
			gbaPersoon.PersoonInOnderzoek = null;
		}
	}
}
