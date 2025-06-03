using AutoMapper;

namespace Rvig.HaalCentraalApi.Personen.Profiles;

public class GbaPersoonProfile : Profile
{
    public GbaPersoonProfile()
    {
        CreateMap<Generated.Deprecated.GbaPersoon, ApiModels.BRP.GbaPersoon>(); // map deprecated GbaPersoon

        CreateMap<Generated.Common.GbaPersoonBeperkt, ApiModels.BRP.GbaPersoonBeperkt>(); // gbaPersoonBeperkt is een common DTO, er is hier geen onderscheid tussen de versies

        CreateMap<Generated.Deprecated.GbaGezagPersoonBeperkt, ApiModels.BRP.GbaGezagPersoonBeperkt>(); // map deprecated GbaGezagPersoonBeperkt
    }
}
