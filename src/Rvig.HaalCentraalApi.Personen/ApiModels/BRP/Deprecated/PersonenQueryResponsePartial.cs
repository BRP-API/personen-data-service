
namespace Rvig.HaalCentraalApi.Personen.ApiModels.BRP.Deprecated;

public partial class PersonenQueryResponse
{
    public static PersonenQueryResponse MapFrom(BRP.PersonenQueryResponse personenResponse)
    {
        switch (personenResponse)
        {
            case BRP.RaadpleegMetBurgerservicenummerResponse response:
                return MapFrom(response);
            case BRP.ZoekMetAdresseerbaarObjectIdentificatieResponse response:
                return MapFrom(response);
            case BRP.ZoekMetGeslachtsnaamEnGeboortedatumResponse response:
                return MapFrom(response);
            case BRP.ZoekMetNaamEnGemeenteVanInschrijvingResponse response:
                return MapFrom(response);
            case BRP.ZoekMetNummeraanduidingIdentificatieResponse response:
                return MapFrom(response);
            case BRP.ZoekMetPostcodeEnHuisnummerResponse response:
                return MapFrom(response);
            case BRP.ZoekMetStraatHuisnummerEnGemeenteVanInschrijvingResponse response:
                return MapFrom(response);
            default:
                return new RaadpleegMetBurgerservicenummerResponse(); ;
        }
    }

    private static RaadpleegMetBurgerservicenummerResponse MapFrom(BRP.RaadpleegMetBurgerservicenummerResponse response)
    {
        return new RaadpleegMetBurgerservicenummerResponse
        {
            Personen = response.Personen.Select(p => GbaPersoon.MapFrom(p)).ToList()
        };
    }

    private static ZoekMetAdresseerbaarObjectIdentificatieResponse MapFrom(BRP.ZoekMetAdresseerbaarObjectIdentificatieResponse response)
    {
        return new ZoekMetAdresseerbaarObjectIdentificatieResponse
        {
            Personen = response.Personen.Select(p => p.Map()).ToList()
        };
    }

    private static ZoekMetGeslachtsnaamEnGeboortedatumResponse MapFrom(BRP.ZoekMetGeslachtsnaamEnGeboortedatumResponse response)
    {
        return new ZoekMetGeslachtsnaamEnGeboortedatumResponse
        {
            Personen = response.Personen.Select(p => p.Map()).ToList()
        };
    }

    private static ZoekMetNaamEnGemeenteVanInschrijvingResponse MapFrom(BRP.ZoekMetNaamEnGemeenteVanInschrijvingResponse response)
    {
        return new ZoekMetNaamEnGemeenteVanInschrijvingResponse
        {
            Personen = response.Personen.Select(p => p.Map()).ToList()
        };
    }

    private static ZoekMetNummeraanduidingIdentificatieResponse MapFrom(BRP.ZoekMetNummeraanduidingIdentificatieResponse response)
    {
        return new ZoekMetNummeraanduidingIdentificatieResponse
        {
            Personen = response.Personen.Select(p => p.Map()).ToList()
        };
    }

    private static ZoekMetPostcodeEnHuisnummerResponse MapFrom(BRP.ZoekMetPostcodeEnHuisnummerResponse response)
    {
        return new ZoekMetPostcodeEnHuisnummerResponse
        {
            Personen = response.Personen.Select(p => p.Map()).ToList()
        };
    }
    
    private static ZoekMetStraatHuisnummerEnGemeenteVanInschrijvingResponse MapFrom(BRP.ZoekMetStraatHuisnummerEnGemeenteVanInschrijvingResponse response)
    {
        return new ZoekMetStraatHuisnummerEnGemeenteVanInschrijvingResponse
        {
            Personen = response.Personen.Select(p => p.Map()).ToList()
        };
    }
}