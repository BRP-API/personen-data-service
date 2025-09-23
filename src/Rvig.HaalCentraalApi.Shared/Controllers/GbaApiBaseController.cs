using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rvig.HaalCentraalApi.Shared.Validation;

namespace Rvig.HaalCentraalApi.Shared.Controllers;

public class GbaApiBaseController : ControllerBase
{
	protected Task ValidateUnusableQueryParams(object model)
	{
		return ApiCallValidator.ValidateUnusableQueryParams(model, HttpContext);
	}

    protected void AddPlIdsToResponseHeaders(List<long>? plIds)
    {
        if (plIds?.Any() == true)
        {
            Response.Headers.Append("x-geleverde-pls", string.Join(",", plIds.OrderBy(plId => plId)));
        }
    }
}