using Rvig.HaalCentraalApi.Personen.ApiModels.BRP;
using Rvig.HaalCentraalApi.Personen.ApiModels.Gezag;
using Rvig.HaalCentraalApi.Personen.Helpers;
using Rvig.HaalCentraalApi.Personen.Interfaces;
using Rvig.HaalCentraalApi.Personen.Repositories;
using Rvig.HaalCentraalApi.Shared.Interfaces;
using Rvig.HaalCentraalApi.Shared.Services;
using BRP = Rvig.HaalCentraalApi.Personen.ApiModels.BRP;

namespace Rvig.HaalCentraalApi.Personen.Services
{
    public class GezagService : BaseApiService
    {
        private readonly IRepoGezagsrelatie _gezagsrelatieRepo;

        public GezagService(
            IGezagPersonenService getAndMapPersoonService,
            IDomeinTabellenRepo domeinTabellenRepo,
            IRepoGezagsrelatie gezagsrelatieRepo)
        : base(domeinTabellenRepo)
        {
            _gezagsrelatieRepo = gezagsrelatieRepo;
        }

        public async Task<PersonenQueryResponse> GetGezag(PersonenQueryResponse personenResponse, List<string> fields, List<string>? bsns)
        {
            if (bsns == null || !bsns.Any())
            {
                return personenResponse;
            }

            var gezagPersonen = await GetGezagIfRequested(fields, bsns);

            if (gezagPersonen.Any())
            {
                if(personenResponse is RaadpleegMetBurgerservicenummerResponse r1)
                {
                    r1.Personen.ForEach(p =>
                    {
                        var gezag = gezagPersonen.FirstOrDefault(g => g.Burgerservicenummer == p.Burgerservicenummer);
                        if (gezag != null)
                        {
                            p.Gezag = BRP.Gezagsrelatie.MapFrom(gezag.Gezag);
                        }
                    });
                }
                else if (personenResponse is ZoekMetAdresseerbaarObjectIdentificatieResponse r2)
                {
                    r2.Personen.ForEach(p =>
                    {
                        var gezag = gezagPersonen.FirstOrDefault(g => g.Burgerservicenummer == p.Burgerservicenummer);
                        if (gezag != null)
                        {
                            p.Gezag = BRP.Gezagsrelatie.MapFrom(gezag.Gezag);
                        }
                    });
                }
            }
            return personenResponse;
        }

        public async Task<IEnumerable<Persoon>> GetGezagIfRequested(List<string> fields, List<string> bsns)
        {
            if(GezagHelper.GezagIsRequested(fields))
            {
                // Get gezagsrelaties from the repository
                GezagResponse response = (await _gezagsrelatieRepo.GetGezag(bsns)) ?? new GezagResponse();

                return response.Personen;
            }

            return new List<Persoon>();
        }
    }
}
