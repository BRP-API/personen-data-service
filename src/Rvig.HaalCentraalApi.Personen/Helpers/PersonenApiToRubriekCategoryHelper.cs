using Rvig.HaalCentraalApi.Personen.ApiModels.BRP;
using Rvig.HaalCentraalApi.Shared.Exceptions;
using Rvig.HaalCentraalApi.Shared.Helpers;
using Rvig.HaalCentraalApi.Personen.RequestModels.BRP;

namespace Rvig.HaalCentraalApi.Personen.Helpers;

public static class PersonenApiToRubriekCategoryHelper
{
	private static readonly IDictionary<string, string> _fieldRubriekCategoryDictionary = new Dictionary<string, string>
	{
		{ "rni", "018810, 048810, 068810, 078810, 088810, 018820, 048820, 068820, 078820, 088820" },
		{ "rni.categorie", "018810, 048810, 068810, 078810, 088810, 018820, 048820, 068820, 078820, 088820" },
		{ "rni.deelnemer", "018810, 048810, 068810, 078810, 088810" },
		{ "rni.deelnemer.code", "018810, 048810, 068810, 078810, 088810" },
		{ "rni.deelnemer.omschrijving", "018810, 048810, 068810, 078810, 088810" },
		{ "rni.omschrijvingVerdrag", "018820, 048820, 068820, 078820, 088820" },

		{ "aNummer", "010110" },
		{ "burgerservicenummer", "010120" },
		{ "naam", "010210, 010220, 010230, 010240, 016110" },
		{ "naam.voornamen", "010210" },
		{ "naam.adellijkeTitelPredicaat", "010220" },
		{ "naam.adellijkeTitelPredicaat.code", "010220" },
		{ "naam.adellijkeTitelPredicaat.omschrijving", "010220" },
		{ "naam.adellijkeTitelPredicaat.soort", "010220" },
		{ "naam.voorvoegsel", "010230" },
		{ "naam.geslachtsnaam", "010240" },
		{ "geboorte", "010310, 010320, 010330" },
		{ "geboorte.datum", "010310" },
		{ "geboorte.plaats", "010320" },
		{ "geboorte.plaats.code", "010320" },
		{ "geboorte.plaats.omschrijving", "010320" },
		{ "geboorte.land", "010330" },
		{ "geboorte.land.code", "010330" },
		{ "geboorte.land.omschrijving", "010330" },
		{ "geslacht", "010410" },
		{ "geslacht.code", "010410" },
		{ "geslacht.omschrijving", "010410" },
		//{ "", "012010" }, // Vorige a nummer
		//{ "", "012020" }, // Volgende a nummer
		{ "naam.aanduidingNaamgebruik", "016110" },
		{ "naam.aanduidingNaamgebruik.code", "016110" },
		{ "naam.aanduidingNaamgebruik.omschrijving", "016110" },
		//{ "", "018110" }, // Registergemeente akte
		//{ "", "018120" }, // Aktenummer van de akte
		//{ "", "018210" }, // gemeente van de gegevens over persoon aan het document ontleend zijn
		//{ "", "018220" }, // Datum van de ontlening van de gegevens over persoon
		//{ "", "018230" }, // Beschrijving van het document waaraan de gegevens over persoon ontleend zijn
		{ "persoonInOnderzoek", "018310, 018320" }, // By default geaccepteerd
		{ "persoonInOnderzoek.aanduidingGegevensInOnderzoek", "018310" }, // By default geaccepteerd
		{ "persoonInOnderzoek.datumIngangOnderzoek", "018320" }, // By default geaccepteerd
		//{ "persoonInOnderzoek", "018330" }, // Geen einddatum
		//{ "", "018410" }, // Geen indicatie onjuist
		//{ "", "018510" }, // Datum ingang geldigheid
		//{ "", "018610" }, // Datum opneming met betrekking tot persoon categorie
		//{ "", "018810" }, // RNI By default geaccepteerd
		//{ "", "018820" }, // RNI By default geaccepteerd

		//{ "ouders.", "020110, 030110" }, // Geen anummer ouders.
		{ "ouders", "020120, 030120, 020210, 030210, 020220, 030220, 020230, 030230, 020240, 030240, 020310, 030310, 020320, 030320, 020330, 030330, 020410, 030410, 026210, 036210, 028310, 038310, 028320, 038320" },
		{ "ouders.burgerservicenummer", "020120, 030120" },
		{ "ouders.naam", "020210, 030210, 020220, 030220, 020230, 030230, 020240, 030240" },
		{ "ouders.naam.voornamen", "020210, 030210" },
		{ "ouders.naam.adellijkeTitelPredicaat", "020220, 030220" },
		{ "ouders.naam.adellijkeTitelPredicaat.code", "020220, 030220" },
		{ "ouders.naam.adellijkeTitelPredicaat.omschrijving", "020220, 030220" },
		{ "ouders.naam.adellijkeTitelPredicaat.soort", "020220, 030220" },
		{ "ouders.naam.voorvoegsel", "020230, 030230" },
		{ "ouders.naam.geslachtsnaam", "020240, 030240" },
		{ "ouders.geboorte", "020310, 030310, 020320, 030320, 020330, 030330" },
		{ "ouders.geboorte.datum", "020310, 030310" },
		{ "ouders.geboorte.plaats", "020320, 030320" },
		{ "ouders.geboorte.plaats.code", "020320, 030320" },
		{ "ouders.geboorte.plaats.omschrijving", "020320, 030320" },
		{ "ouders.geboorte.land", "020330, 030330" },
		{ "ouders.geboorte.land.code", "020330, 030330" },
		{ "ouders.geboorte.land.omschrijving", "020330, 030330" },
		{ "ouders.geslacht", "020410, 030410" },
		{ "ouders.geslacht.code", "020410, 030410" },
		{ "ouders.geslacht.omschrijving", "020410, 030410" },
		{ "ouders.datumIngangFamilierechtelijkeBetrekking", "026210, 036210" },
		//{ "ouders.", "028110, 038110" }, // Registergemeente akte
		//{ "ouders.", "028120, 038120" }, // Aktenummer van de akte
		//{ "ouders.", "028210, 038210" }, // gemeente van de gegevens over ouder aan het document ontleend zijn
		//{ "ouders.", "028220, 038220" }, // Datum van de ontlening van de gegevens over ouder
		//{ "ouders.", "028230, 038230" }, // Beschrijving van het document waaraan de gegevens over ouder ontleend zijn
		{ "ouders.inOnderzoek", "028310, 038310, 028320, 038320" }, // By default geaccepteerd inonderzoek
		{ "ouders.inOnderzoek.aanduidingGegevensInOnderzoek", "028310, 038310" }, // By default geaccepteerd inonderzoek
		{ "ouders.inOnderzoek.datumIngangOnderzoek", "028320, 038320" }, // By default geaccepteerd inonderzoek
		//{ "ouders.", "028330, 038330" }, // Geen einddatum inonderzoek
		//{ "ouders.", "028410, 038410" }, // Geen indicatie onjuist
		//{ "ouders.", "028510, 038510" }, // Datum ingang geldigheid
		//{ "ouders.", "028610, 038610" }, // Datum opneming met betrekking tot persoon categorie

		{ "nationaliteiten", "040510, 046310, 046510, 048310, 048320, 048510" },
		{ "nationaliteiten.nationaliteit", "040510" },
		{ "nationaliteiten.nationaliteit.code", "040510" },
		{ "nationaliteiten.nationaliteit.omschrijving", "040510" },
		{ "nationaliteiten.redenOpname", "046310" },
		{ "nationaliteiten.redenOpname.code", "046310" },
		{ "nationaliteiten.redenOpname.omschrijving", "046310" },
		//{ "nationaliteiten.", "046410" }, // Geen reden beeindigen in actueel
		{ "nationaliteiten.aanduidingBijzonderNederlanderschap", "046510" },
		//{ "nationaliteiten.", "047310" }, //EU Persoonsnummer
		//{ "nationaliteiten.", "048210" }, // gemeente van de gegevens over nationaliteit aan het document ontleend zijn
		//{ "nationaliteiten.", "048220" }, // Datum van de ontlening van de gegevens over nationaliteit
		//{ "nationaliteiten.", "048230" }, // Beschrijving van het document waaraan de gegevens over nationaliteit ontleend zijn
		{ "nationaliteiten.inOnderzoek", "048310, 048320" }, // By default geaccepteerd inonderzoek
		{ "nationaliteiten.inOnderzoek.aanduidingGegevensInOnderzoek", "048310" }, // By default geaccepteerd inonderzoek
		{ "nationaliteiten.inOnderzoek.datumIngangOnderzoek", "048320" }, // By default geaccepteerd inonderzoek
		//{ "nationaliteiten.", "048330" }, // Geen einddatum inonderzoek
		//{ "nationaliteiten.", "048410" }, // Geen indicatie onjuist
		{ "nationaliteiten.datumIngangGeldigheid", "048510" }, // Datum ingang geldigheid
		//{ "nationaliteiten.", "048610" }, // Datum opneming met betrekking tot nationaliteit categorie
		//{ "nationaliteiten.", "048810" }, // RNI
		//{ "nationaliteiten.", "048820" }, // RNI

		//{ "ouders.", "020110" }, // Geen anummer ouders.
		{ "partners", "050120, 050210, 050220, 050230, 050240, 050310, 050320, 050330, 050410, 050610, 050620, 050630, 058310, 058320, 051510, 058310, 058320, 050710" },
		{ "partners.burgerservicenummer", "050120" },
		{ "partners.naam", "050210, 050220, 050230, 050240" },
		{ "partners.naam.voornamen", "050210" },
		{ "partners.naam.adellijkeTitelPredicaat", "050220" },
		{ "partners.naam.adellijkeTitelPredicaat.code", "050220" },
		{ "partners.naam.adellijkeTitelPredicaat.omschrijving", "050220" },
		{ "partners.naam.adellijkeTitelPredicaat.soort", "050220" },
		{ "partners.naam.voorvoegsel", "050230" },
		{ "partners.naam.geslachtsnaam", "050240" },
		{ "partners.geboorte", "050310, 050320, 050330" },
		{ "partners.geboorte.datum", "050310" },
		{ "partners.geboorte.plaats", "050320" },
		{ "partners.geboorte.plaats.code", "050320" },
		{ "partners.geboorte.plaats.omschrijving", "050320" },
		{ "partners.geboorte.land", "050330" },
		{ "partners.geboorte.land.code", "050330" },
		{ "partners.geboorte.land.omschrijving", "050330" },
		{ "partners.geslacht", "050410" },
		{ "partners.geslacht.code", "050410" },
		{ "partners.geslacht.omschrijving", "050410" },
		{ "partners.aangaanHuwelijkPartnerschap", "050610, 050620, 050630, 058310, 058320" },
		{ "partners.aangaanHuwelijkPartnerschap.datum", "050610" },
		{ "partners.aangaanHuwelijkPartnerschap.plaats", "050620" },
		{ "partners.aangaanHuwelijkPartnerschap.plaats.code", "050620" },
		{ "partners.aangaanHuwelijkPartnerschap.plaats.omschrijving", "050620" },
		{ "partners.aangaanHuwelijkPartnerschap.land", "050630" },
		{ "partners.aangaanHuwelijkPartnerschap.land.code", "050630" },
		{ "partners.aangaanHuwelijkPartnerschap.land.omschrijving", "050630" },
		{ "partners.ontbindingHuwelijkPartnerschap", "050710" },
		{ "partners.ontbindingHuwelijkPartnerschap.datum", "050710" },
		//{ "partners.ontbindingHuwelijkPartnerschap", "050720" },	// Niet aanwezig in actueel
		//{ "partners.ontbindingHuwelijkPartnerschap", "050730" },	// Niet aanwezig in actueel
		//{ "partners.ontbindingHuwelijkPartnerschap", "050740" },	// Niet aanwezig in actueel
		{ "partners.soortVerbintenis", "051510" },
		//{ "partners.", "058110" }, // Registergemeente akte
		//{ "partners.", "058120" }, // Aktenummer van de akte
		//{ "partners.", "058210" }, // gemeente van de gegevens over ouder aan het document ontleend zijn
		//{ "partners.", "058220" }, // Datum van de ontlening van de gegevens over ouder
		//{ "partners.", "058230" }, // Beschrijving van het document waaraan de gegevens over ouder ontleend zijn
		{ "partners.inOnderzoek", "058310, 058320" }, // By default geaccepteerd inonderzoek
		{ "partners.inOnderzoek.aanduidingGegevensInOnderzoek", "058310" }, // By default geaccepteerd inonderzoek
		{ "partners.inOnderzoek.datumIngangOnderzoek", "058320" }, // By default geaccepteerd inonderzoek
		//{ "partners.", "058330" }, // Geen einddatum inonderzoek
		//{ "partners.", "058410" }, // Geen indicatie onjuist
		//{ "partners.", "058510" }, // Datum ingang geldigheid
		//{ "partners.", "058610" }, // Datum opneming met betrekking tot persoon categorie

		{ "overlijden", "060810, 060820, 060830, 068310, 068320" },
		{ "overlijden.datum", "060810" },
		{ "overlijden.plaats", "060820" },
		{ "overlijden.plaats.code", "060820" },
		{ "overlijden.plaats.omschrijving", "060820" },
		{ "overlijden.land", "060830" },
		{ "overlijden.land.code", "060830" },
		{ "overlijden.land.omschrijving", "060830" },
		//{ "overlijden.", "068110" }, // Registergemeente akte
		//{ "overlijden.", "068120" }, // Aktenummer van de akte
		//{ "overlijden.", "068210" }, // gemeente van de gegevens over ouder aan het document ontleend zijn
		//{ "overlijden.", "068220" }, // Datum van de ontlening van de gegevens over ouder
		//{ "overlijden.", "068230" }, // Beschrijving van het document waaraan de gegevens over ouder ontleend zijn
		{ "overlijden.inOnderzoek", "068310, 068320" }, // By default geaccepteerd inonderzoek
		{ "overlijden.inOnderzoek.aanduidingGegevensInOnderzoek", "068310" }, // By default geaccepteerd inonderzoek
		{ "overlijden.inOnderzoek.datumIngangOnderzoek", "068320" }, // By default geaccepteerd inonderzoek
		//{ "overlijden.", "068330" }, // Geen einddatum inonderzoek
		//{ "overlijden.", "068410" }, // Geen indicatie onjuist
		//{ "overlijden.", "068510" }, // Datum ingang geldigheid
		//{ "overlijden.", "068610" }, // Datum opneming met betrekking tot persoon categorie
		//{ "", "068810" }, // RNI
		//{ "", "068820" }, // RNI

		//{ "", "076620" }, // Niet gebruikt datum ingang blokkering PL
		{ "opschortingBijhouding", "076710, 076720" },
		{ "opschortingBijhouding.datum", "076710" },
		{ "opschortingBijhouding.reden", "076720" },
		{ "opschortingBijhouding.reden.code", "076720" },
		{ "opschortingBijhouding.reden.omschrijving", "076720" },
		{ "datumEersteInschrijvingGBA", "076810" },
		//{ "", "076910" }, // Niet beschikbaar
		{ "geheimhoudingPersoonsgegevens", "077010" },
		{ "verificatie", "077110, 077120" }, // Verificatie
		{ "verificatie.datum", "077110" }, // Verificatie
		{ "verificatie.omschrijving", "077120" }, // Verificatie
		//{ "", "078010" }, // Versienummer
		//{ "", "078020" }, // Datumtijdstempel
		//{ "", "078710" }, // PK-gegevens volledig meegeconverteerd
		//{ "", "078810" }, // RNI
		//{ "", "078820" }, // RNI

		{ "gemeenteVanInschrijving", "080910" },
		{ "gemeenteVanInschrijving.code", "080910" },
		{ "gemeenteVanInschrijving.omschrijving", "080910" },
		{ "datumInschrijvingInGemeente", "080920" },
		{ "verblijfplaats", "080910, 080920, 081010, 081030, 081110, 081115, 081120, 081130, 081140, 081150, 081160, 081170, 081180, 081190, 081210, 081310, 081320, 081330, 081340, 081350, 081410, 081420, 088310, 088320, 088510" },
		{ "verblijfplaats.functieAdres", "081010" },
		{ "verblijfplaats.functieAdres.code", "081010" },
		{ "verblijfplaats.functieAdres.omschrijving", "081010" },
		//{ "verblijfplaats", "081020" }, // Gemeentedeel niet aanwezig
		{ "verblijfplaats.datumAanvangAdreshouding", "081030" },
		{ "verblijfplaats.straat", "081110" },
		{ "verblijfplaats.naamOpenbareRuimte", "081115" },
		{ "verblijfplaats.huisnummer", "081120" },
		{ "verblijfplaats.huisletter", "081130" },
		{ "verblijfplaats.huisnummertoevoeging", "081140" },
		{ "verblijfplaats.aanduidingBijHuisnummer", "081150" },
		{ "verblijfplaats.aanduidingBijHuisnummer.code", "081150" },
		{ "verblijfplaats.aanduidingBijHuisnummer.omschrijving", "081150" },
		{ "verblijfplaats.postcode", "081160" },
		{ "verblijfplaats.woonplaats", "081170" },
		{ "verblijfplaats.adresseerbaarObjectIdentificatie", "081180" },
		{ "verblijfplaats.nummeraanduidingIdentificatie", "081190" },
		{ "verblijfplaats.locatiebeschrijving", "081210" },
		{ "verblijfplaats.land", "081310" },
		{ "verblijfplaats.land.code", "081310" },
		{ "verblijfplaats.land.omschrijving", "081310" },
		{ "verblijfplaats.datumAanvangAdresBuitenland", "081320" },
		{ "verblijfplaats.regel1", "081330" },
		{ "verblijfplaats.regel2", "081340" },
		{ "verblijfplaats.regel3", "081350" },
		{ "immigratie", "081410, 081420" },
		{ "immigratie.landVanwaarIngeschreven", "081410" },
		{ "immigratie.landVanwaarIngeschreven.code", "081410" },
		{ "immigratie.landVanwaarIngeschreven.omschrijving", "081410" },
		{ "immigratie.datumVestigingInNederland", "081420" },
		//{ "verblijfplaats.", "087210" }, // Omschrijving van de aangifte adreshouding
		//{ "verblijfplaats.", "087510" }, // Indicatie document
		{ "verblijfplaats.inOnderzoek", "088310, 088320" }, // By default geaccepteerd inonderzoek
		{ "verblijfplaats.inOnderzoek.aanduidingGegevensInOnderzoek", "088310" }, // By default geaccepteerd inonderzoek
		{ "verblijfplaats.inOnderzoek.datumIngangOnderzoek", "088320" }, // By default geaccepteerd inonderzoek
		//{ "verblijfplaats.", "088330" }, // Geen einddatum inonderzoek
		//{ "verblijfplaats.", "088410" }, // Geen indicatie onjuist
		{ "verblijfplaats.datumIngangGeldigheid", "088510" }, // Datum ingang geldigheid
		//{ "verblijfplaats.", "088610" }, // Datum opneming met betrekking tot persoon categorie
		//{ "verblijfplaats.", "088810" }, // RNI
		//{ "verblijfplaats.", "088820" }, // RNI

		//{ "kinderen.", "090110" }, // Geen anummer kinderen.
		{ "kinderen", "090120, 090210, 090220, 090230, 090240, 090310, 090320, 090330, 098310, 098320" },
		{ "kinderen.burgerservicenummer", "090120" },
		{ "kinderen.naam", "090210, 090220, 090230, 090240" },
		{ "kinderen.naam.voornamen", "090210" },
		{ "kinderen.naam.adellijkeTitelPredicaat", "090220" },
		{ "kinderen.naam.adellijkeTitelPredicaat.code", "090220" },
		{ "kinderen.naam.adellijkeTitelPredicaat.omschrijving", "090220" },
		{ "kinderen.naam.adellijkeTitelPredicaat.soort", "090220" },
		{ "kinderen.naam.voorvoegsel", "090230" },
		{ "kinderen.naam.geslachtsnaam", "090240" },
		{ "kinderen.geboorte", "090310, 090320, 090330" },
		{ "kinderen.geboorte.datum", "090310" },
		{ "kinderen.geboorte.plaats", "090320" },
		{ "kinderen.geboorte.plaats.code", "090320" },
		{ "kinderen.geboorte.plaats.omschrijving", "090320" },
		{ "kinderen.geboorte.land", "090330" },
		{ "kinderen.geboorte.land.code", "090330" },
		{ "kinderen.geboorte.land.omschrijving", "090330" },
		//{ "kinderen.", "098110" }, // Registergemeente akte
		//{ "kinderen.", "098120" }, // Aktenummer van de akte
		//{ "kinderen.", "098210" }, // gemeente van de gegevens over ouder aan het document ontleend zijn
		//{ "kinderen.", "098220" }, // Datum van de ontlening van de gegevens over ouder
		//{ "kinderen.", "098230" }, // Beschrijving van het document waaraan de gegevens over ouder ontleend zijn
		{ "kinderen.inOnderzoek", "098310, 098320" }, // By default geaccepteerd inonderzoek
		{ "kinderen.inOnderzoek.aanduidingGegevensInOnderzoek", "098310" }, // By default geaccepteerd inonderzoek
		{ "kinderen.inOnderzoek.datumIngangOnderzoek", "098320" }, // By default geaccepteerd inonderzoek
		//{ "kinderen.", "098330" }, // Geen einddatum inonderzoek
		//{ "kinderen.", "098410" }, // Geen indicatie onjuist
		//{ "kinderen.", "098510" }, // Datum ingang geldigheid
		//{ "kinderen.", "098610" }, // Datum opneming met betrekking tot persoon categorie
		//{ "", "098910" }, // Registratie betrekking

		{ "verblijfstitel", "103910, 103920, 103930, 108310, 108320" },
		{ "verblijfstitel.aanduiding", "103910" },
		{ "verblijfstitel.aanduiding.code", "103910" },
		{ "verblijfstitel.aanduiding.omschrijving", "103910" },
		{ "verblijfstitel.datumEinde", "103920" },
		{ "verblijfstitel.datumIngang", "103930" },
		{ "verblijfstitel.inOnderzoek", "108310, 108320" }, // By default geaccepteerd inonderzoek
		{ "verblijfstitel.inOnderzoek.aanduidingGegevensInOnderzoek", "108310" }, // By default geaccepteerd inonderzoek
		{ "verblijfstitel.inOnderzoek.datumIngangOnderzoek", "108320" }, // By default geaccepteerd inonderzoek
		//{ "verblijfstitel.", "108330" }, // Geen einddatum inonderzoek
		//{ "verblijfstitel.", "108410" }, // Geen indicatie onjuist
		//{ "verblijfstitel.", "108510" }, // Datum ingang geldigheid
		//{ "verblijfstitel.", "108610" }, // Datum opneming met betrekking tot persoon categorie

		{ "indicatieGezagMinderjarige", "113210" },
		{ "indicatieGezagMinderjarige.code", "113210" },
		{ "indicatieGezagMinderjarige.omschrijving", "113210" },
		{ "indicatieCurateleRegister", "113310" },
		//{ "", "118210" }, // gemeente van de gegevens over ouder aan het document ontleend zijn
		//{ "", "118220" }, // Datum van de ontlening van de gegevens over ouder
		//{ "", "118230" }, // Beschrijving van het document waaraan de gegevens over ouder ontleend zijn
		{ "gezagInOnderzoek", "118310, 118320" }, // By default geaccepteerd inonderzoek
		{ "gezagInOnderzoek.aanduidingGegevensInOnderzoek", "118310" }, // By default geaccepteerd inonderzoek
		{ "gezagInOnderzoek.datumIngangOnderzoek", "118320" }, // By default geaccepteerd inonderzoek
		//{ "", "118330" }, // Geen einddatum inonderzoek
		//{ "", "118410" }, // Geen indicatie onjuist
		//{ "", "118510" }, // Datum ingang geldigheid
		//{ "", "118610" }, // Datum opneming met betrekking tot persoon categorie

		// REISDOCUMENTEN NOT SUPPORTED IN ACTUEEL BRP
		//{ "", "123510" },
		//{ "", "123520" },
		//{ "", "123530" },
		//{ "", "123540" },
		//{ "", "123550" },
		//{ "", "123560" },
		//{ "", "123570" },
		//{ "", "123580" },
		//{ "", "123610" },
		////{ "", "123710" }, ?
		//{ "", "128210" },
		//{ "", "128220" },
		//{ "", "128230" },
		//{ "", "128310" },
		//{ "", "128320" },
		//{ "", "128330" },
		//{ "", "128410" }, // Indicatie onjuist Reisdocumenten
		//{ "", "128510" },
		//{ "", "128610" },

		{ "europeesKiesrecht", "133110, 133130" },
		{ "europeesKiesrecht.aanduiding", "133110" },
		{ "europeesKiesrecht.aanduiding.code", "133110" },
		{ "europeesKiesrecht.aanduiding.omschrijving", "133110" },
		//{ "europeesKiesrecht", "133120" }, // Datum verzoek of mdedeling europees kiesrecht
		{ "europeesKiesrecht.einddatumUitsluiting", "133130" },
		{ "uitsluitingKiesrecht", "133810, 133820" },
		{ "uitsluitingKiesrecht.uitgeslotenVanKiesrecht", "133810" },
		{ "uitsluitingKiesrecht.uitgeslotenVanKiesrecht.code", "133810" },
		{ "uitsluitingKiesrecht.uitgeslotenVanKiesrecht.omschrijving", "133810" },
		{ "uitsluitingKiesrecht.einddatum", "133820" },
		//{ "", "138210" }, // gemeente van de gegevens over ouder aan het document ontleend zijn
		//{ "", "138220" }, // Datum van de ontlening van de gegevens over ouder
		//{ "", "138230" } // Beschrijving van het document waaraan de gegevens over ouder ontleend zijn
	};

