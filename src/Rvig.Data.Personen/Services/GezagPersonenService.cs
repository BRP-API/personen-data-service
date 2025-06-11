using Rvig.Data.Personen.Mappers;
using Rvig.Data.Personen.Repositories;
using Rvig.HaalCentraalApi.Personen.ApiModels.BRP;
using Rvig.HaalCentraalApi.Personen.Interfaces;
using Rvig.Data.Base.Postgres.Services;

namespace Rvig.Data.Personen.Services;
public class GezagPersonenService : GetAndMapGbaServiceBase, IGezagPersonenService
{
	private readonly IRvigPersoonRepo _dbPersoonRepo;
    private readonly IRvIGDataPersonenMapper _persoonMapper;

    public GezagPersonenService(IRvigPersoonRepo dbPersoonRepo, IRvIGDataPersonenMapper persoonMapper)
	{
		_dbPersoonRepo = dbPersoonRepo;
        _persoonMapper = persoonMapper;
    }

    public async Task<IEnumerable<GbaPersoon>> GetGezagPersonen(IEnumerable<string> burgerservicenummers)
    {
        var dbPersonen = await _dbPersoonRepo.GetPersoonByBsns(burgerservicenummers,null,new List<string>() { "naam", "geslacht", "geboorte.datum" });

        return (await Task.WhenAll(dbPersonen.Select(async dbPersoon =>
            {
                if (dbPersoon == null)
                {
                    return default;
                }

                return (gbaPersoon: await _persoonMapper.MapFrom(dbPersoon), dbPersoon.Persoon.pl_id);
            }))).Where(x => !x.gbaPersoon.Equals(default)).Select(x => x.gbaPersoon);
    }
}
