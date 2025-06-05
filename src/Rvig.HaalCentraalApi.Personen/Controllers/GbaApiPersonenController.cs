using Microsoft.AspNetCore.Mvc;
using Rvig.HaalCentraalApi.Personen.ApiModels.BRP.Deprecated;
using Rvig.HaalCentraalApi.Shared.Validation;
using Rvig.HaalCentraalApi.Personen.Services;
using Rvig.HaalCentraalApi.Shared.Controllers;
using Rvig.HaalCentraalApi.Personen.Helpers;

namespace Rvig.HaalCentraalApi.Personen.Controllers;

[ApiController, Route("haalcentraal/api/brp"), ValidateContentTypeHeader]
public class GbaApiPersonenController : GbaApiBaseController
{
	private readonly IGbaPersonenApiService _gbaService;
	private readonly IGezagService _gezagService;
    private readonly GezagService _newGezagService;

    public GbaApiPersonenController(IGbaPersonenApiService gbaService, IGezagService gezagService, GezagService newGezagService)
	{
		_gbaService = gbaService;
		_gezagService = gezagService;
		_newGezagService = newGezagService;
	}


	[HttpPost]
	[Route("personen")]
	public async Task<IActionResult> GetPersonen([FromBody] PersonenQuery model)
	{
		await ValidateUnusableQueryParams(model);

		var vraagtBsn = model.Fields.Contains("burgerservicenummer");

		(PersonenQueryResponse personenResponse, List<long>? plIds, List<string>? bsns) = await _gbaService.GetPersonen(model);

        personenResponse = await HandleGezagRequest(model, personenResponse, vraagtBsn, bsns);

		AddPlIdsToResponseHeaders(plIds);
        return Ok(personenResponse);
	}

    private async Task<PersonenQueryResponse> HandleGezagRequest(PersonenQuery model, PersonenQueryResponse personenResponse, bool vraagtBsn, List<string>? bsns)
    {
        if (GezagHelper.GezagIsRequested(model.Fields))
        {
            bool acceptNewGezag = Request.Headers.ContainsKey("Accept-Gezag-Version");

            if(!acceptNewGezag)
            {
                var personenResponseMetGezag = await _gezagService.GetGezag(personenResponse, model.Fields, bsns);
                if (!vraagtBsn)
                {
                    if (personenResponseMetGezag is RaadpleegMetBurgerservicenummerResponse response)
                    {
                        if (response != null)
                        {
                            foreach (var p in response.Personen)
                            {
                                p.Burgerservicenummer = null;
                            }
                        }
                    }
                    else if (personenResponseMetGezag is ZoekMetAdresseerbaarObjectIdentificatieResponse zoekResponse)
                    {
                        if (zoekResponse != null)
                        {
                            foreach (var p in zoekResponse.Personen)
                            {
                                p.Burgerservicenummer = null;
                            }
                        }
                    }
                }
                return personenResponseMetGezag;
            } 
            else
            {
                var mappedPersonenResponse = ApiModels.BRP.PersonenQueryResponse.MapFrom(personenResponse);
                var personenResponseMetNewGezag = await _newGezagService.GetGezag(mappedPersonenResponse, model.Fields, bsns);
                throw new NotImplementedException("New Gezag version is not implemented yet.");
            }

        }

        return personenResponse;
    }
}