	private static readonly IDictionary<string, string> _fieldRubriekBeperktCategoryDictionary = new Dictionary<string, string>
	{
		{ "rni", "018810, 078810, 088810, 018820, 078820, 088820" },
		{ "rni.categorie", "018810, 078810, 088810, 018820, 078820, 088820" },
		{ "rni.deelnemer", "018810, 078810, 088810" },
		{ "rni.deelnemer.code", "018810, 078810, 088810" },
		{ "rni.deelnemer.omschrijving", "018810, 078810, 088810" },
		{ "rni.omschrijvingVerdrag", "018820, 078820, 088820" },

		{ "burgerservicenummer", "010120" },
		{ "naam", "010210, 010220, 010230, 010240" },
		{ "naam.voornamen", "010210" },
		{ "naam.adellijkeTitelPredicaat", "010220" },
		{ "naam.adellijkeTitelPredicaat.code", "010220" },
		{ "naam.adellijkeTitelPredicaat.omschrijving", "010220" },
		{ "naam.adellijkeTitelPredicaat.soort", "010220" },
		{ "naam.voorvoegsel", "010230" },
		{ "naam.geslachtsnaam", "010240" },
		{ "geboorte", "010310" },
		{ "geboorte.datum", "010310" },
		{ "geslacht", "010410" },
		{ "geslacht.code", "010410" },
		{ "geslacht.omschrijving", "010410" },
		//{ "", "012010" }, // Vorige a nummer
		//{ "", "012020" }, // Volgende a nummer
		//{ "", "018110" }, // Registergemeente akte
		//{ "", "018120" }, // Aktenummer van de akte
		//{ "", "018210" }, // gemeente van de gegevens over persoon aan het document ontleend zijn
		//{ "", "018220" }, // Datum van de ontlening van de gegevens over persoon
		//{ "", "018230" }, // Beschrijving van het document waaraan de gegevens over persoon ontleend zijn
		{ "persoonInOnderzoek", "018310, 018320" }, // By default geaccepteerd
		{ "persoonInOnderzoek.aanduidingGegevensInOnderzoek", "018310" }, // By default geaccepteerd
		{ "persoonInOnderzoek.datumIngangOnderzoek", "018320" }, // By default geaccepteerd
		//{ "persoonInOnderzoek", "018330" }, // Geen einddatum
		//{ "", "018410" }, // Geen indicatie onjuist
		//{ "", "018510" }, // Datum ingang geldigheid
		//{ "", "018610" }, // Datum opneming met betrekking tot persoon categorie
		//{ "", "018810" }, // RNI By default geaccepteerd
		//{ "", "018820" }, // RNI By default geaccepteerd

		{ "overlijden", "060810, 068310, 068320" },
		{ "overlijden.datum", "060810" },
		//{ "overlijden.", "068110" }, // Registergemeente akte
		//{ "overlijden.", "068120" }, // Aktenummer van de akte
		//{ "overlijden.", "068210" }, // gemeente van de gegevens over ouder aan het document ontleend zijn
		//{ "overlijden.", "068220" }, // Datum van de ontlening van de gegevens over ouder
		//{ "overlijden.", "068230" }, // Beschrijving van het document waaraan de gegevens over ouder ontleend zijn
		{ "overlijden.inOnderzoek", "068310, 068320" }, // By default geaccepteerd inonderzoek
		{ "overlijden.inOnderzoek.aanduidingGegevensInOnderzoek", "068310" }, // By default geaccepteerd inonderzoek
		{ "overlijden.inOnderzoek.datumIngangOnderzoek", "068320" }, // By default geaccepteerd inonderzoek
		//{ "overlijden.", "068330" }, // Geen einddatum inonderzoek
		//{ "overlijden.", "068410" }, // Geen indicatie onjuist
		//{ "overlijden.", "068510" }, // Datum ingang geldigheid
		//{ "overlijden.", "068610" }, // Datum opneming met betrekking tot persoon categorie
		//{ "", "068810" }, // RNI
		//{ "", "068820" }, // RNI

		//{ "", "076620" }, // Niet gebruikt datum ingang blokkering PL
		{ "opschortingBijhouding", "076710, 076720" },
		{ "opschortingBijhouding.datum", "076710" },
		{ "opschortingBijhouding.reden", "076720" },
		{ "opschortingBijhouding.reden.code", "076720" },
		{ "opschortingBijhouding.reden.omschrijving", "076720" },
		{ "datumEersteInschrijvingGBA", "076810" },
		//{ "", "076910" }, // Niet beschikbaar
		{ "geheimhoudingPersoonsgegevens", "077010" },
		{ "verificatie", "077110, 077120" }, // Verificatie
		{ "verificatie.datum", "077110" }, // Verificatie
		{ "verificatie.omschrijving", "077120" }, // Verificatie
		//{ "", "078010" }, // Versienummer
		//{ "", "078020" }, // Datumtijdstempel
		//{ "", "078710" }, // PK-gegevens volledig meegeconverteerd
		//{ "", "078810" }, // RNI
		//{ "", "078820" }, // RNI

		{ "gemeenteVanInschrijving", "080910" },
		{ "gemeenteVanInschrijving.code", "080910" },
		{ "gemeenteVanInschrijving.omschrijving", "080910" },
		{ "verblijfplaats", "080910, 081110, 081120, 081130, 081140, 081150, 081160, 081170, 081180, 081190, 081210, 081310, 081330, 081340, 081350, 088310, 088320" },
		//{ "verblijfplaats", "081020" }, // Gemeentedeel niet aanwezig
		{ "verblijfplaats.straat", "081110" },
		{ "verblijfplaats.huisnummer", "081120" },
		{ "verblijfplaats.huisletter", "081130" },
		{ "verblijfplaats.huisnummertoevoeging", "081140" },
		{ "verblijfplaats.aanduidingBijHuisnummer", "081150" },
		{ "verblijfplaats.aanduidingBijHuisnummer.code", "081150" },
		{ "verblijfplaats.aanduidingBijHuisnummer.omschrijving", "081150" },
		{ "verblijfplaats.postcode", "081160" },
		{ "verblijfplaats.woonplaats", "081170" },
		{ "verblijfplaats.locatiebeschrijving", "081210" },
		{ "verblijfplaats.land", "081310" },
		{ "verblijfplaats.land.code", "081310" },
		{ "verblijfplaats.land.omschrijving", "081310" },
		{ "verblijfplaats.regel1", "081330" },
		{ "verblijfplaats.regel2", "081340" },
		{ "verblijfplaats.regel3", "081350" },
		//{ "verblijfplaats.", "087210" }, // Omschrijving van de aangifte adreshouding
		//{ "verblijfplaats.", "087510" }, // Indicatie document
		{ "verblijfplaats.inOnderzoek", "088310, 088320" }, // By default geaccepteerd inonderzoek
		{ "verblijfplaats.inOnderzoek.aanduidingGegevensInOnderzoek", "088310" }, // By default geaccepteerd inonderzoek
		{ "verblijfplaats.inOnderzoek.datumIngangOnderzoek", "088320" }, // By default geaccepteerd inonderzoek
		//{ "verblijfplaats.", "088330" }, // Geen einddatum inonderzoek
		//{ "verblijfplaats.", "088410" }, // Geen indicatie onjuist
		//{ "verblijfplaats.", "088610" }, // Datum opneming met betrekking tot persoon categorie
		//{ "verblijfplaats.", "088810" }, // RNI
		//{ "verblijfplaats.", "088820" }, // RNI
		{ "verblijfplaats.adresseerbaarObjectIdentificatie", "081180" },
		{ "verblijfplaats.nummeraanduidingIdentificatie", "081190" },
	};

