using Rvig.HaalCentraalApi.Shared.Exceptions;

namespace Rvig.HaalCentraalApi.Personen.ApiModels.BRP.Deprecated;

public static class QueryResponseMappers
{
    public static PersonenQueryResponse Map(this BRP.PersonenQueryResponse personenResponse)
    {
        return personenResponse switch
        {
            BRP.RaadpleegMetBurgerservicenummerResponse response => response.Map(),
            BRP.ZoekMetAdresseerbaarObjectIdentificatieResponse response => response.Map(),
            BRP.ZoekMetGeslachtsnaamEnGeboortedatumResponse response => response.Map(),
            BRP.ZoekMetNaamEnGemeenteVanInschrijvingResponse response => response.Map(),
            BRP.ZoekMetNummeraanduidingIdentificatieResponse response => response.Map(),
            BRP.ZoekMetPostcodeEnHuisnummerResponse response => response.Map(),
            BRP.ZoekMetStraatHuisnummerEnGemeenteVanInschrijvingResponse response => response.Map(),
            _ => throw new CustomInvalidOperationException($"Onbekend type response: {personenResponse}"),
        };
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

    private static ZoekMetGeslachtsnaamEnGeboortedatumResponse Map(this BRP.ZoekMetGeslachtsnaamEnGeboortedatumResponse response)
    {
        return new ZoekMetGeslachtsnaamEnGeboortedatumResponse
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