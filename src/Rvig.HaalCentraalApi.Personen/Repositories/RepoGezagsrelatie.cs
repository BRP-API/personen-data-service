using Microsoft.Extensions.Options;
using Rvig.Data.Base.WebApi.Options;
using Rvig.HaalCentraalApi.Shared.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.Net.Http;
using Rvig.Data.Base.Gezag.Repositories;
using Rvig.HaalCentraalApi.Personen.ApiModels.Gezag;

namespace Rvig.HaalCentraalApi.Personen.Repositories;

public interface IRepoGezagsrelatie
{
	Task<object?> GetGezagDynamic(IEnumerable<string> burgerservicenummer);
}
public class RepoGezagsrelatie : RepoWebApiBase, IRepoGezagsrelatie
{
	private readonly IHttpContextAccessor _httpContextAccessor;

    public RepoGezagsrelatie(IHttpContextAccessor httpContextAccessor, IOptions<WebApiOptions> webApiOptions, ILoggingHelper loggingHelper) : base(httpContextAccessor, webApiOptions, loggingHelper) => _httpContextAccessor = httpContextAccessor;

	public async Task<object?> GetGezagDynamic(IEnumerable<string> burgerservicenummer)
	{
		if (burgerservicenummer.IsNullOrEmpty())
		{
			return null;
		}

		var requestBody = new GezagRequest
		{
			Burgerservicenummer = burgerservicenummer.ToList()
		};

		var url = _webApiOptions.Value.Url + "/opvragenBevoegdheidTotGezag";

		var httpContext = _httpContextAccessor.HttpContext;
		var acceptGezagVersion = httpContext?.Request?.Headers["accept-gezag-version"].ToString();

		if (!string.IsNullOrEmpty(acceptGezagVersion) && acceptGezagVersion.Equals("2"))
		{
			// Use V2 DTO
			var headers = new List<(string Name, string Content)>
			{
				("Accept-Gezag-Version", "2")
			};

            return await GetResultFromHttpRequest<ApiModels.GezagV2.GezagResponse>(url, null, HttpMethod.Post, headers, requestBody);
		}
		else
		{
			// Use default DTO
			return await GetResultFromHttpRequest<GezagResponse>(url, null, HttpMethod.Post, null, requestBody);
		}
	}
}