	public static List<string> ConvertModelParamsToRubrieken(PersonenQuery model)
	{
		return model switch
		{
			RaadpleegMetBurgerservicenummer raadpleegMetBurgerservicenummer => ConvertModelParamsToRubrieken(raadpleegMetBurgerservicenummer),
			ZoekMetGeslachtsnaamEnGeboortedatum zoekMetGeslachtsnaamEnGeboortedatum => ConvertModelParamsToRubrieken(zoekMetGeslachtsnaamEnGeboortedatum),
			ZoekMetNaamEnGemeenteVanInschrijving zoekMetNaamEnGemeenteVanInschrijving => ConvertModelParamsToRubrieken(zoekMetNaamEnGemeenteVanInschrijving),
			ZoekMetNummeraanduidingIdentificatie zoekMetNummeraanduidingIdentificatie => ConvertModelParamsToRubrieken(zoekMetNummeraanduidingIdentificatie),
			ZoekMetPostcodeEnHuisnummer zoekMetPostcodeEnHuisnummer => ConvertModelParamsToRubrieken(zoekMetPostcodeEnHuisnummer),
			ZoekMetStraatHuisnummerEnGemeenteVanInschrijving zoekMetStraatHuisnummerEnGemeenteVanInschrijving => ConvertModelParamsToRubrieken(zoekMetStraatHuisnummerEnGemeenteVanInschrijving),
			ZoekMetAdresseerbaarObjectIdentificatie zoekMetAdresseerbaarObjectIdentificatie => ConvertModelParamsToRubrieken(zoekMetAdresseerbaarObjectIdentificatie),
			_ => throw new CustomInvalidOperationException($"Onbekend type query: {model}"),
		};
	}

	private static List<string> ConvertModelParamsToRubrieken(RaadpleegMetBurgerservicenummer model)
	{
		var rubrieken = new List<string>();

		if (_fieldRubriekCategoryDictionary.ContainsKey(nameof(RaadpleegMetBurgerservicenummer.burgerservicenummer)))
		{
			rubrieken.Add(_fieldRubriekCategoryDictionary[nameof(RaadpleegMetBurgerservicenummer.burgerservicenummer)]);
		}
		if (!string.IsNullOrWhiteSpace(model.gemeenteVanInschrijving) && _fieldRubriekCategoryDictionary.ContainsKey(nameof(RaadpleegMetBurgerservicenummer.gemeenteVanInschrijving)))
		{
			rubrieken.Add(_fieldRubriekCategoryDictionary[nameof(RaadpleegMetBurgerservicenummer.gemeenteVanInschrijving)]);
		}

		return rubrieken;
	}

	private static List<string> ConvertModelParamsToRubrieken(ZoekMetGeslachtsnaamEnGeboortedatum model)
	{
		var rubrieken = new List<string>();

		if (_fieldRubriekBeperktCategoryDictionary.ContainsKey($"{nameof(GbaPersoon.Geboorte).ToLower()}.{nameof(ZoekMetGeslachtsnaamEnGeboortedatum.geboortedatum).Replace("geboorte", "")}"))
		{
			rubrieken.Add(_fieldRubriekBeperktCategoryDictionary[$"{nameof(GbaPersoon.Geboorte).ToLower()}.{nameof(ZoekMetGeslachtsnaamEnGeboortedatum.geboortedatum).Replace("geboorte", "")}"]);
		}
		if (_fieldRubriekBeperktCategoryDictionary.ContainsKey($"{nameof(GbaPersoon.Naam).ToLower()}.{nameof(ZoekMetGeslachtsnaamEnGeboortedatum.geslachtsnaam)}"))
		{
			rubrieken.Add(_fieldRubriekBeperktCategoryDictionary[$"{nameof(GbaPersoon.Naam).ToLower()}.{nameof(ZoekMetGeslachtsnaamEnGeboortedatum.geslachtsnaam)}"]);
		}
		if (!string.IsNullOrWhiteSpace(model.geslacht) && _fieldRubriekBeperktCategoryDictionary.ContainsKey(nameof(ZoekMetGeslachtsnaamEnGeboortedatum.geslacht)))
		{
			rubrieken.Add(_fieldRubriekBeperktCategoryDictionary[nameof(ZoekMetGeslachtsnaamEnGeboortedatum.geslacht)]);
		}
		if (!string.IsNullOrWhiteSpace(model.gemeenteVanInschrijving) && _fieldRubriekBeperktCategoryDictionary.ContainsKey(nameof(ZoekMetGeslachtsnaamEnGeboortedatum.gemeenteVanInschrijving)))
		{
			rubrieken.Add(_fieldRubriekBeperktCategoryDictionary[nameof(ZoekMetGeslachtsnaamEnGeboortedatum.gemeenteVanInschrijving)]);
		}
		if (!string.IsNullOrWhiteSpace(model.voorvoegsel) && _fieldRubriekBeperktCategoryDictionary.ContainsKey($"{nameof(GbaPersoon.Naam).ToLower()}.{nameof(ZoekMetGeslachtsnaamEnGeboortedatum.voorvoegsel)}"))
		{
			rubrieken.Add(_fieldRubriekBeperktCategoryDictionary[$"{nameof(GbaPersoon.Naam).ToLower()}.{nameof(ZoekMetGeslachtsnaamEnGeboortedatum.voorvoegsel)}"]);
		}
		if (!string.IsNullOrWhiteSpace(model.voornamen) && _fieldRubriekBeperktCategoryDictionary.ContainsKey($"{nameof(GbaPersoon.Naam).ToLower()}.{nameof(ZoekMetGeslachtsnaamEnGeboortedatum.voornamen)}"))
		{
			rubrieken.Add(_fieldRubriekBeperktCategoryDictionary[$"{nameof(GbaPersoon.Naam).ToLower()}.{nameof(ZoekMetGeslachtsnaamEnGeboortedatum.voornamen)}"]);
		}

		return rubrieken;
	}

