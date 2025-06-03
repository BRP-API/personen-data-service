using AutoMapper;
using Rvig.HaalCentraalApi.Personen.ApiModels.Gezag;
using Rvig.HaalCentraalApi.Personen.Generated.Deprecated;
using Rvig.HaalCentraalApi.Personen.Helpers;
using Rvig.HaalCentraalApi.Personen.Interfaces;
using Rvig.HaalCentraalApi.Personen.Mappers;
using Rvig.HaalCentraalApi.Personen.Repositories;
using Rvig.HaalCentraalApi.Shared.Interfaces;
using Rvig.HaalCentraalApi.Shared.Services;

namespace Rvig.HaalCentraalApi.Personen.Services
{
    public interface IGezagServiceDeprecated
    {
        public Task<IEnumerable<object>> GetGezag(List<string> fields, List<string?> bsns);
        public Task<List<GbaPersoon>> GetGezagPersonen(List<string> fields, IEnumerable<object> gezag);
        public void VerrijkPersonenMetGezag(List<string> fields, IEnumerable<object> persoonGezagsrelaties, List<GbaPersoon> gezagPersonen, (GbaPersoon persoon, long pl_id) x);
    }

    public class GezagServiceDeprecated : BaseApiService
    {
        private readonly IRepoGezagsrelatieDeprecated _gezagsrelatieRepo;
        private readonly IGezagPersonenService _gezagPersonenService;
        private readonly IMapper _mapper;

        private static bool GezagIsRequested(List<string> fields) =>
           fields.Any(field =>
               field.Contains("gezag", StringComparison.CurrentCultureIgnoreCase) &&
               !field.StartsWith("indicatieGezagMinderjarige"));

        public GezagServiceDeprecated(
            IGezagPersonenService getAndMapPersoonService,
            IDomeinTabellenRepo domeinTabellenRepo,
            IRepoGezagsrelatieDeprecated gezagsrelatieRepo,
            IMapper mapper)
        : base(domeinTabellenRepo)
        {
            _gezagsrelatieRepo = gezagsrelatieRepo;
            _gezagPersonenService = getAndMapPersoonService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Persoon>> GetGezagIfRequested(List<string> fields, List<string?> bsns)
        {
            if (GezagIsRequested(fields))
            {
                var response = (await _gezagsrelatieRepo.GetGezag(bsns!)) ?? new GezagResponse();
                return response.Personen.ToList();
            }
            return new List<Persoon>();
        }

        public async Task<List<GbaPersoon>> GetGezagPersonenIfRequested(List<string> fields, IEnumerable<object> gezag)
        {
            var gezagsrelaties = gezag
                .OfType<Persoon>() // Only V1 Personen
                .Where(p => p.Gezag != null).SelectMany(p => p.Gezag).ToList();

            if (GezagIsRequested(fields) && gezagsrelaties.Count != 0)
            {
                var gezagBsns = GezagHelper.GetGezagBsns(gezagsrelaties);

                return await GetGezagPersonen(gezagBsns);
            }

            return new List<GbaPersoon>();
        }

        public void VerrijkPersonenMetGezagIfRequested(List<string> fields, IEnumerable<object> persoonGezagsrelaties, List<GbaPersoon> gezagPersonen, (GbaPersoon persoon, long pl_id) x)
        {
            if (GezagIsRequested(fields) &&
                x.persoon is { Burgerservicenummer: { } bsn } &&
                !string.IsNullOrWhiteSpace(x.persoon.Burgerservicenummer))
            {
                var persoonGezagsrelatie = persoonGezagsrelaties
                    .OfType<Persoon>() // Only V1 Personen
                    .Where(pgr => pgr.Burgerservicenummer == bsn);

                if (persoonGezagsrelatie.Any())
                {
                    x.persoon.Gezag = new List<Generated.Deprecated.AbstractGezagsrelatie>();
                }
                foreach (var pg in persoonGezagsrelatie)
                {
                    var gezagResponse = new GezagResponse { Personen = new List<Persoon>() { pg } };
                    var gezag = GezagsrelatieMapperDeprecated.Map(gezagResponse, gezagPersonen);

                    foreach (var g in gezag)
                    {
                        if (g is Generated.Deprecated.AbstractGezagsrelatie gezagsrelatie)
                        {
                            x.persoon.Gezag.Add(gezagsrelatie);
                        }
                    }
                }
            }
        }

        private async Task<List<GbaPersoon>> GetGezagPersonen(List<string> gezagBsns)
        {
            var gezagPersonen = await _gezagPersonenService.GetGezagPersonen(gezagBsns); 

            var mappedGezagPersonen = _mapper.Map<List<GbaPersoon>>(gezagPersonen);

            return mappedGezagPersonen.ToList();
        }
    }
}
