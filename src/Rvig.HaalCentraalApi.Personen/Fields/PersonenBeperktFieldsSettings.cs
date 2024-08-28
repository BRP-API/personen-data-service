using Rvig.HaalCentraalApi.Shared.Fields;

namespace Rvig.HaalCentraalApi.Personen.Fields;

public class PersonenBeperktFieldsSettings : FieldsSettings
{
	public override FieldsSettingsModel GbaFieldsSettings { get; }
	public PersonenBeperktFieldsSettings()
	{
		GbaFieldsSettings = InitGbaFieldsSettings();
	}

	protected override FieldsSettingsModel InitGbaFieldsSettings()
	{
		return new FieldsSettingsModel("fields")
		{
			ForbiddenProperties = new List<string>()
			{
				"geheimhoudingPersoonsgegevens", "rni", "persoonInOnderzoek",
				"inOnderzoek", "opschortingBijhouding", "verificatie"
			},
			PropertiesToDiscard = new List<string>(),
			MandatoryProperties = new List<string>
			{
				"geheimhoudingPersoonsgegevens", "opschortingBijhouding",
				"verificatie", "persoonInOnderzoek",
				"rni"
			},
			SetChildPropertiesIfExistInScope = new Dictionary<string, string>(),
			SetPropertiesIfContextPropertyNotNull = new Dictionary<string, string>
			{
				{ "persoonInOnderzoek", "" }, { "persoonInOnderzoek.datumIngangOnderzoek", "persoonInOnderzoek" }
			},
			ShortHandMappings = new Dictionary<string, string>
			{
				// Proxy to GBA translation
				{ "naam", "naam&geslacht" },
				{ "adressering", "gemeenteVanInschrijving&verblijfplaats.straat&verblijfplaats.huisnummer&verblijfplaats.huisletter&verblijfplaats.huisnummertoevoeging&verblijfplaats.aanduidingBijHuisnummer&verblijfplaats.postcode&verblijfplaats.woonplaats&verblijfplaats.locatiebeschrijving&verblijfplaats.land&verblijfplaats.regel1&verblijfplaats.regel2&verblijfplaats.regel3" },
				{ "adressering.adresregel1", "verblijfplaats.straat&verblijfplaats.huisnummer&verblijfplaats.huisletter&verblijfplaats.huisnummertoevoeging&verblijfplaats.aanduidingBijHuisnummer&verblijfplaats.locatiebeschrijving&verblijfplaats.land&verblijfplaats.regel1" },
				{ "adressering.adresregel2", "gemeenteVanInschrijving&verblijfplaats.straat&verblijfplaats.huisnummer&verblijfplaats.postcode&verblijfplaats.woonplaats&verblijfplaats.locatiebeschrijving&verblijfplaats.land&verblijfplaats.regel2" },
				{ "adressering.adresregel3", "verblijfplaats.land&verblijfplaats.regel3" },
				{ "adressering.land", "verblijfplaats.land&verblijfplaats.regel1&verblijfplaats.regel2&verblijfplaats.regel3" },
				{ "adressering.land.code", "verblijfplaats.land&verblijfplaats.regel1&verblijfplaats.regel2&verblijfplaats.regel3" },
				{ "adressering.land.omschrijving", "verblijfplaats.land&verblijfplaats.regel1&verblijfplaats.regel2&verblijfplaats.regel3" },
				{ "adresseringBinnenland", "gemeenteVanInschrijving&verblijfplaats.straat&verblijfplaats.huisnummer&verblijfplaats.huisletter&verblijfplaats.huisnummertoevoeging&verblijfplaats.aanduidingBijHuisnummer&verblijfplaats.postcode&verblijfplaats.woonplaats&verblijfplaats.locatiebeschrijving" },
				{ "adresseringBinnenland.adresregel1", "verblijfplaats.straat&verblijfplaats.huisnummer&verblijfplaats.huisletter&verblijfplaats.huisnummertoevoeging&verblijfplaats.aanduidingBijHuisnummer&verblijfplaats.locatiebeschrijving" },
				{ "adresseringBinnenland.adresregel2", "gemeenteVanInschrijving&verblijfplaats.straat&verblijfplaats.huisnummer&verblijfplaats.postcode&verblijfplaats.woonplaats&verblijfplaats.locatiebeschrijving" },
				{ "naam.volledigeNaam", "naam.voornamen&naam.adellijkeTitelPredicaat&naam.voorvoegsel&naam.geslachtsnaam&geslacht" },
				{ "naam.voorletters", "naam.voornamen" },
				{ "leeftijd", "geboorte.datum" },

				// DATE FIELDS
				{ "geboorte.datum.type", "geboorte.datum" },
				{ "geboorte.datum.langFormaat", "geboorte.datum" },
				{ "geboorte.datum.datum", "geboorte.datum" },
				{ "geboorte.datum.jaar", "geboorte.datum" },
				{ "geboorte.datum.maand", "geboorte.datum" },
				{ "geboorte.datum.onbekend", "geboorte.datum" },

				// WAARDE TABELLEN
				{ "geslacht.code", "geslacht" },
				{ "geslacht.omschrijving", "geslacht" },
				{ "gemeenteVanInschrijving.code", "gemeenteVanInschrijving" },
				{ "gemeenteVanInschrijving.omschrijving", "gemeenteVanInschrijving" },
				{ "rni.deelnemer.code", "rni.deelnemer" },
				{ "rni.deelnemer.omschrijving", "rni.deelnemer" },
				{ "naam.adellijkeTitelPredicaat.code", "naam.adellijkeTitelPredicaat" },
				{ "naam.adellijkeTitelPredicaat.omschrijving", "naam.adellijkeTitelPredicaat" },
				{ "naam.adellijkeTitelPredicaat.soort", "naam.adellijkeTitelPredicaat" },
				{ "opschorting.reden.code", "opschorting.reden" },
				{ "opschorting.reden.omschrijving", "opschorting.reden" },
				{ "geboorte.land.code", "geboorte.land" },
				{ "geboorte.land.omschrijving", "geboorte.land" },
				{ "geboorte.plaats.code", "geboorte.plaats" },
				{ "geboorte.plaats.omschrijving", "geboorte.plaats" },

				// GEZAG TABELLEN
				{ "gezag.type", "gezag" },
				{ "gezag.ouder", "gezag" },
				{ "gezag.ouder.burgerservicenummer", "gezag" },
				{ "gezag.ouders", "gezag" },
				{ "gezag.ouders.burgerservicenummer", "gezag" },
				{ "gezag.derde", "gezag" },
				{ "gezag.derde.burgerservicenummer", "gezag" },
				{ "gezag.derden", "gezag" },
				{ "gezag.derden.burgerservicenummer", "gezag" },
				{ "gezag.minderjarige", "gezag" },
				{ "gezag.minderjarige.burgerservicenummer", "gezag" }
			}
		};
	}
}