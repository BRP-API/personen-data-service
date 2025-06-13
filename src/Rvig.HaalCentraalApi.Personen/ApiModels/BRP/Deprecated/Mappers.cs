
namespace Rvig.HaalCentraalApi.Personen.ApiModels.BRP.Deprecated;

public static class Mappers
{
    public static GbaGezagPersoonBeperkt Map(this BRP.GbaGezagPersoonBeperkt persoon)
    {
        return new GbaGezagPersoonBeperkt
        {
            Burgerservicenummer = persoon.Burgerservicenummer,
            Geboorte = persoon.Geboorte,
            GeheimhoudingPersoonsgegevens = persoon.GeheimhoudingPersoonsgegevens,
            Geslacht = persoon.Geslacht,
            PersoonInOnderzoek = persoon.PersoonInOnderzoek,
            Naam = persoon.Naam,
            OpschortingBijhouding = persoon.OpschortingBijhouding,
            GemeenteVanInschrijving = persoon.GemeenteVanInschrijving,
            Verblijfplaats = persoon.Verblijfplaats,
            Rni = persoon.Rni,
            Verificatie = persoon.Verificatie,
        };
    }

    public static GbaPersoonBeperkt Map(this BRP.GbaPersoonBeperkt persoon)
    {
        return new GbaPersoonBeperkt
        {
            Burgerservicenummer = persoon.Burgerservicenummer,
            Geboorte = persoon.Geboorte,
            GeheimhoudingPersoonsgegevens = persoon.GeheimhoudingPersoonsgegevens,
            Geslacht = persoon.Geslacht,
            PersoonInOnderzoek = persoon.PersoonInOnderzoek,
            Naam = persoon.Naam,
            OpschortingBijhouding = persoon.OpschortingBijhouding,
            GemeenteVanInschrijving = persoon.GemeenteVanInschrijving,
            Verblijfplaats = persoon.Verblijfplaats,
            Rni = persoon.Rni,
            Verificatie = persoon.Verificatie,
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
            Geslacht = persoon.Geslacht,
            PersoonInOnderzoek = persoon.PersoonInOnderzoek,
            GezagInOnderzoek = persoon.GezagInOnderzoek,
            UitsluitingKiesrecht = persoon.UitsluitingKiesrecht,
            EuropeesKiesrecht = persoon.EuropeesKiesrecht,
            Naam = persoon.Naam,
            Nationaliteiten = persoon.Nationaliteiten,
            Geboorte = persoon.Geboorte,
            OpschortingBijhouding = persoon.OpschortingBijhouding,
            Overlijden = persoon.Overlijden,
            Verblijfplaats = persoon.Verblijfplaats,
            Immigratie = persoon.Immigratie,
            GemeenteVanInschrijving = persoon.GemeenteVanInschrijving,
            DatumInschrijvingInGemeente = persoon.DatumInschrijvingInGemeente,
            IndicatieCurateleRegister = persoon.IndicatieCurateleRegister,
            IndicatieGezagMinderjarige = persoon.IndicatieGezagMinderjarige,
            Gezag = null,
            Verblijfstitel = persoon.Verblijfstitel,
            Kinderen = persoon.Kinderen,
            Ouders = persoon.Ouders,
            Partners = persoon.Partners,
            Rni = persoon.Rni,
            Verificatie = persoon.Verificatie
        };
    }
}
