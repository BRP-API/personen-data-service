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

		var vraagtBsn = model.Fields.Contains("burgerservicenummer");

		(PersonenQueryResponse personenResponse, List<long>? plIds, List<string>? bsns) = await _gbaService.GetPersonen(model);

		AddPlIdsToResponseHeaders(plIds);

		if (GezagHelper.GezagIsRequested(model.Fields))
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
					return Ok(personenResponseMetGezag);
				} else if (personenResponseMetGezag is ZoekMetAdresseerbaarObjectIdentificatieResponse zoekResponse)
				{
					if (zoekResponse != null)
					{
						foreach (var p in zoekResponse.Personen)
						{
							p.Burgerservicenummer = null;
						}
					}
					return Ok(personenResponseMetGezag);
                }
            }

			return Ok(personenResponse);
		}
	}
}
