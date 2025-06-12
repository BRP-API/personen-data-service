

using Rvig.HaalCentraalApi.Shared.ApiModels.Universal;

namespace Rvig.HaalCentraalApi.Personen.ApiModels.BRP.Deprecated;

public partial class GbaPersoon : IPersoonMetGezag
{
    public static GbaPersoon MapFrom(BRP.GbaPersoon persoon)
    {
        return new GbaPersoon
        {
            ANummer = persoon.ANummer,
            Burgerservicenummer = persoon.Burgerservicenummer,
            DatumEersteInschrijvingGBA = persoon.DatumEersteInschrijvingGBA,
            GeheimhoudingPersoonsgegevens = persoon.GeheimhoudingPersoonsgegevens,
            Geslacht = MapGeslacht(persoon.Geslacht),
            PersoonInOnderzoek = MapInOnderzoek(persoon.PersoonInOnderzoek),
            GezagInOnderzoek = MapInOnderzoek(persoon.GezagInOnderzoek),
            UitsluitingKiesrecht = MapUitsluitingKiesrecht(persoon.UitsluitingKiesrecht),
            EuropeesKiesrecht = MapEuropeesKiesrecht(persoon.EuropeesKiesrecht),
            Naam = MapNaam(persoon.Naam),
            Nationaliteiten = MapNationaliteiten(persoon.Nationaliteiten),
            Geboorte = MapGeboorte(persoon.Geboorte),
            OpschortingBijhouding = MapOpschortingBijhouding(persoon.OpschortingBijhouding),
            Overlijden = MapOverlijden(persoon.Overlijden),
            Verblijfplaats = MapVerblijfplaats(persoon.Verblijfplaats),
            Immigratie = MapImmigratie(persoon.Immigratie),
            GemeenteVanInschrijving = MapWaardetabel(persoon.GemeenteVanInschrijving),
            DatumInschrijvingInGemeente = persoon.DatumInschrijvingInGemeente,
            IndicatieCurateleRegister = persoon.IndicatieCurateleRegister,
            IndicatieGezagMinderjarige = MapWaardetabel(persoon.IndicatieGezagMinderjarige),
            Gezag = null,
            Verblijfstitel = MapVerblijfstitel(persoon.Verblijfstitel),
            Kinderen = MapKinderen(persoon.Kinderen),
            Ouders = MapOuders(persoon.Ouders),
            Partners = MapPartners(persoon.Partners),
            Rni = MapRni(persoon.Rni),
            Verificatie = MapVerificatie(persoon.Verificatie)
        };
    }

    private static GbaVerificatie? MapVerificatie(BRP.GbaVerificatie? verificatie)
    {
        return verificatie != null ? new GbaVerificatie
        {
            Datum = verificatie.Datum,
            Omschrijving = verificatie.Omschrijving
        } : null;
    }

    private static List<RniDeelnemer>? MapRni(List<BRP.RniDeelnemer>? rni)
    {
        return rni?.Select(r => new RniDeelnemer
        {
            Deelnemer = MapWaardetabel(r.Deelnemer),
            OmschrijvingVerdrag = r.OmschrijvingVerdrag,
            Categorie = r.Categorie
        }).ToList();
    }

    private static List<GbaPartner>? MapPartners(List<BRP.GbaPartner>? partners)
    {
        return partners?.Select(p => new GbaPartner
        {
            Burgerservicenummer = p.Burgerservicenummer,
            Geslacht = MapWaardetabel(p.Geslacht),
            SoortVerbintenis = MapWaardetabel(p.SoortVerbintenis),
            Naam = MapNaamBasis(p.Naam),
            Geboorte = MapGeboorte(p.Geboorte),
            AangaanHuwelijkPartnerschap = p.AangaanHuwelijkPartnerschap != null ? new()
            {
                Datum = p.AangaanHuwelijkPartnerschap?.Datum,
                Land = MapWaardetabel(p.AangaanHuwelijkPartnerschap?.Land),
                Plaats = MapWaardetabel(p.AangaanHuwelijkPartnerschap?.Plaats)
            } : null,
            OntbindingHuwelijkPartnerschap = p.OntbindingHuwelijkPartnerschap != null ? new()
            {
                Datum = p.OntbindingHuwelijkPartnerschap?.Datum
            } : null,
            InOnderzoek = MapInOnderzoek(p.InOnderzoek),
        }).ToList();
    }

