using Microsoft.AspNetCore.Mvc;
using Rvig.HaalCentraalApi.Personen.ApiModels.BRP.Deprecated;
using Rvig.HaalCentraalApi.Shared.Validation;
using Rvig.HaalCentraalApi.Personen.Services;
using Rvig.HaalCentraalApi.Shared.Controllers;

namespace Rvig.HaalCentraalApi.Personen.Controllers;

[ApiController, Route("haalcentraal/api/brp"), ValidateContentTypeHeader]
public class GbaApiPersonenController : GbaApiBaseController
{
	private readonly IGbaPersonenApiService _gbaService;
	private readonly IGezagService _gezagService;
    private static bool GezagIsRequested(List<string> fields) =>
            fields.Any(field =>
                field.Contains("gezag", StringComparison.CurrentCultureIgnoreCase) &&
                !field.StartsWith("indicatieGezagMinderjarige"));

	public GbaApiPersonenController(IGbaPersonenApiService gbaService, IGezagService gezagService)
	{
		_gbaService = gbaService;
		_gezagService = gezagService;
	}


    [HttpPost]
	[Route("personen")]
	public async Task<IActionResult> GetPersonen([FromBody] PersonenQuery model)
	{
		await ValidateUnusableQueryParams(model);
		
		(PersonenQueryResponse personenResponse, List<long>? plIds, List<string>? bsns) = await _gbaService.GetPersonen(model);
        
		AddPlIdsToResponseHeaders(plIds);

		if(GezagIsRequested(model.Fields))
		{
			var personenResponseMetGezag = await _gezagService.GetGezag(personenResponse, model.Fields, bsns);
			return Ok(personenResponseMetGezag);
		}

		return Ok(personenResponse);
	}
}