	private static List<string> ConvertModelParamsToRubrieken(ZoekMetNaamEnGemeenteVanInschrijving model)
	{
		var rubrieken = new List<string>();

		if (_fieldRubriekBeperktCategoryDictionary.ContainsKey(nameof(ZoekMetNaamEnGemeenteVanInschrijving.gemeenteVanInschrijving)))
		{
			rubrieken.Add(_fieldRubriekBeperktCategoryDictionary[nameof(ZoekMetNaamEnGemeenteVanInschrijving.gemeenteVanInschrijving)]);
		}
		if (_fieldRubriekBeperktCategoryDictionary.ContainsKey($"{nameof(GbaPersoon.Naam).ToLower()}.{nameof(ZoekMetNaamEnGemeenteVanInschrijving.geslachtsnaam)}"))
		{
			rubrieken.Add(_fieldRubriekBeperktCategoryDictionary[$"{nameof(GbaPersoon.Naam).ToLower()}.{nameof(ZoekMetNaamEnGemeenteVanInschrijving.geslachtsnaam)}"]);
		}
		if (_fieldRubriekBeperktCategoryDictionary.ContainsKey($"{nameof(GbaPersoon.Naam).ToLower()}.{nameof(ZoekMetNaamEnGemeenteVanInschrijving.voornamen)}"))
		{
			rubrieken.Add(_fieldRubriekBeperktCategoryDictionary[$"{nameof(GbaPersoon.Naam).ToLower()}.{nameof(ZoekMetNaamEnGemeenteVanInschrijving.voornamen)}"]);
		}
		if (!string.IsNullOrWhiteSpace(model.geslacht) && _fieldRubriekBeperktCategoryDictionary.ContainsKey(nameof(ZoekMetNaamEnGemeenteVanInschrijving.geslacht)))
		{
			rubrieken.Add(_fieldRubriekBeperktCategoryDictionary[nameof(ZoekMetNaamEnGemeenteVanInschrijving.geslacht)]);
		}
		if (!string.IsNullOrWhiteSpace(model.voorvoegsel) && _fieldRubriekBeperktCategoryDictionary.ContainsKey($"{nameof(GbaPersoon.Naam).ToLower()}.{nameof(ZoekMetNaamEnGemeenteVanInschrijving.voorvoegsel)}"))
		{
			rubrieken.Add(_fieldRubriekBeperktCategoryDictionary[$"{nameof(GbaPersoon.Naam).ToLower()}.{nameof(ZoekMetNaamEnGemeenteVanInschrijving.voorvoegsel)}"]);
		}

		return rubrieken;
	}

	private static List<string> ConvertModelParamsToRubrieken(ZoekMetPostcodeEnHuisnummer model)
	{
		var rubrieken = new List<string>();

		if (_fieldRubriekBeperktCategoryDictionary.ContainsKey($"{nameof(GbaPersoon.Verblijfplaats).ToLower()}.{nameof(ZoekMetPostcodeEnHuisnummer.huisnummer)}"))
		{
			rubrieken.Add(_fieldRubriekBeperktCategoryDictionary[$"{nameof(GbaPersoon.Verblijfplaats).ToLower()}.{nameof(ZoekMetPostcodeEnHuisnummer.huisnummer)}"]);
		}
		if (_fieldRubriekBeperktCategoryDictionary.ContainsKey($"{nameof(GbaPersoon.Verblijfplaats).ToLower()}.{nameof(ZoekMetPostcodeEnHuisnummer.postcode)}"))
		{
			rubrieken.Add(_fieldRubriekBeperktCategoryDictionary[$"{nameof(GbaPersoon.Verblijfplaats).ToLower()}.{nameof(ZoekMetPostcodeEnHuisnummer.postcode)}"]);
		}
		if (!string.IsNullOrWhiteSpace(model.gemeenteVanInschrijving) && _fieldRubriekBeperktCategoryDictionary.ContainsKey(nameof(ZoekMetPostcodeEnHuisnummer.gemeenteVanInschrijving)))
		{
			rubrieken.Add(_fieldRubriekBeperktCategoryDictionary[nameof(ZoekMetPostcodeEnHuisnummer.gemeenteVanInschrijving)]);
		}
		if (!string.IsNullOrWhiteSpace(model.huisletter) && _fieldRubriekBeperktCategoryDictionary.ContainsKey($"{nameof(GbaPersoon.Verblijfplaats).ToLower()}.{nameof(ZoekMetPostcodeEnHuisnummer.huisletter)}"))
		{
			rubrieken.Add(_fieldRubriekBeperktCategoryDictionary[$"{nameof(GbaPersoon.Verblijfplaats).ToLower()}.{nameof(ZoekMetPostcodeEnHuisnummer.huisletter)}"]);
		}
		if (!string.IsNullOrWhiteSpace(model.huisnummertoevoeging) && _fieldRubriekBeperktCategoryDictionary.ContainsKey($"{nameof(GbaPersoon.Verblijfplaats).ToLower()}.{nameof(ZoekMetPostcodeEnHuisnummer.huisnummertoevoeging)}"))
		{
			rubrieken.Add(_fieldRubriekBeperktCategoryDictionary[$"{nameof(GbaPersoon.Verblijfplaats).ToLower()}.{nameof(ZoekMetPostcodeEnHuisnummer.huisnummertoevoeging)}"]);
		}

		return rubrieken;
	}

	private static List<string> ConvertModelParamsToRubrieken(ZoekMetStraatHuisnummerEnGemeenteVanInschrijving model)
	{
		var rubrieken = new List<string>();

		if (_fieldRubriekBeperktCategoryDictionary.ContainsKey(nameof(ZoekMetStraatHuisnummerEnGemeenteVanInschrijving.gemeenteVanInschrijving)))
		{
			rubrieken.Add(_fieldRubriekBeperktCategoryDictionary[nameof(ZoekMetStraatHuisnummerEnGemeenteVanInschrijving.gemeenteVanInschrijving)]);
		}
		if (_fieldRubriekBeperktCategoryDictionary.ContainsKey($"{nameof(GbaPersoon.Verblijfplaats).ToLower()}.{nameof(ZoekMetStraatHuisnummerEnGemeenteVanInschrijving.huisnummer)}"))
		{
			rubrieken.Add(_fieldRubriekBeperktCategoryDictionary[$"{nameof(GbaPersoon.Verblijfplaats).ToLower()}.{nameof(ZoekMetStraatHuisnummerEnGemeenteVanInschrijving.huisnummer)}"]);
		}
		if (_fieldRubriekBeperktCategoryDictionary.ContainsKey($"{nameof(GbaPersoon.Verblijfplaats).ToLower()}.{nameof(ZoekMetStraatHuisnummerEnGemeenteVanInschrijving.straat)}"))
		{
			rubrieken.Add(_fieldRubriekBeperktCategoryDictionary[$"{nameof(GbaPersoon.Verblijfplaats).ToLower()}.{nameof(ZoekMetStraatHuisnummerEnGemeenteVanInschrijving.straat)}"]);
		}
		if (!string.IsNullOrWhiteSpace(model.huisletter) && _fieldRubriekBeperktCategoryDictionary.ContainsKey($"{nameof(GbaPersoon.Verblijfplaats).ToLower()}.{nameof(ZoekMetStraatHuisnummerEnGemeenteVanInschrijving.huisletter)}"))
		{
			rubrieken.Add(_fieldRubriekBeperktCategoryDictionary[$"{nameof(GbaPersoon.Verblijfplaats).ToLower()}.{nameof(ZoekMetStraatHuisnummerEnGemeenteVanInschrijving.huisletter)}"]);
		}
		if (!string.IsNullOrWhiteSpace(model.huisnummertoevoeging) && _fieldRubriekBeperktCategoryDictionary.ContainsKey($"{nameof(GbaPersoon.Verblijfplaats).ToLower()}.{nameof(ZoekMetStraatHuisnummerEnGemeenteVanInschrijving.huisnummertoevoeging)}"))
		{
			rubrieken.Add(_fieldRubriekBeperktCategoryDictionary[$"{nameof(GbaPersoon.Verblijfplaats).ToLower()}.{nameof(ZoekMetStraatHuisnummerEnGemeenteVanInschrijving.huisnummertoevoeging)}"]);
		}

		return rubrieken;
	}

	private static List<string> ConvertModelParamsToRubrieken(ZoekMetNummeraanduidingIdentificatie model)
	{
		var rubrieken = new List<string>();

		if (_fieldRubriekBeperktCategoryDictionary.ContainsKey($"{nameof(GbaPersoon.Verblijfplaats).ToLower()}.{nameof(ZoekMetNummeraanduidingIdentificatie.nummeraanduidingIdentificatie)}"))
		{
			rubrieken.Add(_fieldRubriekBeperktCategoryDictionary[$"{nameof(GbaPersoon.Verblijfplaats).ToLower()}.{nameof(ZoekMetNummeraanduidingIdentificatie.nummeraanduidingIdentificatie)}"]);
		}
		if (!string.IsNullOrWhiteSpace(model.gemeenteVanInschrijving) && _fieldRubriekBeperktCategoryDictionary.ContainsKey(nameof(ZoekMetNummeraanduidingIdentificatie.gemeenteVanInschrijving)))
		{
			rubrieken.Add(_fieldRubriekBeperktCategoryDictionary[nameof(ZoekMetNummeraanduidingIdentificatie.gemeenteVanInschrijving)]);
		}
		return rubrieken;
	}

	private static List<string> ConvertModelParamsToRubrieken(ZoekMetAdresseerbaarObjectIdentificatie model)
	{
		var rubrieken = new List<string>();

		if (_fieldRubriekBeperktCategoryDictionary.ContainsKey($"{nameof(GbaPersoon.Verblijfplaats).ToLower()}.{nameof(ZoekMetAdresseerbaarObjectIdentificatie.adresseerbaarObjectIdentificatie)}"))
		{
			rubrieken.Add(_fieldRubriekBeperktCategoryDictionary[$"{nameof(GbaPersoon.Verblijfplaats).ToLower()}.{nameof(ZoekMetAdresseerbaarObjectIdentificatie.adresseerbaarObjectIdentificatie)}"]);
		}
		if (!string.IsNullOrWhiteSpace(model.gemeenteVanInschrijving) && _fieldRubriekBeperktCategoryDictionary.ContainsKey(nameof(ZoekMetAdresseerbaarObjectIdentificatie.gemeenteVanInschrijving)))
		{
			rubrieken.Add(_fieldRubriekBeperktCategoryDictionary[nameof(ZoekMetAdresseerbaarObjectIdentificatie.gemeenteVanInschrijving)]);
		}
		return rubrieken;
	}

	public static List<(string field, string rubriek)> ConvertFieldsToRubriekCategory(List<string> fields, bool beperktFields)
	{
		var fieldsDictionary = beperktFields ? _fieldRubriekBeperktCategoryDictionary : _fieldRubriekCategoryDictionary;
		// String join and Split needed as some fields may result in multiple fields because of Haal Centraal logic.
		// These are already & separated so this is just a step to make every field a single item in the list.
		var correctedFields = new List<string>(fields.Where(field => !field.Contains("ouderAanduiding") && !(field.Contains("gezag", StringComparison.CurrentCultureIgnoreCase) && !field.StartsWith("indicatieGezagMinderjarige"))));
		fields.ForEach(field =>
		{
			if (field.Contains("&"))
			{
				var splitFields = field.Split("&").ToList();
				splitFields.ForEach(splitField => correctedFields.Add(splitField));
				correctedFields.RemoveAt(correctedFields.IndexOf(field));
			}
		});
		var unknownFields = correctedFields.Where(field => !fieldsDictionary.ContainsKey(field));
		if (unknownFields?.Any() == true)
		{
			throw new AuthorizationException($"No translation available for field: {string.Join(", ", unknownFields)}.");
		}

		// String join and Split needed as some fields may result in multiple rubrieken of a category.
		// These are already comma separated so this is just a step to make every rubriek a single item in the list.
		var fieldRubrieken = correctedFields.ConvertAll(field => (field, rubriek: fieldsDictionary[field]));
		var correctedRubrieken = new List<(string field, string rubriek)>(fieldRubrieken);
		fieldRubrieken.ForEach(fieldRubriek =>
		{
			if (fieldRubriek.rubriek.Contains(", "))
			{
				var rubrieken = fieldRubriek.rubriek.Split(", ").ToList();
				rubrieken.ForEach(rubriek => correctedRubrieken.Add((fieldRubriek.field, rubriek)));
				correctedRubrieken.RemoveAt(correctedRubrieken.IndexOf(fieldRubriek));
			}
		});

		if (fields.Any(field => field.Contains("ouderAanduiding")))
		{
			correctedRubrieken.Add(("ouders.ouderAanduiding", ""));
		}

		return correctedRubrieken;
	}

