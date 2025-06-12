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
}