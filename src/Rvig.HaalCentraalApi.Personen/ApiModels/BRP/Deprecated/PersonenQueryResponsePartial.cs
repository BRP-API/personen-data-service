
namespace Rvig.HaalCentraalApi.Personen.ApiModels.BRP.Deprecated;

public partial class PersonenQueryResponse
{
    public static PersonenQueryResponse MapFrom(BRP.PersonenQueryResponse personenResponse)
    {
        switch(personenResponse)
        {
            case BRP.RaadpleegMetBurgerservicenummerResponse response:
                return MapFrom(response);
            default:
                return new RaadpleegMetBurgerservicenummerResponse(); ;
        }
    }

    private static RaadpleegMetBurgerservicenummerResponse MapFrom(BRP.RaadpleegMetBurgerservicenummerResponse response)
    {
        return new RaadpleegMetBurgerservicenummerResponse
        {
            Personen = response.Personen.Select(p => GbaPersoon.MapFrom(p)).ToList(),
        };
    }
}