	public static List<string> ConvertHaalCentraalBrpPropsToRubriekCategory(GbaPersoon persoon)
	{
		var deliveredRubrieken = new List<string>();
		var categoryCode = "01";
		foreach (var propertyName in ObjectHelper.GetPropertyNames<GbaPersoon>())
		{
			switch (propertyName)
			{
				case nameof(GbaPersoon.Rni):
					if (persoon.Rni?.Any() == true)
					{
						deliveredRubrieken.AddRange(ConvertRniToRubriekCategory(persoon.Rni));
					}
					break;
				case nameof(GbaPersoon.ANummer):
					if (!string.IsNullOrWhiteSpace(persoon.ANummer))
					{
						deliveredRubrieken.Add("010110");
					}
					break;
				case nameof(GbaPersoon.Burgerservicenummer):
					if (!string.IsNullOrWhiteSpace(persoon.Burgerservicenummer))
					{
						deliveredRubrieken.Add("010120");
					}
					break;
				case nameof(GbaPersoon.Naam):
					if (persoon.Naam != null)
					{
						deliveredRubrieken.AddRange(ConvertNaamToRubriekCategory(persoon.Naam));
					}
					break;
				case nameof(GbaPersoon.Geboorte):
					if (persoon.Geboorte != null)
					{
						deliveredRubrieken.AddRange(ConvertGeboorteToRubriekCategory(persoon.Geboorte, categoryCode));
					}
					break;
				case nameof(GbaPersoon.Geslacht):
					if (persoon.Geslacht != null && (!string.IsNullOrWhiteSpace(persoon.Geslacht.Code) || !string.IsNullOrWhiteSpace(persoon.Geslacht.Omschrijving)))
					{
						deliveredRubrieken.Add("010410");
					}
					break;
				case nameof(GbaPersoon.PersoonInOnderzoek):
					if (persoon.PersoonInOnderzoek != null)
					{
						if (!string.IsNullOrWhiteSpace(persoon.PersoonInOnderzoek.AanduidingGegevensInOnderzoek))
						{
							deliveredRubrieken.Add("018310");
						}
						if (!string.IsNullOrWhiteSpace(persoon.PersoonInOnderzoek.DatumIngangOnderzoek))
						{
							deliveredRubrieken.Add("018320");
						}
					}
					break; // By default geaccepteerd
				case nameof(GbaPersoon.Ouders):
					if (persoon.Ouders?.Any() == true)
					{
						deliveredRubrieken.AddRange(ConvertOudersToRubriekCategory(persoon.Ouders));
					}
					break;
				case nameof(GbaPersoon.Nationaliteiten):
					if (persoon.Nationaliteiten?.Any() == true)
					{
						deliveredRubrieken.AddRange(ConvertNationaliteitenToRubriekCategory(persoon.Nationaliteiten));
					}
					break;
				case nameof(GbaPersoon.Partners):
					if (persoon.Partners?.Any() == true)
					{
						deliveredRubrieken.AddRange(ConvertPartnersToRubriekCategory(persoon.Partners));
					}
					break;
				case nameof(GbaPersoon.Overlijden):
					if (persoon.Overlijden != null)
					{
						deliveredRubrieken.AddRange(ConvertOverlijdenToRubriekCategory(persoon.Overlijden));
					}
					break;
				case nameof(GbaPersoon.OpschortingBijhouding):
					if (persoon.OpschortingBijhouding != null)
					{
						deliveredRubrieken.AddRange(ConvertOpschortingBijhoudingToRubriekCategory(persoon.OpschortingBijhouding));
					}
					break;
				case nameof(GbaPersoon.DatumEersteInschrijvingGBA):
					if (!string.IsNullOrWhiteSpace(persoon.DatumEersteInschrijvingGBA))
					{
						deliveredRubrieken.Add("076810");
					}
					break;
				case nameof(GbaPersoon.GeheimhoudingPersoonsgegevens):
					if (persoon.GeheimhoudingPersoonsgegevens.HasValue)
					{
						deliveredRubrieken.Add("077010");
					}
					break;
				case nameof(GbaPersoon.GemeenteVanInschrijving):
					if (persoon.GemeenteVanInschrijving != null &&
						(!string.IsNullOrWhiteSpace(persoon.GemeenteVanInschrijving?.Code) || !string.IsNullOrWhiteSpace(persoon.GemeenteVanInschrijving?.Omschrijving)))
					{
						deliveredRubrieken.Add("080910");
					}
					break;
				case nameof(GbaPersoon.DatumInschrijvingInGemeente):
					if (!string.IsNullOrWhiteSpace(persoon.DatumInschrijvingInGemeente))
					{
						deliveredRubrieken.Add("080920");
					}
					break;
				case nameof(GbaPersoon.Verblijfplaats):
					if (persoon.Verblijfplaats != null)
					{
						deliveredRubrieken.AddRange(ConvertVerblijfplaatsToRubriekCategory(persoon.Verblijfplaats));
					}
					break;
				case nameof(GbaPersoon.Immigratie):
					if (persoon.Immigratie != null)
					{
						deliveredRubrieken.AddRange(ConvertImmigratieToRubriekCategory(persoon.Immigratie));
					}
					break;
				case nameof(GbaPersoon.Kinderen):
					if (persoon.Kinderen?.Any() == true)
					{
						deliveredRubrieken.AddRange(ConvertKinderenToRubriekCategory(persoon.Kinderen));
					}
					break;
				case nameof(GbaPersoon.Verblijfstitel):
					if (persoon.Verblijfstitel != null)
					{
						deliveredRubrieken.AddRange(ConvertVerblijfstitelToRubriekCategory(persoon.Verblijfstitel));
					}
					break;
				case nameof(GbaPersoon.IndicatieGezagMinderjarige):
					if (persoon.IndicatieGezagMinderjarige != null &&
						(!string.IsNullOrWhiteSpace(persoon.IndicatieGezagMinderjarige?.Code) || !string.IsNullOrWhiteSpace(persoon.IndicatieGezagMinderjarige?.Omschrijving)))
					{
						deliveredRubrieken.Add("113210");
					}
					break;
				case nameof(GbaPersoon.IndicatieCurateleRegister):
					if (persoon.IndicatieCurateleRegister.HasValue)
					{
						deliveredRubrieken.Add("113310");
					}
					break;
				case nameof(GbaPersoon.GezagInOnderzoek):
					if (persoon.GezagInOnderzoek != null)
					{
						if (!string.IsNullOrWhiteSpace(persoon.GezagInOnderzoek.AanduidingGegevensInOnderzoek))
						{
							deliveredRubrieken.Add("118310");
						}
						if (!string.IsNullOrWhiteSpace(persoon.GezagInOnderzoek.DatumIngangOnderzoek))
						{
							deliveredRubrieken.Add("118320");
						}
					}
					break; // By default geaccepteerd inonderzoek
				case nameof(GbaPersoon.EuropeesKiesrecht):
					if (persoon.EuropeesKiesrecht != null)
					{
						deliveredRubrieken.AddRange(ConvertEuropeesKiesrechtToRubriekCategory(persoon.EuropeesKiesrecht));
					}
					break;
				case nameof(GbaPersoon.UitsluitingKiesrecht):
					if (persoon.UitsluitingKiesrecht != null)
					{
						deliveredRubrieken.AddRange(ConvertUitsluitingKiesrechtToRubriekCategory(persoon.UitsluitingKiesrecht));
					}
					break;
				case nameof(GbaPersoon.Verificatie):
					if (persoon.Verificatie != null)
					{
						deliveredRubrieken.AddRange(ConvertVerificatieToRubriekCategory(persoon.Verificatie));
					}
					break;
				default:
					throw new CustomNotImplementedException($"Mapping not implemented for {nameof(GbaPersoon)} property {propertyName}");
			}
		}
		return deliveredRubrieken.OrderBy(rubriek => rubriek.Substring(0)).ToList();
	}

	private static List<string> ConvertRniToRubriekCategory(List<RniDeelnemer> rniDeelnemers)
	{
		var deliveredRubrieken = new List<string>();
		foreach (var propertyName in ObjectHelper.GetPropertyNames<RniDeelnemer>())
		{
			rniDeelnemers.ForEach(rniDeelnemer =>
			{
				switch (propertyName)
				{
					case nameof(RniDeelnemer.Categorie):
						var rniCategoryRubrieken = "018810, 048810, 068810, 078810, 088810, 018820, 048820, 068820, 078820, 088820".Split(',').Except(deliveredRubrieken);
						if (!string.IsNullOrWhiteSpace(rniDeelnemer.Categorie) && rniCategoryRubrieken.Any())
						{
							deliveredRubrieken.AddRange(rniCategoryRubrieken);
						}
						break;
					case nameof(RniDeelnemer.Deelnemer):
						var rniDeelnemerRubrieken = "018810, 048810, 068810, 078810, 088810".Split(',').Except(deliveredRubrieken);
						if (rniDeelnemer.Deelnemer != null && (!string.IsNullOrWhiteSpace(rniDeelnemer.Deelnemer.Code) || !string.IsNullOrWhiteSpace(rniDeelnemer.Deelnemer.Omschrijving)) && rniDeelnemerRubrieken.Any())
						{
							deliveredRubrieken.AddRange(rniDeelnemerRubrieken);
						}
						break;
					case nameof(RniDeelnemer.OmschrijvingVerdrag):
						var rniOmschrijvingVerdragRubrieken = "018820, 048820, 068820, 078820, 088820".Split(',').Except(deliveredRubrieken);
						if (!string.IsNullOrWhiteSpace(rniDeelnemer.OmschrijvingVerdrag) && rniOmschrijvingVerdragRubrieken.Any())
						{
							deliveredRubrieken.AddRange(rniOmschrijvingVerdragRubrieken);
						}
						break;
					default:
						throw new CustomNotImplementedException($"Mapping not implemented for {nameof(RniDeelnemer)} property {propertyName}");
				}
			});
		}

		return deliveredRubrieken;
	}

	private static List<string> ConvertNaamBasisToRubriekCategory(NaamBasis naam, string categoryCode)
	{
		var deliveredRubrieken = new List<string>();
		foreach (var propertyName in ObjectHelper.GetPropertyNames<NaamBasis>())
		{
			switch (propertyName)
			{
				case nameof(NaamBasis.Voornamen):
					if (!string.IsNullOrWhiteSpace(naam.Voornamen))
					{
						deliveredRubrieken.Add($"{categoryCode}0210");
					}
					break;
				case nameof(NaamBasis.AdellijkeTitelPredicaat):
					if (naam.AdellijkeTitelPredicaat != null
						&& (!string.IsNullOrWhiteSpace(naam.AdellijkeTitelPredicaat?.Code) || !string.IsNullOrWhiteSpace(naam.AdellijkeTitelPredicaat?.Omschrijving) || naam.AdellijkeTitelPredicaat?.Soort != null))
					{
						deliveredRubrieken.Add($"{categoryCode}0220");
					}
					break;
				case nameof(NaamBasis.Voorvoegsel):
					if (!string.IsNullOrWhiteSpace(naam.Voorvoegsel))
					{
						deliveredRubrieken.Add($"{categoryCode}0230");
					}
					break;
				case nameof(NaamBasis.Geslachtsnaam):
					if (!string.IsNullOrWhiteSpace(naam.Geslachtsnaam))
					{
						deliveredRubrieken.Add($"{categoryCode}0240");
					}
					break;
				default:
					throw new CustomNotImplementedException($"Mapping not implemented for {nameof(NaamBasis)} property {propertyName}");
			}
		}

		return deliveredRubrieken;
	}

	private static List<string> ConvertNaamToRubriekCategory(GbaNaamPersoon naam)
	{
		var deliveredRubrieken = ConvertNaamBasisToRubriekCategory(naam, "01");
		foreach (var propertyName in ObjectHelper.GetPropertyNames<GbaNaamPersoon>())
		{
			switch (propertyName)
			{
				// Already handled in ConvertNaamBasisToRubriekCategory
				case nameof(GbaNaamPersoon.Voornamen):
				case nameof(GbaNaamPersoon.AdellijkeTitelPredicaat):
				case nameof(GbaNaamPersoon.Voorvoegsel):
				case nameof(GbaNaamPersoon.Geslachtsnaam):
					break;
				case nameof(GbaNaamPersoon.AanduidingNaamgebruik):
					if (naam.AanduidingNaamgebruik != null
						&& (!string.IsNullOrWhiteSpace(naam.AanduidingNaamgebruik?.Code) || !string.IsNullOrWhiteSpace(naam.AanduidingNaamgebruik?.Omschrijving)))
					{
						deliveredRubrieken.Add("016110");
					}
					break;
				default:
					throw new CustomNotImplementedException($"Mapping not implemented for {nameof(GbaNaamPersoon)} property {propertyName}");
			}
		}

		return deliveredRubrieken;
	}

