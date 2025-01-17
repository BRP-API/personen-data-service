using Microsoft.AspNetCore.Http;
using Rvig.Data.Personen.Mappers;
using Rvig.Data.Personen.Repositories;
using Rvig.HaalCentraalApi.Personen.ApiModels.BRP;
using Rvig.HaalCentraalApi.Personen.Interfaces;
using Rvig.HaalCentraalApi.Shared.Interfaces;
using Rvig.Data.Base.Postgres.Services;
using Rvig.Data.Base.Postgres.Repositories;

namespace Rvig.Data.Personen.Services;
public class GezagPersonenService : GetAndMapGbaServiceBase, IGezagPersonenService
{
	private readonly IRvigPersoonRepo _dbPersoonRepo;
    private readonly IRvIGDataPersonenMapper _persoonMapper;

    public GezagPersonenService(IAutorisationRepo autorisationRepo, IRvigPersoonRepo dbPersoonRepo, IRvigPersoonBeperktRepo dbPersoonBeperktRepo, IRvIGDataPersonenMapper persoonMapper,
		IHttpContextAccessor httpContextAccessor, IProtocolleringService protocolleringService)
		: base(httpContextAccessor, autorisationRepo, protocolleringService)
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
