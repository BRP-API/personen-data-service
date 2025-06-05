using Rvig.HaalCentraalApi.Personen.ApiModels.BRP.Deprecated;
using Rvig.HaalCentraalApi.Personen.ApiModels.Gezag.Deprecated;

namespace Rvig.HaalCentraalApi.Personen.Services
{
    public interface IGezagService
    {
        public Task<PersonenQueryResponse> GetGezag(PersonenQueryResponse personenResponse, List<string> fields, List<string>? bsns);
        void VerrijkPersonenMetGezagIfRequested(List<string> fields, IEnumerable<Persoon> persoonGezagsrelaties, List<GbaPersoon> gezagPersonen, (IPersoonMetGezag persoon, long pl_id) x);
    }
}
