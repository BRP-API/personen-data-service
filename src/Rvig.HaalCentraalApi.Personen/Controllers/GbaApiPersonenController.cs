using Microsoft.AspNetCore.Mvc;
using Rvig.HaalCentraalApi.Personen.ApiModels.BRP;
using Rvig.HaalCentraalApi.Personen.RequestModels.BRP;
using Rvig.HaalCentraalApi.Shared.Validation;
using Rvig.HaalCentraalApi.Personen.Services;
using Rvig.HaalCentraalApi.Shared.Controllers;

namespace Rvig.HaalCentraalApi.Personen.Controllers;

[ApiController, Route("haalcentraal/api/brp"), ValidateContentTypeHeader]
public class GbaApiPersonenController : GbaApiBaseController
{
	private readonly IGbaPersonenApiService _gbaService;

	public GbaApiPersonenController(IGbaPersonenApiService gbaService)
	{
		_gbaService = gbaService;
	}

	[HttpPost]
	[Route("personen")]
	public async Task<PersonenQueryResponse> GetPersonen([FromBody] PersonenQuery model)
	{
		await ValidateUnusableQueryParams(model);
		(PersonenQueryResponse personenResponse, List<long>? plIds) = await _gbaService.GetPersonen(model);
		AddPlIdsToResponseHeaders(plIds);

		return personenResponse;
	}
}
