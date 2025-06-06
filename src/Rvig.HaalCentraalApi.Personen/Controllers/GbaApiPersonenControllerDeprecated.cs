using Microsoft.AspNetCore.Mvc;
using Rvig.HaalCentraalApi.Personen.ApiModels.BRP.Deprecated;
using Rvig.HaalCentraalApi.Shared.Validation;
using Rvig.HaalCentraalApi.Personen.Services;
using Rvig.HaalCentraalApi.Shared.Controllers;
using Rvig.HaalCentraalApi.Personen.Helpers;
using Elastic.CommonSchema;

namespace Rvig.HaalCentraalApi.Personen.Controllers;

[ApiController, Route("haalcentraal/api/brp"), ValidateContentTypeHeader]
public class GbaApiPersonenControllerDeprecated : GbaApiBaseController
{
    private readonly IGbaPersonenApiService _gbaService;
    private readonly IGezagService _gezagService;

    public GbaApiPersonenControllerDeprecated(IGbaPersonenApiService gbaService, IGezagService gezagService)
    {
        _gbaService = gbaService;
        _gezagService = gezagService;
    }


    [HttpPost]
    [Route("personen")]
    public async Task<IActionResult> GetPersonen([FromBody] PersonenQuery model)
    {
        await ValidateUnusableQueryParams(model);

        var vraagtBsn = model.Fields.Contains("burgerservicenummer");

        (PersonenQueryResponse personenResponse, List<long>? plIds, List<string>? bsns) = await _gbaService.GetPersonen(model);

        personenResponse = await HandleGezagRequest(model, personenResponse, vraagtBsn, bsns); // deprecated version

        AddPlIdsToResponseHeaders(plIds);
        return Ok(personenResponse);
    }

    private async Task<PersonenQueryResponse> HandleGezagRequest(PersonenQuery model, PersonenQueryResponse personenResponse, bool vraagtBsn, List<string>? bsns)
    {
        if (!GezagHelper.GezagIsRequested(model.Fields)) return personenResponse;

        var personenResponseMetGezag = await _gezagService.GetGezag(personenResponse, model.Fields, bsns);
        if (!vraagtBsn)
        {
            if (personenResponseMetGezag is RaadpleegMetBurgerservicenummerResponse response && response != null)
            {
                foreach (var p in response.Personen)
                {
                    p.Burgerservicenummer = null;
                }
            }
            else if (personenResponseMetGezag is ZoekMetAdresseerbaarObjectIdentificatieResponse zoekResponse && zoekResponse != null)
            {
                foreach (var p in zoekResponse.Personen)
                {
                    p.Burgerservicenummer = null;
                }
            }
        }
        return personenResponseMetGezag;
    }
}
