using AutoMapper;
using GezagDtos = Rvig.HaalCentraalApi.Gezag.Generated;
using BrpDtos = Rvig.HaalCentraalApi.Personen.Generated;

namespace Rvig.HaalCentraalApi.Personen.Profiles;
public class GezagProfile : Profile
{
    public GezagProfile()
    {
        CreateMap<GezagDtos.Gezagsrelatie, BrpDtos.Gezagsrelatie?>().ConvertUsing<GezagConverter>();
    }
}

public class GezagConverter : ITypeConverter<GezagDtos.Gezagsrelatie, BrpDtos.Gezagsrelatie?>
{
    public BrpDtos.Gezagsrelatie? Convert(GezagDtos.Gezagsrelatie source, BrpDtos.Gezagsrelatie? destination, ResolutionContext context)
    {
        return source switch
        {
            GezagDtos.EenhoofdigOuderlijkGezag => context.Mapper.Map<BrpDtos.EenhoofdigOuderlijkGezag>(source),
            GezagDtos.GezamenlijkOuderlijkGezag => context.Mapper.Map<BrpDtos.GezamenlijkOuderlijkGezag>(source),
            GezagDtos.GezamenlijkGezag => context.Mapper.Map<BrpDtos.GezamenlijkGezag>(source),
            GezagDtos.Voogdij => context.Mapper.Map<BrpDtos.Voogdij>(source),
            GezagDtos.TijdelijkGeenGezag => context.Mapper.Map<BrpDtos.TijdelijkGeenGezag>(source),
            GezagDtos.GezagNietTeBepalen => context.Mapper.Map<BrpDtos.GezagNietTeBepalen>(source),
            _ => null
        };
    }
}