	private static List<string> ConvertGeboorteBeperktToRubriekCategory(GeboorteBasis geboorte, string categoryCode)
	{
		var deliveredRubrieken = new List<string>();
		foreach (var propertyName in ObjectHelper.GetPropertyNames<GeboorteBasis>())
		{
			switch (propertyName)
			{
				case nameof(GeboorteBasis.Datum):
					if (!string.IsNullOrWhiteSpace(geboorte.Datum))
					{
						deliveredRubrieken.Add($"{categoryCode}0310");
					}
					break;
				case nameof(GeboorteBasis.DatumJaar):
				case nameof(GeboorteBasis.DatumMaand):
				case nameof(GeboorteBasis.DatumDag):
					break;
				default:
					throw new CustomNotImplementedException($"Mapping not implemented for {nameof(GeboorteBasis)} property {propertyName}");
			}
		}

		return deliveredRubrieken;
	}

	private static List<string> ConvertGeboorteToRubriekCategory(GbaGeboorte geboorte, string categoryCode)
	{
		var deliveredRubrieken = ConvertGeboorteBeperktToRubriekCategory(geboorte, categoryCode);
		foreach (var propertyName in ObjectHelper.GetPropertyNames<GbaGeboorte>())
		{
			switch (propertyName)
			{
				case nameof(GbaGeboorte.Plaats):
					if (geboorte.Plaats != null &&
						(!string.IsNullOrWhiteSpace(geboorte.Plaats?.Code) || !string.IsNullOrWhiteSpace(geboorte.Plaats?.Omschrijving)))
					{
						deliveredRubrieken.Add($"{categoryCode}0320");
					}
					break;
				case nameof(GbaGeboorte.Land):
					if (geboorte.Land != null &&
						(!string.IsNullOrWhiteSpace(geboorte.Land?.Code) || !string.IsNullOrWhiteSpace(geboorte.Land?.Omschrijving)))
					{
						deliveredRubrieken.Add($"{categoryCode}0330");
					}
					break;
				// Taken care of in ConvertGeboorteBeperktToRubriekCategory
				case nameof(GbaGeboorte.Datum):
				case nameof(GbaGeboorte.DatumJaar):
				case nameof(GbaGeboorte.DatumMaand):
				case nameof(GbaGeboorte.DatumDag):
					break;
				default:
					throw new CustomNotImplementedException($"Mapping not implemented for {nameof(GbaGeboorte)} property {propertyName}");
			}
		}

		return deliveredRubrieken;
	}

	private static List<string> ConvertOudersToRubriekCategory(List<GbaOuder> ouders)
	{
		var deliveredRubrieken = new List<string>();
		foreach (var propertyName in ObjectHelper.GetPropertyNames<GbaOuder>())
		{
			ouders.ForEach(ouder =>
			{
				var ouderCategoryCode = ouder.OuderAanduiding?.Equals("1") == true ? "02" : "03";
				switch (propertyName)
				{
					case nameof(GbaOuder.Burgerservicenummer):
						if (!string.IsNullOrWhiteSpace(ouder.Burgerservicenummer))
						{
							deliveredRubrieken.Add($"{ouderCategoryCode}0120");
						}
						break;
					case nameof(GbaOuder.Naam):
						if (ouder.Naam != null)
						{
							deliveredRubrieken.AddRange(ConvertNaamBasisToRubriekCategory(ouder.Naam, ouderCategoryCode));
						}
						break;
					case nameof(GbaOuder.Geboorte):
						if (ouder.Geboorte != null)
						{
							deliveredRubrieken.AddRange(ConvertGeboorteToRubriekCategory(ouder.Geboorte, ouderCategoryCode));
						}
						break;
					case nameof(GbaOuder.Geslacht):
						if (ouder.Geslacht != null && (!string.IsNullOrWhiteSpace(ouder.Geslacht.Code) || !string.IsNullOrWhiteSpace(ouder.Geslacht.Omschrijving)))
						{
							deliveredRubrieken.Add($"{ouderCategoryCode}0410");
						}
						break;
					case nameof(GbaOuder.DatumIngangFamilierechtelijkeBetrekking):
						if (!string.IsNullOrWhiteSpace(ouder.DatumIngangFamilierechtelijkeBetrekking))
						{
							deliveredRubrieken.Add($"{ouderCategoryCode}6210");
						}
						break;
					case nameof(GbaOuder.InOnderzoek):
						if (ouder.InOnderzoek != null)
						{
							if (!string.IsNullOrWhiteSpace(ouder.InOnderzoek.AanduidingGegevensInOnderzoek))
							{
								deliveredRubrieken.Add($"{ouderCategoryCode}8310");
							}
							if (!string.IsNullOrWhiteSpace(ouder.InOnderzoek.DatumIngangOnderzoek))
							{
								deliveredRubrieken.Add($"{ouderCategoryCode}8320");
							}
						}
						break; // By default geaccepteerd inonderzoek
					case nameof(GbaOuder.OuderAanduiding):
						break;
					default:
						throw new CustomNotImplementedException($"Mapping not implemented for {nameof(GbaOuder)} property {propertyName}");
				}
			});
		}

		return deliveredRubrieken;
	}

	private static List<string> ConvertNationaliteitenToRubriekCategory(List<GbaNationaliteit> nationaliteiten)
	{
		var deliveredRubrieken = new List<string>();
		foreach (var propertyName in ObjectHelper.GetPropertyNames<GbaNationaliteit>())
		{
			nationaliteiten.ForEach(nationaliteit =>
			{
				switch (propertyName)
				{
					case nameof(GbaNationaliteit.Nationaliteit):
						if (nationaliteit.Nationaliteit != null && (!string.IsNullOrWhiteSpace(nationaliteit.Nationaliteit.Code) || !string.IsNullOrWhiteSpace(nationaliteit.Nationaliteit.Omschrijving)))
						{
							deliveredRubrieken.Add("040510");
						}
						break;
					case nameof(GbaNationaliteit.RedenOpname):
						if (nationaliteit.RedenOpname != null && (!string.IsNullOrWhiteSpace(nationaliteit.RedenOpname.Code) || !string.IsNullOrWhiteSpace(nationaliteit.RedenOpname.Omschrijving)))
						{
							deliveredRubrieken.Add("040510");
						}
						break;
					case nameof(GbaNationaliteit.AanduidingBijzonderNederlanderschap):
						if (!string.IsNullOrWhiteSpace(nationaliteit.AanduidingBijzonderNederlanderschap))
						{
							deliveredRubrieken.Add("046510");
						}
						break;
					case nameof(GbaNationaliteit.InOnderzoek):
						if (nationaliteit.InOnderzoek != null)
						{
							if (!string.IsNullOrWhiteSpace(nationaliteit.InOnderzoek.AanduidingGegevensInOnderzoek))
							{
								deliveredRubrieken.Add("048310");
							}
							if (!string.IsNullOrWhiteSpace(nationaliteit.InOnderzoek.DatumIngangOnderzoek))
							{
								deliveredRubrieken.Add("048320");
							}
						}
						break; // By default geaccepteerd inonderzoek
					case nameof(GbaNationaliteit.DatumIngangGeldigheid):
						if (!string.IsNullOrWhiteSpace(nationaliteit.DatumIngangGeldigheid))
						{
							deliveredRubrieken.Add("048510");
						}
						break; // Datum ingang geldigheid
					case nameof(GbaNationaliteit._datumOpneming):
						break;
					default:
						throw new CustomNotImplementedException($"Mapping not implemented for {nameof(GbaNationaliteit)} property {propertyName}");
				}
			});
		}

		return deliveredRubrieken;
	}

	private static List<string> ConvertPartnersToRubriekCategory(List<GbaPartner> partners)
	{
		var categoryCode = "05";
		var deliveredRubrieken = new List<string>();
		foreach (var propertyName in ObjectHelper.GetPropertyNames<GbaPartner>())
		{
			partners.ForEach(partner =>
			{
				switch (propertyName)
				{
					case nameof(GbaPartner.Burgerservicenummer):
						if (!string.IsNullOrWhiteSpace(partner.Burgerservicenummer))
						{
							deliveredRubrieken.Add("050120");
						}
						break;
					case nameof(GbaPartner.Naam):
						if (partner.Naam != null)
						{
							deliveredRubrieken.AddRange(ConvertNaamBasisToRubriekCategory(partner.Naam, categoryCode));
						}
						break;
					case nameof(GbaPartner.Geboorte):
						if (partner.Geboorte != null)
						{
							deliveredRubrieken.AddRange(ConvertGeboorteToRubriekCategory(partner.Geboorte, categoryCode));
						}
						break;
					case nameof(GbaPartner.Geslacht):
						if (partner.Geslacht != null && (!string.IsNullOrWhiteSpace(partner.Geslacht.Code) || !string.IsNullOrWhiteSpace(partner.Geslacht.Omschrijving)))
						{
							deliveredRubrieken.Add("050410");
						}
						break;
					case nameof(GbaPartner.AangaanHuwelijkPartnerschap):
						if (partner.AangaanHuwelijkPartnerschap != null)
						{
							deliveredRubrieken.AddRange(ConvertAangaanHuwelijkPartnerschapToRubriekCategory(partner.AangaanHuwelijkPartnerschap));
						}
						break;
					case nameof(GbaPartner.OntbindingHuwelijkPartnerschap):
						if (!string.IsNullOrWhiteSpace(partner.OntbindingHuwelijkPartnerschap?.Datum))
						{
							deliveredRubrieken.Add("050710");
						}
						break;
					case nameof(GbaPartner.SoortVerbintenis):
						if (partner.SoortVerbintenis != null && (!string.IsNullOrWhiteSpace(partner.SoortVerbintenis.Code) || !string.IsNullOrWhiteSpace(partner.SoortVerbintenis.Omschrijving)))
						{
							deliveredRubrieken.Add("051510");
						}
						break;
					case nameof(GbaPartner.InOnderzoek):
						if (partner.InOnderzoek != null)
						{
							if (!string.IsNullOrWhiteSpace(partner.InOnderzoek.AanduidingGegevensInOnderzoek))
							{
								deliveredRubrieken.Add("058310");
							}
							if (!string.IsNullOrWhiteSpace(partner.InOnderzoek.DatumIngangOnderzoek))
							{
								deliveredRubrieken.Add("058320");
							}
						}
						break; // By default geaccepteerd inonderzoek
					default:
						throw new CustomNotImplementedException($"Mapping not implemented for {nameof(GbaPartner)} property {propertyName}");
				}
			});
		}

		return deliveredRubrieken;
	}

	private static List<string> ConvertAangaanHuwelijkPartnerschapToRubriekCategory(GbaAangaanHuwelijkPartnerschap aangaanHuwelijkPartnerschap)
	{
		var deliveredRubrieken = new List<string>();
		foreach (var propertyName in ObjectHelper.GetPropertyNames<GbaOverlijden>())
		{
			switch (propertyName)
			{
				case nameof(GbaAangaanHuwelijkPartnerschap.Datum):
					deliveredRubrieken.Add("050610");
					break;
				case nameof(GbaAangaanHuwelijkPartnerschap.Plaats):
					if (aangaanHuwelijkPartnerschap.Plaats != null && (!string.IsNullOrWhiteSpace(aangaanHuwelijkPartnerschap.Plaats.Code) || !string.IsNullOrWhiteSpace(aangaanHuwelijkPartnerschap.Plaats.Omschrijving)))
					{
						deliveredRubrieken.Add("050620");
					}
					break;
				case nameof(GbaAangaanHuwelijkPartnerschap.Land):
					if (aangaanHuwelijkPartnerschap.Land != null && (!string.IsNullOrWhiteSpace(aangaanHuwelijkPartnerschap.Land.Code) || !string.IsNullOrWhiteSpace(aangaanHuwelijkPartnerschap.Land.Omschrijving)))
					{
						deliveredRubrieken.Add("050630");
					}
					break;
				default:
					throw new CustomNotImplementedException($"Mapping not implemented for {nameof(GbaOverlijden)} property {propertyName}");
			}
		}

		return deliveredRubrieken;
	}

