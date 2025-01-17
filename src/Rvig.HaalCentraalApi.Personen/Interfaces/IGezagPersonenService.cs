using Rvig.HaalCentraalApi.Personen.ApiModels.BRP;

namespace Rvig.HaalCentraalApi.Personen.Interfaces;
public interface IGezagPersonenService
{
	Task<IEnumerable<GbaPersoon>> GetGezagPersonen(IEnumerable<string> burgerservicenummers);
}
