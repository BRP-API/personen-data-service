using Rvig.HaalCentraalApi.Personen.ApiModels.BRP;
using Rvig.HaalCentraalApi.Personen.ApiModels.BRP.Common;
using Rvig.HaalCentraalApi.Personen.Fields;
using Rvig.HaalCentraalApi.Shared.Exceptions;
using Rvig.HaalCentraalApi.Shared.Fields;

namespace Rvig.HaalCentraalApi.Personen.Helpers;

/// <summary>
/// This class is used as a container for all methods that are 'hacks' or specific exceptions to generic code.
/// These methods are a result of the GBA API having to conform to demands that should be solved elsewhere but will not be or there isn't enough time.
/// These solutions should be performed by the RvIG and other organisations.
/// </summary>
public class GbaPersonenApiHelper : GbaPersonenApiHelperBase
{
	private readonly FieldsFilterService _fieldsExpandFilterService;
	private readonly PersonenFieldsSettings _fieldsSettings;

	public GbaPersonenApiHelper(FieldsFilterService fieldsExpandFilterService, PersonenFieldsSettings fieldsSettings)
	{
		_fieldsExpandFilterService = fieldsExpandFilterService;
		_fieldsSettings = fieldsSettings;
	}

	public void HackLogicKinderenPartnersOuders(List<string> fields, List<GbaPersoon>? gbaPersonen)
	{
		// HACK: If Kind is not default in its entirety but is in Geboorte, Naam and Burgerservicenummer then make it default anyway
		if (fields.Any(field => field.Contains("kinderen")))
		{
			gbaPersonen?.ForEach(gbaPersoon =>
			{
				if (gbaPersoon?.Kinderen?.Any() == true)
				{
					gbaPersoon.Kinderen = gbaPersoon.Kinderen.Where(kind => !IsDefaultInGeboorteNaamBurgerservicenummerInOnderzoek(kind.Geboorte, kind.Naam, kind.Burgerservicenummer, kind.InOnderzoek)).ToList();
				}
			});
		}

		// HACK: If Partner is not default in its entirety but is in Geboorte, Naam and Burgerservicenummer then make it default anyway
		if (fields.Any(field => field.Contains("partners")))
		{
			gbaPersonen?.ForEach(gbaPersoon =>
			{
				if (gbaPersoon?.Partners?.Any() == true)
				{
					gbaPersoon.Partners = gbaPersoon.Partners.Where(partner => !IsPartnerDefault(partner)).ToList();
				}
			});
		}

		// HACK: If Ouder is not default in its entirety but is in Geboorte, Naam and Burgerservicenummer then make it default anyway
		if (fields.Any(field => field.Contains("ouders")))
		{
			gbaPersonen?.ForEach(gbaPersoon =>
			{
				if (gbaPersoon?.Ouders?.Any() == true)
				{
					gbaPersoon.Ouders = gbaPersoon.Ouders.Where(ouder => !IsOuderDefault(ouder)).ToList();
				}
			});
		}
	}

	/// <summary>
	/// Hack because business logic require the GBA API to support some fields when given specific cases. This is different from standard logic and therefore a hack.
	/// </summary>
	public static void AddVerblijfplaatsInOnderzoek(List<string> fields, GbaVerblijfplaats verblijfplaats)
	{
		// HACK because of requirement that datumInschrijvingInGemeente, gemeenteVanInschrijving and immigratie.landVanwaarIngeschreven must trigger verblijfplaats.InOnderzoek if any
		// These properties are placed in the GbaPersoon but should be part of GbaVerblijfplaats according to the GBA LO, but Haal Centraal refused to change this.
		// The law states that the data must be as the GBA LO specifies but nothing is said about the structure in which the data is placed.
		if ((fields.Any(field => field.Contains("datumInschrijvingInGemeente"))
				|| fields.Any(field => field.Contains("gemeenteVanInschrijving"))
				|| fields.Any(field => field.Contains("immigratie"))
				|| fields.Any(field => field.Contains("immigratie.landVanwaarIngeschreven"))
				|| fields.Any(field => field.Contains("adressering.adresregel"))
				|| fields.Any(field => field.Contains("adressering.land"))
				|| fields.Any(field => field.Contains("adresseringBinnenland.adresregel"))
				|| fields.Any(field => field.Equals("adressering"))
				|| fields.Any(field => field.Equals("adresseringBinnenland"))
				|| fields.Any(field => field.Contains("verblijfplaatsBinnenland")))
			&& !fields.Contains("verblijfplaats") && !fields.Contains("verblijfplaats.inOnderzoek")
			&& verblijfplaats.InOnderzoek != null)
		{
			fields.Add("verblijfplaats.inOnderzoek");
		}
		else if (fields.All(field => !field.Equals("adressering") && !field.Equals("adresseringBinnenland.") && !field.Contains("verblijfplaats.") && !field.Equals("verblijfplaats") && !field.Contains("adressering.adresregel") && !field.Contains("adresseringBinnenland.adresregel") && !field.Contains("adressering.land")) && !fields.Contains("verblijfplaats.inOnderzoek"))
		{
			verblijfplaats.InOnderzoek = null;
		}
	}

