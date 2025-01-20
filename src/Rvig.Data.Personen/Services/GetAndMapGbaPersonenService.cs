using Rvig.Data.Personen.Mappers;
using Rvig.Data.Personen.Repositories;
using Rvig.HaalCentraalApi.Personen.ApiModels.BRP;
using Rvig.HaalCentraalApi.Shared.Exceptions;
using Rvig.HaalCentraalApi.Personen.RequestModels.BRP;
using Rvig.HaalCentraalApi.Personen.Interfaces;
using Rvig.Data.Base.Postgres.Services;

namespace Rvig.Data.Personen.Services;
public class GetAndMapGbaPersonenService : GetAndMapGbaServiceBase, IGetAndMapGbaPersonenService
{
	private readonly IRvigPersoonRepo _dbPersoonRepo;
	private readonly IRvigPersoonBeperktRepo _dbPersoonBeperktRepo;
	private readonly IRvIGDataPersonenMapper _persoonMapper;

    public GetAndMapGbaPersonenService(IRvigPersoonRepo dbPersoonRepo, IRvigPersoonBeperktRepo dbPersoonBeperktRepo, IRvIGDataPersonenMapper persoonMapper)
	{
		_dbPersoonRepo = dbPersoonRepo;
		_dbPersoonBeperktRepo = dbPersoonBeperktRepo;
		_persoonMapper = persoonMapper;
	}

	public async Task<(IEnumerable<(GbaPersoon persoon, long pl_id)>? personenPlIds, int afnemerCode)> GetPersonenMapByBsns(IEnumerable<string>? burgerservicenummers, string? gemeenteVanInschrijving, List<string> fields, bool checkAuthorization)
	{

		// It is impossible to have an empty or null array of bsns because the API request models already validate this and reject all non valid values.
		var dbPersonen = await _dbPersoonRepo.GetPersoonByBsns(burgerservicenummers!, gemeenteVanInschrijving, fields);
		var personenPlIds = (await Task.WhenAll(dbPersonen.Select(async dbPersoon =>
		{
			if (dbPersoon == null)
			{
				return default;
			}
			var persoonFiltered = dbPersoon;

			if (persoonFiltered != null)
			{
				return (gbaPersoon: await _persoonMapper.MapFrom(persoonFiltered), dbPersoon.Persoon.pl_id);
			}

			return default;
		})))
        .Where(x => !x.gbaPersoon.Equals(default));

		if (!personenPlIds.Any())
		{
			return default;
		}

        // Check above already makes sure that a null value in the list isn't possible.
        return (personenPlIds.Select(x => (x.gbaPersoon, x.pl_id)), 0);
    }

	public async Task<(IEnumerable<(T persoon, long pl_id)>? personenPlIds, int afnemerCode)> GetMapZoekPersonen<T>(PersonenQuery model, List<string> fields, bool checkAuthorization) where T : GbaPersoonBeperkt
    {
        return model switch
        {
            ZoekMetGeslachtsnaamEnGeboortedatum => await GetMapZoekPersonenBase<T>(model, fields),
            ZoekMetNaamEnGemeenteVanInschrijving => await GetMapZoekPersonenBase<T>(model, fields),
            ZoekMetNummeraanduidingIdentificatie => await GetMapZoekPersonenBase<T>(model, fields),
            ZoekMetPostcodeEnHuisnummer => await GetMapZoekPersonenBase<T>(model, fields),
            ZoekMetStraatHuisnummerEnGemeenteVanInschrijving => await GetMapZoekPersonenBase<T>(model, fields),
			ZoekMetAdresseerbaarObjectIdentificatie => await GetMapZoekPersonenBase<T>(model, fields),
			_ => throw new CustomInvalidOperationException($"Onbekend type query: {model}"),
        };
    }

	/// <summary>
	/// Get personen on search criteria.
	/// </summary>
	/// <param name="model"></param>
	/// <param name="checkAdresVraagBevoegdheid"></param>
	/// <returns>List of persons with restricted data.</returns>
	/// <exception cref="AuthorizationException"></exception>
	private async Task<(IEnumerable<(T persoon, long pl_id)>? personenPlIds, int afnemerCode)> GetMapZoekPersonenBase<T>(PersonenQuery model, List<string> fields) where T : GbaPersoonBeperkt
	{
		var dbPersonen = await _dbPersoonBeperktRepo.SearchPersonen(model, fields);
		var personenPlIds = (await Task.WhenAll(dbPersonen.Select(async dbPersoon =>
		{
			if (dbPersoon == null)
			{
				return default;
			}
			var persoonFiltered = dbPersoon;

			if (persoonFiltered != null)
			{
				return (gbaPersoon: await _persoonMapper.MapGbaPersoonBeperkt<T>(persoonFiltered), dbPersoon.Persoon.pl_id);
			}

			return default;
		})))
		.Where(x => !x.gbaPersoon.Equals(default));

		if (!personenPlIds.Any())
		{
			return default;
		}

		// Check above already makes sure that a null value in the list isn't possible.
		return (personenPlIds.Select(x => (x.gbaPersoon, x.pl_id)), 0);
	}
}
