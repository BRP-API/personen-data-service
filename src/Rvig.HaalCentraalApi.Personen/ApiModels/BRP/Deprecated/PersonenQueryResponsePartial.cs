namespace Rvig.HaalCentraalApi.Personen.ApiModels.BRP.Deprecated;

public static class QueryResponseMappers
{
    public static PersonenQueryResponse? Map(this BRP.PersonenQueryResponse personenResponse)
    {
        switch (personenResponse)
        {
            case BRP.RaadpleegMetBurgerservicenummerResponse response:
                return response.Map();
            case BRP.ZoekMetAdresseerbaarObjectIdentificatieResponse response:
                return response.Map();
            case BRP.ZoekMetGeslachtsnaamEnGeboortedatumResponse response:
                return response.Map();
            case BRP.ZoekMetNaamEnGemeenteVanInschrijvingResponse response:
                return response.Map();
            case BRP.ZoekMetNummeraanduidingIdentificatieResponse response:
                return response.Map();
            case BRP.ZoekMetPostcodeEnHuisnummerResponse response:
                return response.Map();
            case BRP.ZoekMetStraatHuisnummerEnGemeenteVanInschrijvingResponse response:
                return response.Map();
            default:
                return null;
        }
    }

    private static RaadpleegMetBurgerservicenummerResponse Map(this BRP.RaadpleegMetBurgerservicenummerResponse response)
    {
        return new RaadpleegMetBurgerservicenummerResponse
        {
            Personen = response.Personen.Select(p => p.Map()).ToList()
        };
    }

    private static ZoekMetAdresseerbaarObjectIdentificatieResponse Map(this BRP.ZoekMetAdresseerbaarObjectIdentificatieResponse response)
    {
        return new ZoekMetAdresseerbaarObjectIdentificatieResponse
        {
            Personen = response.Personen.Select(p => p.Map()).ToList()
        };
    }

    private static ZoekMetNaamEnGemeenteVanInschrijvingResponse Map(this BRP.ZoekMetNaamEnGemeenteVanInschrijvingResponse response)
    {
        return new ZoekMetNaamEnGemeenteVanInschrijvingResponse
        {
            Personen = response.Personen.Select(p => p.Map()).ToList()
        };
    }

    private static ZoekMetNummeraanduidingIdentificatieResponse Map(this BRP.ZoekMetNummeraanduidingIdentificatieResponse response)
    {
        return new ZoekMetNummeraanduidingIdentificatieResponse
        {
            Personen = response.Personen.Select(p => p.Map()).ToList()
        };
    }

    private static ZoekMetPostcodeEnHuisnummerResponse Map(this BRP.ZoekMetPostcodeEnHuisnummerResponse response)
    {
        return new ZoekMetPostcodeEnHuisnummerResponse
        {
            Personen = response.Personen.Select(p => p.Map()).ToList()
        };
    }
    
    private static ZoekMetStraatHuisnummerEnGemeenteVanInschrijvingResponse Map(this BRP.ZoekMetStraatHuisnummerEnGemeenteVanInschrijvingResponse response)
    {
        return new ZoekMetStraatHuisnummerEnGemeenteVanInschrijvingResponse
        {
            Personen = response.Personen.Select(p => p.Map()).ToList()
        };
    }
}