	/// <summary>
	/// Hack because business logic require the GBA API to support some fields when given specific cases. This is different from standard logic and therefore a hack.
	/// </summary>
	public static void AddLandVanwaarIngeschreven(List<string> fields, GbaImmigratie immigratie)
	{
		// HACK because of requirement that vanuitVerblijfplaatsOnbekend and indicatieVestigingVanuitBuitenland must trigger immigratie.landVanwaarIngeschreven if any
		// Again like above this is because of the Haal Centraal standard.
		if (fields.Any(field => field.Contains("vanuitVerblijfplaatsOnbekend"))/* || fields.Any(field => field.Contains("indicatieVestigingVanuitBuitenland"))*/
			&& !fields.Contains("immigratie") && !fields.Contains("immigratie.landVanwaarIngeschreven")
			&& immigratie.LandVanwaarIngeschreven != null)
		{
			fields.Add("immigratie.landVanwaarIngeschreven");
		}
		else if (fields.All(field => !field.Contains("immigratie") && !field.Contains("immigratie.landVanwaarIngeschreven")))
		{
			immigratie.LandVanwaarIngeschreven = null;
		}
	}

	/// <summary>
	/// Hack because business logic require the GBA API to support some fields when given specific cases. This is different from standard logic and therefore a hack.
	/// If PL OpschortingBijhouding is not null then PL is no longer valid and may only return OpschortingBijhouding.Reden and if asked also OpschortingBijhouding.Datum, Burgerservicenummer and ANummer
	/// </summary>
	public static List<string> OpschortingBijhoudingLogicRaadplegen(List<string> fields, GbaOpschortingBijhouding? opschortingBijhouding)
	{
		if (opschortingBijhouding == null || opschortingBijhouding.Reden?.Code?.Equals("F") == false)
		{
			return fields;
		}
		var newFields = new List<string>();
		if (fields.Any(field => field.Contains("burgerservicenummer")))
		{
			newFields.Add("burgerservicenummer");
		}
		if (fields.Any(field => field.Contains("aNummer")))
		{
			newFields.Add("aNummer");
		}

		newFields.Add("opschortingBijhouding");

		return newFields;
	}

	/// <summary>
	/// This method removes all inOnderzoek of gezagsverhouding if no gezagsverhouding attributes are requested in the fields.
	/// Because of generic code, when one requests fields of a different category (in essence an attribute of person, then personInOnderzoek will be automatically revealed.
	/// Object oriented this makes sense, except business logic wise it doesn't and therefore this hack must be added.
	/// </summary>
	/// <param name="model"></param>
	/// <param name="gbaPersoon"></param>
	public void RemoveInOnderzoekFromGezagsverhoudingCategoryIfNoFieldsAreRequested(List<string> fields, GbaPersoon gbaPersoon)
	{
		var fieldsToCompare = string.Join("&", fields.Select(field => _fieldsSettings.GbaFieldsSettings.ShortHandMappings.ContainsKey(field)
															? _fieldsSettings.GbaFieldsSettings.ShortHandMappings[field]
															: field)).Split('&');
		if (gbaPersoon.GezagInOnderzoek != null
		&& new List<string> { "gezagInOnderzoek", "indicatieCurateleRegister", "indicatieGezagMinderjarige" }
			.All(propName => !fieldsToCompare.Any(field => field.Contains(propName))))
		{
			gbaPersoon.GezagInOnderzoek = null;
		}
	}

