using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rvig.Base.App.ResponseModels.HealthCheck;
using Rvig.Base.App.Services;

namespace Rvig.Base.App.Controllers;

[AllowAnonymous, ApiController, Route("haalcentraal/api/health")]
public class HealthCheckController(IHealthCheckApiService healthCheckApiService) : ControllerBase
{
    [HttpGet]
	[Route("check")]
	public Task<HealthCheckResult> Check()
	{
		return healthCheckApiService.CheckConnections();
	}
}