	private static List<string> ConvertOverlijdenToRubriekCategory(GbaOverlijden overlijden)
	{
		var deliveredRubrieken = new List<string>();
		foreach (var propertyName in ObjectHelper.GetPropertyNames<GbaOverlijden>())
		{
			switch (propertyName)
			{
				case nameof(GbaOverlijden.Plaats):
					if (overlijden.Plaats != null && (!string.IsNullOrWhiteSpace(overlijden.Plaats.Code) || !string.IsNullOrWhiteSpace(overlijden.Plaats.Omschrijving)))
					{
						deliveredRubrieken.Add("060820");
					}
					break;
				case nameof(GbaOverlijden.Land):
					if (overlijden.Land != null && (!string.IsNullOrWhiteSpace(overlijden.Land.Code) || !string.IsNullOrWhiteSpace(overlijden.Land.Omschrijving)))
					{
						deliveredRubrieken.Add("060830");
					}
					deliveredRubrieken.Add("060830");
					break;
				case nameof(GbaOverlijden.InOnderzoek):
					if (overlijden.InOnderzoek != null)
					{
						if (!string.IsNullOrWhiteSpace(overlijden.InOnderzoek.AanduidingGegevensInOnderzoek))
						{
							deliveredRubrieken.Add("068310");
						}
						if (!string.IsNullOrWhiteSpace(overlijden.InOnderzoek.DatumIngangOnderzoek))
						{
							deliveredRubrieken.Add("068320");
						}
					}
					break; // By default geaccepteerd inonderzoek
						   // Already taken care of in ConvertOverlijdenBasisToRubriekCategory
				case nameof(GbaOverlijden.Datum):
					if (!string.IsNullOrWhiteSpace(overlijden.Datum))
					{
						deliveredRubrieken.Add("060810");
					}
					break;
				default:
					throw new CustomNotImplementedException($"Mapping not implemented for {nameof(GbaOverlijden)} property {propertyName}");
			}
		}

		return deliveredRubrieken;
	}

	private static List<string> ConvertOpschortingBijhoudingBasisToRubriekCategory(OpschortingBijhoudingBasis opschortingBijhouding)
	{
		var deliveredRubrieken = new List<string>();
		foreach (var propertyName in ObjectHelper.GetPropertyNames<OpschortingBijhoudingBasis>())
		{
			switch (propertyName)
			{
				case nameof(OpschortingBijhoudingBasis.Reden):
					if (opschortingBijhouding.Reden != null && (!string.IsNullOrWhiteSpace(opschortingBijhouding.Reden.Code) || !string.IsNullOrWhiteSpace(opschortingBijhouding.Reden.Omschrijving)))
					{
						deliveredRubrieken.Add("076720");
					}
					break;
				default:
					throw new CustomNotImplementedException($"Mapping not implemented for {nameof(OpschortingBijhoudingBasis)} property {propertyName}");
			}
		}

		return deliveredRubrieken;
	}

	private static List<string> ConvertOpschortingBijhoudingToRubriekCategory(GbaOpschortingBijhouding opschortingBijhouding)
	{
		var deliveredRubrieken = ConvertOpschortingBijhoudingBasisToRubriekCategory(opschortingBijhouding);
		foreach (var propertyName in ObjectHelper.GetPropertyNames<GbaOpschortingBijhouding>())
		{
			switch (propertyName)
			{
				case nameof(GbaOpschortingBijhouding.Datum):
					if (!string.IsNullOrWhiteSpace(opschortingBijhouding.Datum))
					{
						deliveredRubrieken.Add("076710");
					}
					break;
				// Already taken care of in ConvertOpschortingBijhoudingBasisToRubriekCategory
				case nameof(GbaOpschortingBijhouding.Reden):
					break;
				default:
					throw new CustomNotImplementedException($"Mapping not implemented for {nameof(GbaOpschortingBijhouding)} property {propertyName}");
			}
		}

		return deliveredRubrieken;
	}

	private static List<string> ConvertVerblijfplaatsBeperktToRubriekCategory(GbaVerblijfplaatsBeperkt verblijfplaats)
	{
		var deliveredRubrieken = new List<string>();
		foreach (var propertyName in ObjectHelper.GetPropertyNames<GbaVerblijfplaatsBeperkt>())
		{
			switch (propertyName)
			{
				// Was removed
				//case nameof(GbaVerblijfplaatsBeperkt.FunctieAdres):
				//	if (verblijfplaats.FunctieAdres != null && (!string.IsNullOrWhiteSpace(verblijfplaats.FunctieAdres.Code) || !string.IsNullOrWhiteSpace(verblijfplaats.FunctieAdres.Omschrijving)))
				//	{
				//		deliveredRubrieken.Add("081010");
				//	}
				//	break;
				case nameof(GbaVerblijfplaatsBeperkt.Straat):
					if (!string.IsNullOrWhiteSpace(verblijfplaats.Straat))
					{
						deliveredRubrieken.Add("081110");
					}
					break;
				case nameof(GbaVerblijfplaatsBeperkt.Huisnummer):
					if (verblijfplaats.Huisnummer.HasValue)
					{
						deliveredRubrieken.Add("081120");
					}
					break;
				case nameof(GbaVerblijfplaatsBeperkt.Huisletter):
					if (!string.IsNullOrWhiteSpace(verblijfplaats.Huisletter))
					{
						deliveredRubrieken.Add("081130");
					}
					break;
				case nameof(GbaVerblijfplaatsBeperkt.Huisnummertoevoeging):
					deliveredRubrieken.Add("081140");
					break;
				case nameof(GbaVerblijfplaatsBeperkt.AanduidingBijHuisnummer):
					if (verblijfplaats.AanduidingBijHuisnummer != null && (!string.IsNullOrWhiteSpace(verblijfplaats.AanduidingBijHuisnummer.Code) || !string.IsNullOrWhiteSpace(verblijfplaats.AanduidingBijHuisnummer.Omschrijving)))
					{
						deliveredRubrieken.Add("081150");
					}
					break;
				case nameof(GbaVerblijfplaatsBeperkt.Postcode):
					if (!string.IsNullOrWhiteSpace(verblijfplaats.Postcode))
					{
						deliveredRubrieken.Add("081160");
					}
					break;
				case nameof(GbaVerblijfplaatsBeperkt.Woonplaats):
					if (!string.IsNullOrWhiteSpace(verblijfplaats.Woonplaats))
					{
						deliveredRubrieken.Add("081170");
					}
					break;
				case nameof(GbaVerblijfplaatsBeperkt.Locatiebeschrijving):
					if (!string.IsNullOrWhiteSpace(verblijfplaats.Locatiebeschrijving))
					{
						deliveredRubrieken.Add("081210");
					}
					break;
				case nameof(GbaVerblijfplaatsBeperkt.Land):
					if (verblijfplaats.Land != null && (!string.IsNullOrWhiteSpace(verblijfplaats.Land.Code) || !string.IsNullOrWhiteSpace(verblijfplaats.Land.Omschrijving)))
					{
						deliveredRubrieken.Add("081310");
					}
					break;
				case nameof(GbaVerblijfplaatsBeperkt.Regel1):
					if (!string.IsNullOrWhiteSpace(verblijfplaats.Regel1))
					{
						deliveredRubrieken.Add("081330");
					}
					break;
				case nameof(GbaVerblijfplaatsBeperkt.Regel2):
					if (!string.IsNullOrWhiteSpace(verblijfplaats.Regel2))
					{
						deliveredRubrieken.Add("081340");
					}
					break;
				case nameof(GbaVerblijfplaatsBeperkt.Regel3):
					if (!string.IsNullOrWhiteSpace(verblijfplaats.Regel3))
					{
						deliveredRubrieken.Add("081350");
					}
					break;
				case nameof(GbaVerblijfplaatsBeperkt.InOnderzoek):
					if (verblijfplaats.InOnderzoek != null)
					{
						if (!string.IsNullOrWhiteSpace(verblijfplaats.InOnderzoek.AanduidingGegevensInOnderzoek))
						{
							deliveredRubrieken.Add("088310");
						}
						if (!string.IsNullOrWhiteSpace(verblijfplaats.InOnderzoek.DatumIngangOnderzoek))
						{
							deliveredRubrieken.Add("088320");
						}
					}
					break; // By default geaccepteerd inonderzoek
				default:
					throw new CustomNotImplementedException($"Mapping not implemented for {nameof(GbaVerblijfplaatsBeperkt)} property {propertyName}");
			}
		}

		return deliveredRubrieken;
	}

	private static List<string> ConvertVerblijfplaatsToRubriekCategory(GbaVerblijfplaats verblijfplaats)
	{
		var deliveredRubrieken = new List<string>();
		foreach (var propertyName in ObjectHelper.GetPropertyNames<GbaVerblijfplaats>())
		{
			switch (propertyName)
			{
				case nameof(GbaVerblijfplaats.FunctieAdres):
					if (verblijfplaats.FunctieAdres != null && (!string.IsNullOrWhiteSpace(verblijfplaats.FunctieAdres.Code) || !string.IsNullOrWhiteSpace(verblijfplaats.FunctieAdres.Omschrijving)))
					{
						deliveredRubrieken.Add("081010");
					}
					break;
				case nameof(GbaVerblijfplaats.DatumAanvangAdreshouding):
					if (!string.IsNullOrWhiteSpace(verblijfplaats.DatumAanvangAdreshouding))
					{
						deliveredRubrieken.Add("081030");
					}
					break;
				case nameof(GbaVerblijfplaats.Straat):
					if (!string.IsNullOrWhiteSpace(verblijfplaats.Straat))
					{
						deliveredRubrieken.Add("081110");
					}
					break;
				case nameof(GbaVerblijfplaats.NaamOpenbareRuimte):
					if (!string.IsNullOrWhiteSpace(verblijfplaats.NaamOpenbareRuimte))
					{
						deliveredRubrieken.Add("081115");
					}
					break;
				case nameof(GbaVerblijfplaats.Huisnummer):
					if (verblijfplaats.Huisnummer.HasValue)
					{
						deliveredRubrieken.Add("081120");
					}
					break;
				case nameof(GbaVerblijfplaats.Huisletter):
					if (!string.IsNullOrWhiteSpace(verblijfplaats.Huisletter))
					{
						deliveredRubrieken.Add("081130");
					}
					break;
				case nameof(GbaVerblijfplaats.Huisnummertoevoeging):
					deliveredRubrieken.Add("081140");
					break;
				case nameof(GbaVerblijfplaats.AanduidingBijHuisnummer):
					if (verblijfplaats.AanduidingBijHuisnummer != null && (!string.IsNullOrWhiteSpace(verblijfplaats.AanduidingBijHuisnummer.Code) || !string.IsNullOrWhiteSpace(verblijfplaats.AanduidingBijHuisnummer.Omschrijving)))
					{
						deliveredRubrieken.Add("081150");
					}
					break;
				case nameof(GbaVerblijfplaats.Postcode):
					if (!string.IsNullOrWhiteSpace(verblijfplaats.Postcode))
					{
						deliveredRubrieken.Add("081160");
					}
					break;
				case nameof(GbaVerblijfplaats.Woonplaats):
					if (!string.IsNullOrWhiteSpace(verblijfplaats.Woonplaats))
					{
						deliveredRubrieken.Add("081170");
					}
					break;
				case nameof(GbaVerblijfplaats.AdresseerbaarObjectIdentificatie):
					if (!string.IsNullOrWhiteSpace(verblijfplaats.AdresseerbaarObjectIdentificatie))
					{
						deliveredRubrieken.Add("081180");
					}
					break;
				case nameof(GbaVerblijfplaats.NummeraanduidingIdentificatie):
					if (!string.IsNullOrWhiteSpace(verblijfplaats.NummeraanduidingIdentificatie))
					{
						deliveredRubrieken.Add("081190");
					}
					break;
				case nameof(GbaVerblijfplaats.Locatiebeschrijving):
					if (!string.IsNullOrWhiteSpace(verblijfplaats.Locatiebeschrijving))
					{
						deliveredRubrieken.Add("081210");
					}
					break;
				case nameof(GbaVerblijfplaats.Land):
					if (verblijfplaats.Land != null && (!string.IsNullOrWhiteSpace(verblijfplaats.Land.Code) || !string.IsNullOrWhiteSpace(verblijfplaats.Land.Omschrijving)))
					{
						deliveredRubrieken.Add("081310");
					}
					break;
				case nameof(GbaVerblijfplaats.DatumAanvangAdresBuitenland):
					if (!string.IsNullOrWhiteSpace(verblijfplaats.DatumAanvangAdresBuitenland))
					{
						deliveredRubrieken.Add("081320");
					}
					break;
				case nameof(GbaVerblijfplaats.Regel1):
					if (!string.IsNullOrWhiteSpace(verblijfplaats.Regel1))
					{
						deliveredRubrieken.Add("081330");
					}
					break;
				case nameof(GbaVerblijfplaats.Regel2):
					if (!string.IsNullOrWhiteSpace(verblijfplaats.Regel2))
					{
						deliveredRubrieken.Add("081340");
					}
					break;
				case nameof(GbaVerblijfplaats.Regel3):
					if (!string.IsNullOrWhiteSpace(verblijfplaats.Regel3))
					{
						deliveredRubrieken.Add("081350");
					}
					break;
				case nameof(GbaVerblijfplaats.InOnderzoek):
					if (verblijfplaats.InOnderzoek != null)
					{
						if (!string.IsNullOrWhiteSpace(verblijfplaats.InOnderzoek.AanduidingGegevensInOnderzoek))
						{
							deliveredRubrieken.Add("088310");
						}
						if (!string.IsNullOrWhiteSpace(verblijfplaats.InOnderzoek.DatumIngangOnderzoek))
						{
							deliveredRubrieken.Add("088320");
						}
					}
					break; // By default geaccepteerd inonderzoek
				case nameof(GbaVerblijfplaats.DatumIngangGeldigheid):
					if (!string.IsNullOrWhiteSpace(verblijfplaats.DatumIngangGeldigheid))
					{
						deliveredRubrieken.Add("088510");
					}
					break; // Datum ingang geldigheid
				default:
					throw new CustomNotImplementedException($"Mapping not implemented for {nameof(GbaVerblijfplaats)} property {propertyName}");
			}
		}

		return deliveredRubrieken;
	}

