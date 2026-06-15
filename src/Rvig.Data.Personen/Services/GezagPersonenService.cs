using Rvig.Data.Personen.Mappers;
using Rvig.Data.Personen.Repositories;
using Rvig.HaalCentraalApi.Personen.ApiModels.BRP;
using Rvig.HaalCentraalApi.Personen.Interfaces;

namespace Rvig.Data.Personen.Services;
public class GezagPersonenService(IRvigPersoonRepo dbPersoonRepo, IRvIGDataPersonenMapper persoonMapper)
    : IGezagPersonenService
{
    public async Task<IEnumerable<GbaPersoon>> GetGezagPersonen(IEnumerable<string> burgerservicenummers)
    {
        var dbPersonen = await dbPersoonRepo.GetPersoonByBsns(burgerservicenummers,null,new List<string>() { "naam", "geslacht", "geboorte.datum" });

        return (await Task.WhenAll(dbPersonen.Select(async dbPersoon =>
            {
                if (dbPersoon == null)
                {
                    return default;
                }

                return (gbaPersoon: await persoonMapper.MapFrom(dbPersoon), dbPersoon.Persoon.pl_id);
            }))).Where(x => !x.gbaPersoon.Equals(default)).Select(x => x.gbaPersoon);
    }
}
