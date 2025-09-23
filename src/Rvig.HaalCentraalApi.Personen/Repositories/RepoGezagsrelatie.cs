using Microsoft.Extensions.Options;
using Rvig.Data.Base.WebApi.Options;
using Rvig.HaalCentraalApi.Shared.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.Net.Http;
using Rvig.Data.Base.Gezag.Repositories;
using Deprecated = Rvig.HaalCentraalApi.Personen.ApiModels.Gezag.Deprecated;
using Rvig.HaalCentraalApi.Personen.ApiModels.Gezag;

namespace Rvig.HaalCentraalApi.Personen.Repositories;

public interface IRepoGezagsrelatie
{
	Task<Deprecated.GezagResponse?> GetGezagDeprecated(IEnumerable<string> burgerservicenummer);
	Task<GezagResponse?> GetGezag(IEnumerable<string> burgerservicenummer);
}
public class RepoGezagsrelatie : RepoWebApiBase, IRepoGezagsrelatie
{
	public RepoGezagsrelatie(IHttpContextAccessor httpContextAccessor, IOptions<WebApiOptions> webApiOptions, ILoggingHelper loggingHelper) : base(httpContextAccessor, webApiOptions, loggingHelper)
	{
	}

	public Task<Deprecated.GezagResponse?> GetGezagDeprecated(IEnumerable<string> burgerservicenummer)
	{
		if (burgerservicenummer.IsNullOrEmpty())
		{
			return Task.FromResult<Deprecated.GezagResponse?>(null);
		}
		var requestBody = new Deprecated.GezagRequest
		{
			Burgerservicenummer = burgerservicenummer.ToList()
		};
        // http://localhost:8080/api/v1/opvragenBevoegdheidTotGezag
        var url = _webApiOptions.Value.Url + "/opvragenBevoegdheidTotGezag";
		return GetResultFromHttpRequest<Deprecated.GezagResponse>(url, null, HttpMethod.Post, null, requestBody);
	}

	public Task<GezagResponse?> GetGezag(IEnumerable<string> burgerservicenummer)
	{
		if (burgerservicenummer.IsNullOrEmpty())
		{
			return Task.FromResult<GezagResponse?>(null);
		}
		var requestHeaders = new List<(string Name, string Content)>
		{
			("Accept-Gezag-Version", "2")
		};
        var requestBody = new GezagRequest
		{
			Burgerservicenummer = burgerservicenummer.ToList()
		};
		// http://localhost:8080/api/v1/opvragenBevoegdheidTotGezag
		var url = _webApiOptions.Value.Url + "/opvragenBevoegdheidTotGezag";
		return GetResultFromHttpRequest<GezagResponse>(url, null, HttpMethod.Post, requestHeaders, requestBody);
    }
}