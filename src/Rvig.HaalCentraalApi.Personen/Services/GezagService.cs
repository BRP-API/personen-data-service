using Rvig.HaalCentraalApi.Personen.ApiModels.BRP;
using Rvig.HaalCentraalApi.Personen.ApiModels.Gezag.Deprecated;
using Rvig.HaalCentraalApi.Personen.Helpers;
using Rvig.HaalCentraalApi.Personen.Interfaces;
using Rvig.HaalCentraalApi.Personen.Repositories;
using Rvig.HaalCentraalApi.Shared.Interfaces;
using Rvig.HaalCentraalApi.Shared.Services;

namespace Rvig.HaalCentraalApi.Personen.Services
{
    public class GezagService : BaseApiService
    {
        private readonly IRepoGezagsrelatie _gezagsrelatieRepo;
        private readonly IGezagPersonenService _gezagPersonenService;

        public GezagService(
            IGezagPersonenService getAndMapPersoonService,
            IDomeinTabellenRepo domeinTabellenRepo,
            IRepoGezagsrelatie gezagsrelatieRepo)
        : base(domeinTabellenRepo)
        {
            _gezagsrelatieRepo = gezagsrelatieRepo;
            _gezagPersonenService = getAndMapPersoonService;
        }

        public async Task<PersonenQueryResponse> GetGezag(PersonenQueryResponse personenResponse, List<string> fields, List<string>? bsns)
        {
            if (bsns == null || !bsns.Any())
            {
                return personenResponse;
            }
            var gezagPersonen = await GetGezagDeprecatedIfRequested(fields, bsns);
            if (gezagPersonen.Any())
            {
                if(personenResponse is RaadpleegMetBurgerservicenummerResponse r1)
                {
                    await VerrijkPersonenMetGezag(r1, fields, bsns);
                }
                else if (personenResponse is ZoekMetAdresseerbaarObjectIdentificatieResponse r2)
                {
                    await VerrijkPersonenMetGezag(r2, fields, bsns);
                }
            }
            return personenResponse;
        }

        private async Task VerrijkPersonenMetGezag(RaadpleegMetBurgerservicenummerResponse response, List<string> fields, List<string> bsns)
        {
            
            var gezag = await GetGezagDeprecatedIfRequested(fields, bsns);

            // Get gezag for personen in response
        }

        private async Task VerrijkPersonenMetGezag(ZoekMetAdresseerbaarObjectIdentificatieResponse response, List<string> fields, List<string> bsns)
        {
            var gezag = await GetGezagDeprecatedIfRequested(fields, bsns);

            // Get gezag for personen in response
        }

        public async Task<IEnumerable<Persoon>> GetGezagDeprecatedIfRequested(List<string> fields, List<string> bsns)
        {
            if(GezagHelper.GezagIsRequested(fields))
            {

                GezagResponse response = (await _gezagsrelatieRepo.GetGezag(bsns!)) ?? new GezagResponse();
                return response.Personen.ToList();
            }
            
            return new List<Persoon>();
        }
    }
}