	/// <summary>
	/// This method removes all inOnderzoek of person if no person attributes are requested in the fields.
	/// Because of generic code, when one requests fields of a different category (in essence an attribute of person, then personInOnderzoek will be automatically revealed.
	/// Object oriented this makes sense, except business logic wise it doesn't and therefore this hack must be added.
	/// </summary>
	/// <param name="model"></param>
	/// <param name="gbaPersoon"></param>
	public void RemoveInOnderzoekFromPersonCategoryIfNoFieldsAreRequested(List<string> fields, GbaPersoon gbaPersoon)
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

	//private bool IsGezagDefault(AbstractGezagsrelatie gezag)
	//{
	//	return gezag switch
	//	{
	//		EenhoofdigOuderlijkGezag eenhoofdigOuderlijkGezag => eenhoofdigOuderlijkGezag.Minderjarige == null && eenhoofdigOuderlijkGezag.Ouder == null,
	//		TweehoofdigOuderlijkGezag tweehoofdigOuderlijkGezag => tweehoofdigOuderlijkGezag.Minderjarige == null && tweehoofdigOuderlijkGezag.Ouders?.Any() == false,
	//		GezagNietTeBepalen gezagNietTeBepalen => gezagNietTeBepalen != null,
	//		GezamenlijkGezag gezamenlijkGezag => gezamenlijkGezag.Minderjarige == null && gezamenlijkGezag.Ouder == null && gezamenlijkGezag.Derde == null,
	//		TijdelijkGeenGezag tijdelijkGeenGezag => tijdelijkGeenGezag != null,
	//		Voogdij voogdij => voogdij.Minderjarige == null && voogdij.Derden?.Any() == false,
	//		_ => throw new CustomInvalidOperationException($"Onbekend type gezag: {gezag.GetType().Name}")
	//	};
	//}

	private bool IsOuderDefault(GbaOuder ouder)
	{
		return IsDefaultInGeboorteNaamBurgerservicenummerInOnderzoek(ouder.Geboorte, ouder.Naam, ouder.Burgerservicenummer, ouder.InOnderzoek)
			&& string.IsNullOrEmpty(ouder.DatumIngangFamilierechtelijkeBetrekking)
			&& string.IsNullOrEmpty(ouder.OuderAanduiding)
			&& (ouder.Geslacht == null || _fieldsExpandFilterService.IsDefault(ouder.Geslacht, ouder.Geslacht.GetType()));
	}

	private bool IsPartnerDefault(GbaPartner partner)
	{
		return IsDefaultInGeboorteNaamBurgerservicenummerInOnderzoek(partner.Geboorte, partner.Naam, partner.Burgerservicenummer, partner.InOnderzoek)
			&& IsHuwelijkPartnerschapDefault(partner.AangaanHuwelijkPartnerschap)
			&& IsOntbindingDefault(partner.OntbindingHuwelijkPartnerschap)
			&& (partner.Geslacht == null || _fieldsExpandFilterService.IsDefault(partner.Geslacht, partner.Geslacht.GetType()))
			&& (partner.SoortVerbintenis == null || _fieldsExpandFilterService.IsDefault(partner.SoortVerbintenis, partner.SoortVerbintenis.GetType()));
	}

	private bool IsDefaultInGeboorteNaamBurgerservicenummerInOnderzoek(GbaGeboorte? geboorte, NaamBasis? naam, string? burgerservicenummer, InOnderzoek? inOnderzoek)
	{
		return IsGeboorteDefault(geboorte)
			&& IsNaamDefault(naam)
			&& IsInOnderzoek(inOnderzoek)
			&& string.IsNullOrEmpty(burgerservicenummer);
	}

