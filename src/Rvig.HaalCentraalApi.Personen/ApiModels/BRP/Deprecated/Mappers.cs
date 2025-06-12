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

    public static Shared.ApiModels.Universal.GbaInOnderzoek? Map(this Shared.ApiModels.Universal.GbaInOnderzoek? inOnderzoek)
    {
        return inOnderzoek != null ? new Shared.ApiModels.Universal.GbaInOnderzoek
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
}