    private static List<GbaOuder>? MapOuders(List<BRP.GbaOuder>? ouders)
    {
        return ouders?.Select(o => new GbaOuder
        {
            Burgerservicenummer = o.Burgerservicenummer,
            Naam = MapNaamBasis(o.Naam),
            Geboorte = MapGeboorte(o.Geboorte),
            InOnderzoek = MapInOnderzoek(o.InOnderzoek),
            Geslacht = MapWaardetabel(o.Geslacht),
            DatumIngangFamilierechtelijkeBetrekking = o.DatumIngangFamilierechtelijkeBetrekking,
            OuderAanduiding = o.OuderAanduiding
        }).ToList();
    }

    private static List<GbaKind>? MapKinderen(List<BRP.GbaKind>? kinderen)
    {
        return kinderen?.Select(k => new GbaKind
        {
            Burgerservicenummer = k.Burgerservicenummer,
            InOnderzoek = MapInOnderzoek(k.InOnderzoek),
            Naam = MapNaamBasis(k.Naam),
            Geboorte = MapGeboorte(k.Geboorte),
        }).ToList();
    }

    private static NaamBasis? MapNaamBasis(BRP.NaamBasis? naam)
    {
        return naam != null ? new NaamBasis
        {
            Voornamen = naam.Voornamen,
            Voorvoegsel = naam.Voorvoegsel,
            Geslachtsnaam = naam.Geslachtsnaam,
            AdellijkeTitelPredicaat = naam.AdellijkeTitelPredicaat != null ? new()
            {
                Code = naam.AdellijkeTitelPredicaat?.Code,
                Omschrijving = naam.AdellijkeTitelPredicaat?.Omschrijving,
                Soort = naam.AdellijkeTitelPredicaat?.Soort switch
                {
                    BRP.AdellijkeTitelPredicaatSoort.Titel => AdellijkeTitelPredicaatSoort.Titel,
                    BRP.AdellijkeTitelPredicaatSoort.Predicaat => AdellijkeTitelPredicaatSoort.Predicaat,
                    _ => null
                },
            } : null,
        } : null;
    }

    private static GbaVerblijfstitel? MapVerblijfstitel(BRP.GbaVerblijfstitel? verblijfstitel)
    {
        return verblijfstitel != null ? new GbaVerblijfstitel
        {
            DatumEinde = verblijfstitel.DatumEinde,
            DatumIngang = verblijfstitel.DatumIngang,
            InOnderzoek = MapInOnderzoek(verblijfstitel.InOnderzoek),
            Aanduiding = MapWaardetabel(verblijfstitel.Aanduiding),
        } : null;
    }

    private static Waardetabel? MapWaardetabel(BRP.Waardetabel? waardetabel)
    {
        return waardetabel != null ? new Waardetabel
        {
            Code = waardetabel.Code,
            Omschrijving = waardetabel.Omschrijving
        } : null;
    }

    private static GbaImmigratie? MapImmigratie(BRP.GbaImmigratie? immigratie)
    {
        return immigratie != null ? new GbaImmigratie
        {
            DatumVestigingInNederland = immigratie.DatumVestigingInNederland,
            LandVanwaarIngeschreven = MapWaardetabel(immigratie.LandVanwaarIngeschreven),
        } : null;
    }

    private static GbaVerblijfplaats? MapVerblijfplaats(BRP.GbaVerblijfplaats? verblijfplaats)
    {
        return verblijfplaats != null ? new GbaVerblijfplaats
        {
            AdresseerbaarObjectIdentificatie = verblijfplaats.AdresseerbaarObjectIdentificatie,
            NummeraanduidingIdentificatie = verblijfplaats.NummeraanduidingIdentificatie,
            DatumAanvangAdresBuitenland = verblijfplaats.DatumAanvangAdresBuitenland,
            DatumAanvangAdreshouding = verblijfplaats.DatumAanvangAdreshouding,
            DatumIngangGeldigheid = verblijfplaats.DatumIngangGeldigheid,
            FunctieAdres = MapWaardetabel(verblijfplaats.FunctieAdres),
            NaamOpenbareRuimte = verblijfplaats.NaamOpenbareRuimte,
            Straat = verblijfplaats.Straat,
            Huisnummer = verblijfplaats.Huisnummer,
            Huisletter = verblijfplaats.Huisletter,
            Huisnummertoevoeging = verblijfplaats.Huisnummertoevoeging,
            AanduidingBijHuisnummer = MapWaardetabel(verblijfplaats.AanduidingBijHuisnummer),
            Postcode = verblijfplaats.Postcode,
            Woonplaats = verblijfplaats.Woonplaats,
            Locatiebeschrijving = verblijfplaats.Locatiebeschrijving,
            Land = MapWaardetabel(verblijfplaats.Land),
            Regel1 = verblijfplaats.Regel1,
            Regel2 = verblijfplaats.Regel2,
            Regel3 = verblijfplaats.Regel3,
            InOnderzoek = MapInOnderzoek(verblijfplaats.InOnderzoek)
        } : null;
    }

