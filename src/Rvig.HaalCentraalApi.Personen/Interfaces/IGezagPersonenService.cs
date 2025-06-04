using Rvig.HaalCentraalApi.Personen.ApiModels.BRP.Deprecated;

namespace Rvig.HaalCentraalApi.Personen.Interfaces;
public interface IGezagPersonenService
{
	Task<IEnumerable<GbaPersoon>> GetGezagPersonen(IEnumerable<string> burgerservicenummers);
}
