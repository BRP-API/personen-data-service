
namespace Rvig.HaalCentraalApi.Personen.ApiModels.BRP.Deprecated;

public static class Mappers
{
    public static GbaGezagPersoonBeperkt Map(this BRP.GbaGezagPersoonBeperkt persoon)
    {
        return new GbaGezagPersoonBeperkt
        {
            Burgerservicenummer = persoon.Burgerservicenummer,
            Geboorte = persoon.Geboorte.Map(),
            GeheimhoudingPersoonsgegevens = persoon.GeheimhoudingPersoonsgegevens,
            Geslacht = persoon.Geslacht.Map(),
            PersoonInOnderzoek = persoon.PersoonInOnderzoek.Map(),
            Naam = persoon.Naam.Map(),
            OpschortingBijhouding = persoon.OpschortingBijhouding.Map(),
            GemeenteVanInschrijving = persoon.GemeenteVanInschrijving.Map(),
            Verblijfplaats = persoon.Verblijfplaats.Map(),
            Rni = persoon.Rni.Map(),
            Verificatie = persoon.Verificatie.Map(),
        };
    }

    public static GbaPersoonBeperkt Map(this BRP.GbaPersoonBeperkt persoon)
    {
        return new GbaPersoonBeperkt
        {
            Burgerservicenummer = persoon.Burgerservicenummer,
            Geboorte = persoon.Geboorte.Map(),
            GeheimhoudingPersoonsgegevens = persoon.GeheimhoudingPersoonsgegevens,
            Geslacht = persoon.Geslacht.Map(),
            PersoonInOnderzoek = persoon.PersoonInOnderzoek.Map(),
            Naam = persoon.Naam.Map(),
            OpschortingBijhouding = persoon.OpschortingBijhouding.Map(),
            GemeenteVanInschrijving = persoon.GemeenteVanInschrijving.Map(),
            Verblijfplaats = persoon.Verblijfplaats.Map(),
            Rni = persoon.Rni.Map(),
            Verificatie = persoon.Verificatie.Map(),
        };
    }

    public static GbaPersoon Map(this BRP.GbaPersoon persoon)
    {
        return new GbaPersoon
        {
            ANummer = persoon.ANummer,
            Burgerservicenummer = persoon.Burgerservicenummer,
            DatumEersteInschrijvingGBA = persoon.DatumEersteInschrijvingGBA,
            GeheimhoudingPersoonsgegevens = persoon.GeheimhoudingPersoonsgegevens,
            Geslacht = persoon.Geslacht.Map(),
            PersoonInOnderzoek = persoon.PersoonInOnderzoek.Map(),
            GezagInOnderzoek = persoon.GezagInOnderzoek.Map(),
            UitsluitingKiesrecht = persoon.UitsluitingKiesrecht.Map(),
            EuropeesKiesrecht = persoon.EuropeesKiesrecht.Map(),
            Naam = persoon.Naam.Map(),
            Nationaliteiten = persoon.Nationaliteiten.Map(),
            Geboorte = persoon.Geboorte.Map(),
            OpschortingBijhouding = persoon.OpschortingBijhouding.Map(),
            Overlijden = persoon.Overlijden.Map(),
            Verblijfplaats = persoon.Verblijfplaats.Map(),
            Immigratie = persoon.Immigratie.Map(),
            GemeenteVanInschrijving = persoon.GemeenteVanInschrijving.Map(),
            DatumInschrijvingInGemeente = persoon.DatumInschrijvingInGemeente,
            IndicatieCurateleRegister = persoon.IndicatieCurateleRegister,
            IndicatieGezagMinderjarige = persoon.IndicatieGezagMinderjarige.Map(),
            Gezag = null,
            Verblijfstitel = persoon.Verblijfstitel.Map(),
            Kinderen = persoon.Kinderen.Map(),
            Ouders = persoon.Ouders.Map(),
            Partners = persoon.Partners.Map(),
            Rni = persoon.Rni.Map(),
            Verificatie = persoon.Verificatie.Map()
        };
    }

    public static GeboorteBasis? Map(this BRP.GeboorteBasis geboorte)
    {
        return geboorte != null ? new GeboorteBasis
        {
            Datum = geboorte.Datum
        } : null;
    }

    public static Geslachtsaanduiding? Map(this BRP.Geslachtsaanduiding? geslacht)
    {
        return geslacht != null ? new Geslachtsaanduiding
        {
            Code = geslacht.Code,
            Omschrijving = geslacht.Omschrijving,
        } : null;
    }

    public static InOnderzoek? Map(this BRP.InOnderzoek? inOnderzoek)
    {
        return inOnderzoek != null ? new InOnderzoek
        {
            AanduidingGegevensInOnderzoek = inOnderzoek.AanduidingGegevensInOnderzoek,
            DatumIngangOnderzoek = inOnderzoek.DatumIngangOnderzoek,
        } : null;
    }

