
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
            //Geslacht = MapGeslacht(persoon.Geslacht),
            //PersoonInOnderzoek = MapInOnderzoek(persoon.PersoonInOnderzoek),
            //GezagInOnderzoek = MapInOnderzoek(persoon.GezagInOnderzoek),
            //UitsluitingKiesrecht = MapUitsluitingKiesrecht(persoon.UitsluitingKiesrecht),
            //EuropeesKiesrecht = MapEuropeesKiesrecht(persoon.EuropeesKiesrecht),
            //Naam = MapNaam(persoon.Naam),
            //Nationaliteiten = MapNationaliteiten(persoon.Nationaliteiten),
            //Geboorte = MapGeboorte(persoon.Geboorte),
            //OpschortingBijhouding = MapOpschortingBijhouding(persoon.OpschortingBijhouding),
            //Overlijden = MapOverlijden(persoon.Overlijden),
            //Verblijfplaats = MapVerblijfplaats(persoon.Verblijfplaats),
            //Immigratie = MapImmigratie(persoon.Immigratie),
            //GemeenteVanInschrijving = MapGemeenteVanInschrijving(persoon.GemeenteVanInschrijving),
            //DatumInschrijvingInGemeente = persoon.DatumInschrijvingInGemeente,
            //IndicatieCurateleRegister = MapIndicatieCurateleRegister(persoon.IndicatieCurateleRegister),
            //IndicatieGezagMinderjarige = MapIndicatieGezagMinderjarige(persoon.IndicatieGezagMinderjarige),
            Gezag = null,
            //Verblijfstitel = MapVerblijfstitel(persoon.Verblijfstitel),
            //Kinderen = MapKinderen(persoon.Kinderen),
            //Ouders = MapOuders(persoon.Ouders),
            //Partners = MapPartners(persoon.Partners),
            //Rni = MapRni(persoon.Rni),
            //Verificatie = MapVerificatie(persoon.Verificatie)
        };
    }
}
