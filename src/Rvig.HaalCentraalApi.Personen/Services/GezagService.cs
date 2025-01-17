using Microsoft.Extensions.Options;
using Rvig.HaalCentraalApi.Personen.ApiModels.BRP;
using Rvig.HaalCentraalApi.Personen.ApiModels.Gezag;
using Rvig.HaalCentraalApi.Personen.Helpers;
using Rvig.HaalCentraalApi.Personen.Interfaces;
using Rvig.HaalCentraalApi.Personen.Mappers;
using Rvig.HaalCentraalApi.Personen.Repositories;
using Rvig.HaalCentraalApi.Shared.Fields;
using Rvig.HaalCentraalApi.Shared.Helpers;
using Rvig.HaalCentraalApi.Shared.Interfaces;
using Rvig.HaalCentraalApi.Shared.Options;
using Rvig.HaalCentraalApi.Shared.Services;

namespace Rvig.HaalCentraalApi.Personen.Services
{
    public interface IGezagService
    {
        public Task<IEnumerable<Persoon>> GetGezagIfRequested(List<string> fields, List<string?> bsns);
        public Task<List<GbaPersoon>> GetGezagPersonenIfRequested(List<string> fields, IEnumerable<Persoon> gezag);
        public void VerrijkPersonenMetGezagIfRequested(List<string> fields, IEnumerable<Persoon> persoonGezagsrelaties, List<GbaPersoon> gezagPersonen, (IPersoonMetGezag persoon, long pl_id) x);
    }

    public class GezagService : BaseApiService, IGezagService
    {
        private readonly IRepoGezagsrelatie _gezagsrelatieRepo;
        private readonly IGezagPersonenService _gezagPersonenService;

        protected override FieldsSettings _fieldsSettings => throw new NotImplementedException();

        public GezagService(
            IGezagPersonenService getAndMapPersoonService,
            IDomeinTabellenRepo domeinTabellenRepo,
            IProtocolleringService protocolleringService,
            ILoggingHelper loggingHelper,
            IRepoGezagsrelatie gezagsrelatieRepo,
            IOptions<ProtocolleringAuthorizationOptions> protocolleringAuthorizationOptions)
        : base(domeinTabellenRepo, protocolleringService, loggingHelper, protocolleringAuthorizationOptions)
        {
            _gezagsrelatieRepo = gezagsrelatieRepo;
            _gezagPersonenService = getAndMapPersoonService;
        }

        public async Task<IEnumerable<Persoon>> GetGezagIfRequested(List<string> fields, List<string?> bsns)
        {
            if (GezagIsRequested(fields))
            {
                GezagResponse response = (await _gezagsrelatieRepo.GetGezag(bsns!)) ?? new GezagResponse();
                return response.Personen.ToList();
            }
            return new List<Persoon>();
        }

        public async Task<List<GbaPersoon>> GetGezagPersonenIfRequested(List<string> fields, IEnumerable<Persoon> gezag)
        {
            var gezagsrelaties = gezag.Where(p => p.Gezag != null).SelectMany(p => p.Gezag).ToList();

            if (GezagIsRequested(fields))
            {
                gezagsrelaties = gezagsrelaties.Where(gr => gr != null).ToList();

                if (gezagsrelaties.Count != 0)
                {
                    var gezagBsns = GezagHelper.GetGezagBsns(gezagsrelaties);
                    
                    return await GetGezagPersonen(gezagBsns);
                }
            }

            return new List<GbaPersoon>();
        }

        public void VerrijkPersonenMetGezagIfRequested(List<string> fields, IEnumerable<Persoon> persoonGezagsrelaties, List<GbaPersoon> gezagPersonen, (IPersoonMetGezag persoon, long pl_id) x)
        {
            if (GezagIsRequested(fields) &&
                !string.IsNullOrWhiteSpace(x.persoon.Burgerservicenummer))
            {
                var persoonGezagsrelatie = persoonGezagsrelaties
                    .Where(pgr => pgr.Burgerservicenummer == x.persoon.Burgerservicenummer);

                if (persoonGezagsrelatie.Any())
                {
                    x.persoon.Gezag = new List<ApiModels.BRP.AbstractGezagsrelatie>();
                }
                foreach (var pg in persoonGezagsrelatie)
                {
                    var gezagResponse = new GezagResponse { Personen = new List<Persoon>() { pg } };
                    var gezag = GezagsrelatieMapper.Map(gezagResponse, gezagPersonen);

                    x.persoon.Gezag?.AddRange(gezag);
                }
            }
        }
        
        private static bool GezagIsRequested(List<string> fields) =>
            fields.Any(field =>
                field.Contains("gezag", StringComparison.CurrentCultureIgnoreCase) &&
                !field.StartsWith("indicatieGezagMinderjarige"));

        private async Task<List<GbaPersoon>> GetGezagPersonen(List<string> gezagBsns)
        {
            var gezagPersonen = await _gezagPersonenService.GetGezagPersonen(gezagBsns);

            return gezagPersonen.ToList();
        }

    }
}
