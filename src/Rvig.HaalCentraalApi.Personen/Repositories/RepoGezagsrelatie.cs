using Microsoft.Extensions.Options;
using Rvig.Data.Base.WebApi.Options;
using Rvig.HaalCentraalApi.Shared.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.Net.Http;
using Rvig.Data.Base.Gezag.Repositories;
using Rvig.HaalCentraalApi.Personen.ApiModels.Gezag.Deprecated;

namespace Rvig.HaalCentraalApi.Personen.Repositories;

public interface IRepoGezagsrelatie
{
	Task<GezagResponse?> GetGezag(IEnumerable<string> burgerservicenummer);
}
public class RepoGezagsrelatie : RepoWebApiBase, IRepoGezagsrelatie
{
	public RepoGezagsrelatie(IHttpContextAccessor httpContextAccessor, IOptions<WebApiOptions> webApiOptions, ILoggingHelper loggingHelper) : base(httpContextAccessor, webApiOptions, loggingHelper)
	{
	}

	public Task<GezagResponse?> GetGezag(IEnumerable<string> burgerservicenummer)
	{
		if (burgerservicenummer.IsNullOrEmpty())
		{
			return Task.FromResult<GezagResponse?>(null);
		}
		var requestBody = new GezagRequest
		{
			Burgerservicenummer = burgerservicenummer.ToList()
		};
        // http://localhost:8080/api/v1/opvragenBevoegdheidTotGezag
        var url = _webApiOptions.Value.Url + "/opvragenBevoegdheidTotGezag";
		return GetResultFromHttpRequest<GezagResponse>(url, null, HttpMethod.Post, null, requestBody);
	}
}