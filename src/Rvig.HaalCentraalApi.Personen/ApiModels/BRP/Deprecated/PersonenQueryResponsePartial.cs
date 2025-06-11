
namespace Rvig.HaalCentraalApi.Personen.ApiModels.BRP.Deprecated;

public partial class PersonenQueryResponse
{
    public static PersonenQueryResponse MapFrom(BRP.PersonenQueryResponse personenResponse)
    {
        // TODO: Implement mapping

        return new RaadpleegMetBurgerservicenummerResponse();
    }
}