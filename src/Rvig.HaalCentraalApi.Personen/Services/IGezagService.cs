using Rvig.HaalCentraalApi.Personen.ApiModels.BRP.Deprecated;

namespace Rvig.HaalCentraalApi.Personen.Services
{
    public interface IGezagService
    {
        public Task<PersonenQueryResponse> GetGezag(PersonenQueryResponse personenResponse, List<string> fields, List<string>? bsns);
    }
}
