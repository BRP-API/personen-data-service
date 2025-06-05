using Rvig.HaalCentraalApi.Personen.ApiModels.BRP.Deprecated;
using Rvig.HaalCentraalApi.Personen.ApiModels.Gezag.Deprecated;
using Rvig.HaalCentraalApi.Personen.Helpers;
using Rvig.HaalCentraalApi.Personen.Interfaces;
using Rvig.HaalCentraalApi.Personen.Mappers;
using Rvig.HaalCentraalApi.Personen.Repositories;
using Rvig.HaalCentraalApi.Shared.Interfaces;
using Rvig.HaalCentraalApi.Shared.Services;

namespace Rvig.HaalCentraalApi.Personen.Services
{
    public class GezagServiceDeprecated : BaseApiService, IGezagService
    {
        private readonly IRepoGezagsrelatie _gezagsrelatieRepo;
        private readonly IGezagPersonenService _gezagPersonenService;

        public GezagServiceDeprecated(
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
            var gp = await GetGezagPersonenIfRequested(fields, gezag);
            foreach (var x in response.Personen.Select((persoon, pl_id) => (persoon, pl_id)))
            {
                if (x.persoon is IPersoonMetGezag)
                {
                    VerrijkPersonenMetGezagIfRequested(fields, gezag, gp, x);
                }
            }
        }

        private async Task VerrijkPersonenMetGezag(ZoekMetAdresseerbaarObjectIdentificatieResponse response, List<string> fields, List<string> bsns)
        {
            var gezag = await GetGezagDeprecatedIfRequested(fields, bsns);
            var gp = await GetGezagPersonenIfRequested(fields, gezag);
            foreach (var x in response.Personen.Select((persoon, pl_id) => (persoon, pl_id)))
            {
                if (x.persoon is IPersoonMetGezag)
                {
                    VerrijkPersonenMetGezagIfRequested(fields, gezag, gp, x);
                }
            }
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

        public async Task<List<GbaPersoon>> GetGezagPersonenIfRequested(List<string> fields, IEnumerable<Persoon> gezag)
        {
            var gezagsrelaties = gezag.Where(p => p.Gezag != null).SelectMany(p => p.Gezag).ToList();

            if (GezagHelper.GezagIsRequested(fields) && gezagsrelaties.Count != 0)
            {
                var gezagBsns = GezagHelper.GetGezagBsns(gezagsrelaties);

                return await GetGezagPersonen(gezagBsns);
            }

            return new List<GbaPersoon>();
        }

        public void VerrijkPersonenMetGezagIfRequested(List<string> fields, IEnumerable<Persoon> persoonGezagsrelaties, List<GbaPersoon> gezagPersonen, (IPersoonMetGezag persoon, long pl_id) x)
        {
            if (GezagHelper.GezagIsRequested(fields) &&
                !string.IsNullOrWhiteSpace(x.persoon.Burgerservicenummer))
            {
                var persoonGezagsrelatie = persoonGezagsrelaties
                    .Where(pgr => pgr.Burgerservicenummer == x.persoon.Burgerservicenummer);

                if (persoonGezagsrelatie.Any())
                {
                    x.persoon.Gezag = new List<ApiModels.BRP.Deprecated.AbstractGezagsrelatie>();
                }
                foreach (var pg in persoonGezagsrelatie)
                {
                    var gezagResponse = new GezagResponse { Personen = new List<Persoon>() { pg } };
                    var gezag = GezagsrelatieMapper.Map(gezagResponse, gezagPersonen);

                    x.persoon.Gezag!.AddRange(gezag);
                }
            }
        }

        private async Task<List<GbaPersoon>> GetGezagPersonen(List<string> gezagBsns)
        {
            var gezagPersonen = await _gezagPersonenService.GetGezagPersonen(gezagBsns);

            return gezagPersonen.ToList();
        }

       
    }
}