    private static GbaOverlijden? MapOverlijden(BRP.GbaOverlijden? overlijden)
    {
        return overlijden != null ? new GbaOverlijden
        {
            Datum = overlijden.Datum,
            InOnderzoek = MapInOnderzoek(overlijden.InOnderzoek),
            Land = MapWaardetabel(overlijden.Land),
            Plaats = MapWaardetabel(overlijden.Plaats),
        } : null;
    }

    private static GbaOpschortingBijhouding? MapOpschortingBijhouding(BRP.GbaOpschortingBijhouding? opschortingBijhouding)
    {
        return opschortingBijhouding != null ? new GbaOpschortingBijhouding
        {
            Datum = opschortingBijhouding.Datum,
            Reden = MapWaardetabel(opschortingBijhouding.Reden),
        } : null;
    }

    private static GbaGeboorte? MapGeboorte(BRP.GbaGeboorte? geboorte)
    {
        return geboorte != null ? new GbaGeboorte
        {
            Datum = geboorte.Datum,
            Land = MapWaardetabel(geboorte.Land),
            Plaats = MapWaardetabel(geboorte.Plaats)
        } : null;
    }

    private static List<GbaNationaliteit>? MapNationaliteiten(List<BRP.GbaNationaliteit>? nationaliteiten)
    {
        return nationaliteiten?.Select(n => new GbaNationaliteit
        {
            AanduidingBijzonderNederlanderschap = n.AanduidingBijzonderNederlanderschap,
            DatumIngangGeldigheid = n.DatumIngangGeldigheid,
            Nationaliteit = MapWaardetabel(n.Nationaliteit),
            RedenOpname = MapWaardetabel(n.RedenOpname),
            InOnderzoek = MapInOnderzoek(n.InOnderzoek)
        }).ToList();
    }

    private static GbaNaamPersoon? MapNaam(BRP.GbaNaamPersoon? naam)
    {
        return naam != null ? new GbaNaamPersoon
        {
            Voornamen = naam.Voornamen,
            AdellijkeTitelPredicaat = naam.AdellijkeTitelPredicaat != null ? new()
            {
                Code = naam.AdellijkeTitelPredicaat?.Code,
                Omschrijving = naam.AdellijkeTitelPredicaat?.Omschrijving,
                Soort = naam.AdellijkeTitelPredicaat?.Soort switch
                {
                    BRP.AdellijkeTitelPredicaatSoort.Titel => AdellijkeTitelPredicaatSoort.Titel,
                    BRP.AdellijkeTitelPredicaatSoort.Predicaat => AdellijkeTitelPredicaatSoort.Predicaat,
                    _ => null
                },
            } : null,
            Voorvoegsel = naam.Voorvoegsel,
            Geslachtsnaam = naam.Geslachtsnaam,
            AanduidingNaamgebruik = MapWaardetabel(naam.AanduidingNaamgebruik),
        } : null;   
    }

    private static GbaEuropeesKiesrecht? MapEuropeesKiesrecht(BRP.GbaEuropeesKiesrecht? europeesKiesrecht)
    {
        return europeesKiesrecht != null ? new GbaEuropeesKiesrecht
        {
            Aanduiding = MapWaardetabel(europeesKiesrecht.Aanduiding),
            EinddatumUitsluiting = europeesKiesrecht.EinddatumUitsluiting
        } : null;
    }

    private static GbaUitsluitingKiesrecht? MapUitsluitingKiesrecht(BRP.GbaUitsluitingKiesrecht? uitsluitingKiesrecht)
    {
        return uitsluitingKiesrecht != null ? new GbaUitsluitingKiesrecht
        {
            Einddatum = uitsluitingKiesrecht.Einddatum,
            UitgeslotenVanKiesrecht = uitsluitingKiesrecht.UitgeslotenVanKiesrecht
        } : null;
    }

    private static GbaInOnderzoek? MapInOnderzoek(GbaInOnderzoek? inOnderzoek)
    {
        return inOnderzoek != null ? new GbaInOnderzoek
        {
            AanduidingGegevensInOnderzoek = inOnderzoek.AanduidingGegevensInOnderzoek,
            DatumIngangOnderzoek = inOnderzoek.DatumIngangOnderzoek,
        } : null;
    }

    private static Geslachtsaanduiding? MapGeslacht(BRP.Geslachtsaanduiding? geslacht)
    {
        return geslacht != null ? new Geslachtsaanduiding
        {
            Code = geslacht.Code,
            Omschrijving = geslacht.Omschrijving,
        } : null;
    }
}
