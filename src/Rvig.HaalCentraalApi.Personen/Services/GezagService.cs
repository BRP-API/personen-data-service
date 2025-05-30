using Rvig.HaalCentraalApi.Gezag.Generated;
using Rvig.HaalCentraalApi.Personen.Repositories;
using Rvig.HaalCentraalApi.Shared.Interfaces;
using Rvig.HaalCentraalApi.Shared.Services;

namespace Rvig.HaalCentraalApi.Personen.Services
{
    public interface IGezagService
    {
        public Task<IEnumerable<Persoon>> GetGezagIfRequested(List<string> fields, List<string?> bsns);
    }

    public class GezagService : BaseApiService, IGezagService
    {
        private readonly IRepoGezagsrelatie _gezagsrelatieRepo;
        private static bool GezagIsRequested(List<string> fields) =>
           fields.Any(field =>
               field.Contains("gezag", StringComparison.CurrentCultureIgnoreCase) &&
               !field.StartsWith("indicatieGezagMinderjarige"));

        public GezagService(
            IDomeinTabellenRepo domeinTabellenRepo,
            IRepoGezagsrelatie gezagsrelatieRepo)
        : base(domeinTabellenRepo)
        {
            _gezagsrelatieRepo = gezagsrelatieRepo;
        }

        public async Task<IEnumerable<Persoon>> GetGezagIfRequested(List<string> fields, List<string?> bsns)
        {
            if (GezagIsRequested(fields))
            {
                var response = await _gezagsrelatieRepo.GetGezag(bsns!) ?? new GezagResponse();
                return response.Personen.ToList();
            }
            return new List<Persoon>();
        }
    }
}