    public static NaamBasis? Map(this BRP.NaamBasis naam)
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

    public static GbaOpschortingBijhouding? Map(this BRP.GbaOpschortingBijhouding? opschortingBijhouding)
    {
        return opschortingBijhouding != null ? new GbaOpschortingBijhouding
        {
            Datum = opschortingBijhouding.Datum,
            Reden = opschortingBijhouding.Reden.Map(),
        } : null;
    }

    public static GbaVerificatie? Map(this BRP.GbaVerificatie? verificatie)
    {
        return verificatie != null ? new GbaVerificatie
        {
            Datum = verificatie.Datum,
            Omschrijving = verificatie.Omschrijving
        } : null;
    }

    public static List<RniDeelnemer>? Map(this List<BRP.RniDeelnemer>? rni)
    {
        return rni?.Select(r => new RniDeelnemer
        {
            Deelnemer = r.Deelnemer.Map(),
            OmschrijvingVerdrag = r.OmschrijvingVerdrag,
            Categorie = r.Categorie
        }).ToList();
    }

    public static Waardetabel? Map(this BRP.Waardetabel? waardetabel)
    {
        return waardetabel != null ? new Waardetabel
        {
            Code = waardetabel.Code,
            Omschrijving = waardetabel.Omschrijving
        } : null;
    }

    public static GbaVerblijfplaatsBeperkt? Map(this BRP.GbaVerblijfplaatsBeperkt? verblijfplaats)
    {
        return verblijfplaats != null ? new GbaVerblijfplaatsBeperkt
        {
            Straat = verblijfplaats.Straat,
            Huisnummer = verblijfplaats.Huisnummer,
            Huisletter = verblijfplaats.Huisletter,
            Huisnummertoevoeging = verblijfplaats.Huisnummertoevoeging,
            AanduidingBijHuisnummer = verblijfplaats.AanduidingBijHuisnummer.Map(),
            Postcode = verblijfplaats.Postcode,
            Woonplaats = verblijfplaats.Woonplaats,
            Locatiebeschrijving = verblijfplaats.Locatiebeschrijving,
            Land = verblijfplaats.Land.Map(),
            Regel1 = verblijfplaats.Regel1,
            Regel2 = verblijfplaats.Regel2,
            Regel3 = verblijfplaats.Regel3,
            InOnderzoek = verblijfplaats.InOnderzoek.Map()
        } : null;
    }

    private static List<GbaPartner>? Map(this List<BRP.GbaPartner>? partners)
    {
        return partners?.Select(p => new GbaPartner
        {
            Burgerservicenummer = p.Burgerservicenummer,
            Geslacht = p.Geslacht.Map(),
            SoortVerbintenis = p.SoortVerbintenis.Map(),
            Naam = p.Naam.Map(),
            Geboorte = p.Geboorte.Map(),
            AangaanHuwelijkPartnerschap = p.AangaanHuwelijkPartnerschap != null ? new()
            {
                Datum = p.AangaanHuwelijkPartnerschap?.Datum,
                Land = p.AangaanHuwelijkPartnerschap?.Land.Map(),
                Plaats = p.AangaanHuwelijkPartnerschap?.Plaats.Map()
            } : null,
            OntbindingHuwelijkPartnerschap = p.OntbindingHuwelijkPartnerschap != null ? new()
            {
                Datum = p.OntbindingHuwelijkPartnerschap?.Datum
            } : null,
            InOnderzoek = p.InOnderzoek.Map(),
        }).ToList();
    }

    private static List<GbaOuder>? Map(this List<BRP.GbaOuder>? ouders)
    {
        return ouders?.Select(o => new GbaOuder
        {
            Burgerservicenummer = o.Burgerservicenummer,
            Naam = o.Naam.Map(),
            Geboorte = o.Geboorte.Map(),
            InOnderzoek = o.InOnderzoek.Map(),
            Geslacht = o.Geslacht.Map(),
            DatumIngangFamilierechtelijkeBetrekking = o.DatumIngangFamilierechtelijkeBetrekking,
            OuderAanduiding = o.OuderAanduiding
        }).ToList();
    }

    private static List<GbaKind>? Map(this List<BRP.GbaKind>? kinderen)
    {
        return kinderen?.Select(k => new GbaKind
        {
            Burgerservicenummer = k.Burgerservicenummer,
            InOnderzoek = k.InOnderzoek.Map(),
            Naam = k.Naam.Map(),
            Geboorte = k.Geboorte.Map(),
        }).ToList();
    }

