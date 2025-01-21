using Rvig.HaalCentraalApi.Personen.ApiModels.BRP;
using Rvig.HaalCentraalApi.Personen.RequestModels.BRP;

namespace Rvig.HaalCentraalApi.Personen.Interfaces;
public interface IGetAndMapGbaPersonenService
{
	Task<(IEnumerable<(GbaPersoon persoon, long pl_id)>? personenPlIds, int afnemerCode)> GetPersonenMapByBsns(IEnumerable<string>? burgerservicenummers, string? gemeenteVanInschrijving, List<string> fields);
	Task<(IEnumerable<(T persoon, long pl_id)>? personenPlIds, int afnemerCode)> GetMapZoekPersonen<T>(PersonenQuery model, List<string> fields) where T : GbaPersoonBeperkt;
}