	private bool IsGeboorteDefault(GbaGeboorte? geboorte)
	{
		return geboorte == null || _fieldsExpandFilterService.IsDefault(geboorte, geboorte.GetType())
			|| (geboorte.Land == null || _fieldsExpandFilterService.IsDefault(geboorte.Land, geboorte.Land.GetType()))
				&& (geboorte.Plaats == null || _fieldsExpandFilterService.IsDefault(geboorte.Plaats, geboorte.Plaats.GetType()))
				&& (geboorte.Datum == null || _fieldsExpandFilterService.IsDefault(geboorte.Datum, geboorte.Datum.GetType()))
				&& (geboorte.DatumJaar == null || _fieldsExpandFilterService.IsDefault(geboorte.DatumJaar, geboorte.DatumJaar.GetType()))
				&& (geboorte.DatumMaand == null || _fieldsExpandFilterService.IsDefault(geboorte.DatumMaand, geboorte.DatumMaand.GetType()))
				&& (geboorte.DatumDag == null || _fieldsExpandFilterService.IsDefault(geboorte.DatumDag, geboorte.DatumDag.GetType()));
	}

	private bool IsInOnderzoek(InOnderzoek? inOnderzoek)
	{
		return inOnderzoek == null || _fieldsExpandFilterService.IsDefault(inOnderzoek, inOnderzoek.GetType())
			|| (inOnderzoek.AanduidingGegevensInOnderzoek == null || _fieldsExpandFilterService.IsDefault(inOnderzoek.AanduidingGegevensInOnderzoek, inOnderzoek.AanduidingGegevensInOnderzoek.GetType()))
				&& (inOnderzoek.DatumIngangOnderzoek == null || _fieldsExpandFilterService.IsDefault(inOnderzoek.DatumIngangOnderzoek, inOnderzoek.DatumIngangOnderzoek.GetType()));
	}

	private bool IsNaamDefault(NaamBasis? naam)
	{
		return naam == null || _fieldsExpandFilterService.IsDefault(naam, naam.GetType())
			|| (naam.Geslachtsnaam == null || _fieldsExpandFilterService.IsDefault(naam.Geslachtsnaam, naam.Geslachtsnaam.GetType()))
				&& (naam.AdellijkeTitelPredicaat == null || _fieldsExpandFilterService.IsDefault(naam.AdellijkeTitelPredicaat, naam.AdellijkeTitelPredicaat.GetType()))
				&& (naam.Voornamen == null || _fieldsExpandFilterService.IsDefault(naam.Voornamen, naam.Voornamen.GetType()))
				&& (naam.Voorvoegsel == null || _fieldsExpandFilterService.IsDefault(naam.Voorvoegsel, naam.Voorvoegsel.GetType()));
	}

	private bool IsHuwelijkPartnerschapDefault(GbaAangaanHuwelijkPartnerschap? aangaan)
	{
		return aangaan == null || _fieldsExpandFilterService.IsDefault(aangaan, aangaan.GetType())
			|| (aangaan.Plaats == null || _fieldsExpandFilterService.IsDefault(aangaan.Plaats, aangaan.Plaats.GetType()))
				&& (aangaan.DatumDag == null || _fieldsExpandFilterService.IsDefault(aangaan.DatumDag, aangaan.DatumDag.GetType()))
				&& (aangaan.DatumMaand == null || _fieldsExpandFilterService.IsDefault(aangaan.DatumMaand, aangaan.DatumMaand.GetType()))
				&& (aangaan.DatumJaar == null || _fieldsExpandFilterService.IsDefault(aangaan.DatumJaar, aangaan.DatumJaar.GetType()))
				&& (aangaan.Datum == null || _fieldsExpandFilterService.IsDefault(aangaan.Datum, aangaan.Datum.GetType()))
				&& (aangaan.Land == null || _fieldsExpandFilterService.IsDefault(aangaan.Land, aangaan.Land.GetType()));
	}

	private bool IsOntbindingDefault(GbaOntbindingHuwelijkPartnerschap? ontbinding)
	{
		return ontbinding == null || _fieldsExpandFilterService.IsDefault(ontbinding, ontbinding.GetType())
			|| ontbinding.Datum == null || _fieldsExpandFilterService.IsDefault(ontbinding.Datum, ontbinding.Datum.GetType());
	}
}