    private static GbaVerblijfstitel? Map(this BRP.GbaVerblijfstitel? verblijfstitel)
    {
        return verblijfstitel != null ? new GbaVerblijfstitel
        {
            DatumEinde = verblijfstitel.DatumEinde,
            DatumIngang = verblijfstitel.DatumIngang,
            InOnderzoek = verblijfstitel.InOnderzoek.Map(),
            Aanduiding = verblijfstitel.Aanduiding.Map(),
        } : null;
    }

    private static GbaImmigratie? Map(this BRP.GbaImmigratie? immigratie)
    {
        return immigratie != null ? new GbaImmigratie
        {
            DatumVestigingInNederland = immigratie.DatumVestigingInNederland,
            LandVanwaarIngeschreven = immigratie.LandVanwaarIngeschreven.Map(),
        } : null;
    }

    private static GbaVerblijfplaats? Map(this BRP.GbaVerblijfplaats? verblijfplaats)
    {
        return verblijfplaats != null ? new GbaVerblijfplaats
        {
            AdresseerbaarObjectIdentificatie = verblijfplaats.AdresseerbaarObjectIdentificatie,
            NummeraanduidingIdentificatie = verblijfplaats.NummeraanduidingIdentificatie,
            DatumAanvangAdresBuitenland = verblijfplaats.DatumAanvangAdresBuitenland,
            DatumAanvangAdreshouding = verblijfplaats.DatumAanvangAdreshouding,
            DatumIngangGeldigheid = verblijfplaats.DatumIngangGeldigheid,
            FunctieAdres = verblijfplaats.FunctieAdres.Map(),
            NaamOpenbareRuimte = verblijfplaats.NaamOpenbareRuimte,
            Straat = verblijfplaats.Straat,
            Huisnummer = verblijfplaats.Huisnummer,
            Huisletter = verblijfplaats.Huisletter,
            Huisnummertoevoeging = verblijfplaats.Huisnummertoevoeging,
            AanduidingBijHuisnummer = verblijfplaats.AanduidingBijHuisnummer.Map(),
            Postcode = verblijfplaats.Postcode,
            Woonplaats = verblijfplaats.Woonplaats,
            Locatiebeschrijving = verblijfplaats.Locatiebeschrijving,
            Land = verblijfplaats.Land.Map(),
            Regel1 = verblijfplaats.Regel1,
            Regel2 = verblijfplaats.Regel2,
            Regel3 = verblijfplaats.Regel3,
            InOnderzoek = verblijfplaats.InOnderzoek.Map()
        } : null;
    }

    private static GbaOverlijden? Map(this BRP.GbaOverlijden? overlijden)
    {
        return overlijden != null ? new GbaOverlijden
        {
            Datum = overlijden.Datum,
            InOnderzoek = overlijden.InOnderzoek.Map(),
            Land = overlijden.Land.Map(),
            Plaats = overlijden.Plaats.Map(),
        } : null;
    }

    private static GbaGeboorte? Map(this BRP.GbaGeboorte? geboorte)
    {
        return geboorte != null ? new GbaGeboorte
        {
            Datum = geboorte.Datum,
            Land = geboorte.Land.Map(),
            Plaats = geboorte.Plaats.Map()
        } : null;
    }

    private static List<GbaNationaliteit>? Map(this List<BRP.GbaNationaliteit>? nationaliteiten)
    {
        return nationaliteiten?.Select(n => new GbaNationaliteit
        {
            AanduidingBijzonderNederlanderschap = n.AanduidingBijzonderNederlanderschap,
            DatumIngangGeldigheid = n.DatumIngangGeldigheid,
            Nationaliteit = n.Nationaliteit.Map(),
            RedenOpname = n.RedenOpname.Map(),
            InOnderzoek = n.InOnderzoek.Map()
        }).ToList();
    }

    private static GbaNaamPersoon? Map(this BRP.GbaNaamPersoon? naam)
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
            AanduidingNaamgebruik = naam.AanduidingNaamgebruik.Map(),
        } : null;
    }

    private static GbaEuropeesKiesrecht? Map(this BRP.GbaEuropeesKiesrecht? europeesKiesrecht)
    {
        return europeesKiesrecht != null ? new GbaEuropeesKiesrecht
        {
            Aanduiding = europeesKiesrecht.Aanduiding.Map(),
            EinddatumUitsluiting = europeesKiesrecht.EinddatumUitsluiting
        } : null;
    }

    private static GbaUitsluitingKiesrecht? Map(this BRP.GbaUitsluitingKiesrecht? uitsluitingKiesrecht)
    {
        return uitsluitingKiesrecht != null ? new GbaUitsluitingKiesrecht
        {
            Einddatum = uitsluitingKiesrecht.Einddatum,
            UitgeslotenVanKiesrecht = uitsluitingKiesrecht.UitgeslotenVanKiesrecht
        } : null;
    }
}