	private static List<string> ConvertImmigratieToRubriekCategory(GbaImmigratie immigratie)
	{
		var deliveredRubrieken = new List<string>();
		foreach (var propertyName in ObjectHelper.GetPropertyNames<GbaImmigratie>())
		{
			switch (propertyName)
			{
				case nameof(GbaImmigratie.LandVanwaarIngeschreven):
					if (immigratie.LandVanwaarIngeschreven != null && (!string.IsNullOrWhiteSpace(immigratie.LandVanwaarIngeschreven.Code) || !string.IsNullOrWhiteSpace(immigratie.LandVanwaarIngeschreven.Omschrijving)))
					{
						deliveredRubrieken.Add("081410");
					}
					break;
				case nameof(GbaImmigratie.DatumVestigingInNederland):
					if (!string.IsNullOrWhiteSpace(immigratie.DatumVestigingInNederland))
					{
						deliveredRubrieken.Add("081420");
					}
					break;
				default:
					throw new CustomNotImplementedException($"Mapping not implemented for {nameof(GbaImmigratie)} property {propertyName}");
			}
		}

		return deliveredRubrieken;
	}

	private static List<string> ConvertKinderenToRubriekCategory(List<GbaKind> kind)
	{
		var categoryCode = "09";
		var deliveredRubrieken = new List<string>();
		foreach (var propertyName in ObjectHelper.GetPropertyNames<GbaKind>())
		{
			kind.ForEach(kind =>
			{
				switch (propertyName)
				{
					case nameof(GbaKind.Burgerservicenummer):
						if (!string.IsNullOrWhiteSpace(kind.Burgerservicenummer))
						{
							deliveredRubrieken.Add("090120");
						}
						break;
					case nameof(GbaKind.Naam):
						if (kind.Naam != null)
						{
							deliveredRubrieken.AddRange(ConvertNaamBasisToRubriekCategory(kind.Naam, categoryCode));
						}
						break;
					case nameof(GbaKind.Geboorte):
						if (kind.Geboorte != null)
						{
							deliveredRubrieken.AddRange(ConvertGeboorteToRubriekCategory(kind.Geboorte, categoryCode));
						}
						break;
					case nameof(GbaKind.InOnderzoek):
						if (kind.InOnderzoek != null)
						{
							if (!string.IsNullOrWhiteSpace(kind.InOnderzoek.AanduidingGegevensInOnderzoek))
							{
								deliveredRubrieken.Add("098310");
							}
							if (!string.IsNullOrWhiteSpace(kind.InOnderzoek.DatumIngangOnderzoek))
							{
								deliveredRubrieken.Add("098320");
							}
						}
						break; // By default geaccepteerd inonderzoek
					default:
						throw new CustomNotImplementedException($"Mapping not implemented for {nameof(GbaKind)} property {propertyName}");
				}
			});
		}

		return deliveredRubrieken;
	}

	private static List<string> ConvertVerblijfstitelToRubriekCategory(GbaVerblijfstitel verblijfstitel)
	{
		var deliveredRubrieken = new List<string>();
		foreach (var propertyName in ObjectHelper.GetPropertyNames<GbaVerblijfstitel>())
		{
			switch (propertyName)
			{
				case nameof(GbaVerblijfstitel.Aanduiding):
					if (verblijfstitel.Aanduiding != null && (!string.IsNullOrWhiteSpace(verblijfstitel.Aanduiding.Code) || !string.IsNullOrWhiteSpace(verblijfstitel.Aanduiding.Omschrijving)))
					{
						deliveredRubrieken.Add("103910");
					}
					break;
				case nameof(GbaVerblijfstitel.DatumEinde):
					if (!string.IsNullOrWhiteSpace(verblijfstitel.DatumEinde))
					{
						deliveredRubrieken.Add("103920");
					}
					break;
				case nameof(GbaVerblijfstitel.DatumIngang):
					if (!string.IsNullOrWhiteSpace(verblijfstitel.DatumIngang))
					{
						deliveredRubrieken.Add("103930");
					}
					break;
				case nameof(GbaVerblijfstitel.InOnderzoek):
					if (verblijfstitel.InOnderzoek != null)
					{
						if (!string.IsNullOrWhiteSpace(verblijfstitel.InOnderzoek.AanduidingGegevensInOnderzoek))
						{
							deliveredRubrieken.Add("108310");
						}
						if (!string.IsNullOrWhiteSpace(verblijfstitel.InOnderzoek.DatumIngangOnderzoek))
						{
							deliveredRubrieken.Add("108320");
						}
					}
					break; // By default geaccepteerd inonderzoek
				case nameof(GbaVerblijfstitel._datumOpneming):
					break;
				default:
					throw new CustomNotImplementedException($"Mapping not implemented for {nameof(GbaVerblijfstitel)} property {propertyName}");
			}
		}

		return deliveredRubrieken;
	}

	private static List<string> ConvertEuropeesKiesrechtToRubriekCategory(GbaEuropeesKiesrecht europeesKiesrecht)
	{
		var deliveredRubrieken = new List<string>();
		foreach (var propertyName in ObjectHelper.GetPropertyNames<GbaEuropeesKiesrecht>())
		{
			switch (propertyName)
			{
				case nameof(GbaEuropeesKiesrecht.Aanduiding):
					if (europeesKiesrecht.Aanduiding != null && (!string.IsNullOrWhiteSpace(europeesKiesrecht.Aanduiding.Code) || !string.IsNullOrWhiteSpace(europeesKiesrecht.Aanduiding.Omschrijving)))
					{
						deliveredRubrieken.Add("133110");
					}
					break;
				case nameof(GbaEuropeesKiesrecht.EinddatumUitsluiting):
					if (!string.IsNullOrWhiteSpace(europeesKiesrecht.EinddatumUitsluiting))
					{
						deliveredRubrieken.Add("133130");
					}
					break;
				default:
					throw new CustomNotImplementedException($"Mapping not implemented for {nameof(GbaEuropeesKiesrecht)} property {propertyName}");
			}
		}

		return deliveredRubrieken;
	}

	private static List<string> ConvertUitsluitingKiesrechtToRubriekCategory(GbaUitsluitingKiesrecht uitsluitingKiesrecht)
	{
		var deliveredRubrieken = new List<string>();
		foreach (var propertyName in ObjectHelper.GetPropertyNames<GbaUitsluitingKiesrecht>())
		{
			switch (propertyName)
			{
				case nameof(GbaUitsluitingKiesrecht.UitgeslotenVanKiesrecht):
					if (uitsluitingKiesrecht.UitgeslotenVanKiesrecht.HasValue)
					{
						deliveredRubrieken.Add("133810");
					}
					break;
				case nameof(GbaUitsluitingKiesrecht.Einddatum):
					if (!string.IsNullOrWhiteSpace(uitsluitingKiesrecht.Einddatum))
					{
						deliveredRubrieken.Add("133820");
					}
					break;
				default:
					throw new CustomNotImplementedException($"Mapping not implemented for {nameof(GbaUitsluitingKiesrecht)} property {propertyName}");
			}
		}

		return deliveredRubrieken;
	}

	private static List<string> ConvertVerificatieToRubriekCategory(GbaVerificatie verificatie)
	{
		var deliveredRubrieken = new List<string>();
		if (!string.IsNullOrWhiteSpace(verificatie.Datum))
		{
			deliveredRubrieken.Add("077110");
		}
		if (!string.IsNullOrWhiteSpace(verificatie.Omschrijving))
		{
			deliveredRubrieken.Add("077120");
		}

		return deliveredRubrieken;
	}

	public static List<string> ConvertHaalCentraalBrpPropsBeperktToRubriekCategory(GbaPersoonBeperkt persoon)
	{
		var deliveredRubrieken = new List<string>();
		var categoryCode = "01";
		foreach (var propertyName in ObjectHelper.GetPropertyNames<GbaPersoonBeperkt>())
		{
			switch (propertyName)
			{
				case nameof(GbaPersoonBeperkt.Rni):
					if (persoon.Rni?.Any() == true)
					{
						deliveredRubrieken.AddRange(ConvertRniToRubriekCategory(persoon.Rni));
					}
					break;
				case nameof(GbaPersoonBeperkt.Burgerservicenummer):
					if (!string.IsNullOrWhiteSpace(persoon.Burgerservicenummer))
					{
						deliveredRubrieken.Add("010120");
					}
					break;
				case nameof(GbaPersoonBeperkt.Naam):
					if (persoon.Naam != null)
					{
						deliveredRubrieken.AddRange(ConvertNaamBasisToRubriekCategory(persoon.Naam, categoryCode));
					}
					break;
				case nameof(GbaPersoonBeperkt.Geboorte):
					if (persoon.Geboorte != null)
					{
						deliveredRubrieken.AddRange(ConvertGeboorteBeperktToRubriekCategory(persoon.Geboorte, categoryCode));
					}
					break;
				case nameof(GbaPersoonBeperkt.Geslacht):
					if (persoon.Geslacht != null && (!string.IsNullOrWhiteSpace(persoon.Geslacht.Code) || !string.IsNullOrWhiteSpace(persoon.Geslacht.Omschrijving)))
					{
						deliveredRubrieken.Add("010410");
					}
					break;
				//case nameof(GbaPersoonBeperkt.Overlijden):
				//	if (persoon.Overlijden != null)
				//	{
				//		deliveredRubrieken.AddRange(ConvertOverlijdenBasisToRubriekCategory(persoon.Overlijden));
				//	}
				//	break;
				case nameof(GbaPersoonBeperkt.OpschortingBijhouding):
					if (persoon.OpschortingBijhouding != null)
					{
						deliveredRubrieken.AddRange(ConvertOpschortingBijhoudingBasisToRubriekCategory(persoon.OpschortingBijhouding));
					}
					break;
				case nameof(GbaPersoonBeperkt.GeheimhoudingPersoonsgegevens):
					if (persoon.GeheimhoudingPersoonsgegevens.HasValue)
					{
						deliveredRubrieken.Add("077010");
					}
					break;
				case nameof(GbaPersoonBeperkt.GemeenteVanInschrijving):
					if (persoon.GemeenteVanInschrijving != null &&
						(!string.IsNullOrWhiteSpace(persoon.GemeenteVanInschrijving?.Code) || !string.IsNullOrWhiteSpace(persoon.GemeenteVanInschrijving?.Omschrijving)))
					{
						deliveredRubrieken.Add("080910");
					}
					break;
				case nameof(GbaPersoonBeperkt.Verblijfplaats):
					if (persoon.Verblijfplaats != null)
					{
						deliveredRubrieken.AddRange(ConvertVerblijfplaatsBeperktToRubriekCategory(persoon.Verblijfplaats));
					}
					break;
				case nameof(GbaPersoonBeperkt.Verificatie):
					if (persoon.Verificatie != null)
					{
						deliveredRubrieken.AddRange(ConvertVerificatieToRubriekCategory(persoon.Verificatie));
					}
					break;
				case nameof(GbaPersoonBeperkt.PersoonInOnderzoek):
					if (persoon.PersoonInOnderzoek != null)
					{
						if (!string.IsNullOrWhiteSpace(persoon.PersoonInOnderzoek.AanduidingGegevensInOnderzoek))
						{
							deliveredRubrieken.Add("018310");
						}
						if (!string.IsNullOrWhiteSpace(persoon.PersoonInOnderzoek.DatumIngangOnderzoek))
						{
							deliveredRubrieken.Add("018320");
						}
					}
					break; // By default geaccepteerd
				default:
					throw new CustomNotImplementedException($"Mapping not implemented for {nameof(GbaPersoon)} property {propertyName}");
			}
		}
		return deliveredRubrieken.OrderBy(rubriek => rubriek[..]).ToList();
	}
}
