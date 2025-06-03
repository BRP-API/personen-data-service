using Microsoft.Extensions.Options;
using Rvig.Data.Base.WebApi.Options;
using Rvig.HaalCentraalApi.Shared.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.Net.Http;
using Rvig.Data.Base.Gezag.Repositories;
using Rvig.HaalCentraalApi.Personen.ApiModels.Gezag;

namespace Rvig.HaalCentraalApi.Personen.Repositories;

public interface IRepoGezagsrelatieDeprecated 
{
	Task<GezagResponse?> GetGezag(IEnumerable<string> burgerservicenummer);
}
public class RepoGezagsrelatieDeprecated : RepoWebApiBase, IRepoGezagsrelatieDeprecated
{
    public RepoGezagsrelatieDeprecated(
		IHttpContextAccessor httpContextAccessor, 
		IOptions<WebApiOptions> webApiOptions, 
		ILoggingHelper loggingHelper) 
	: base(httpContextAccessor, webApiOptions, loggingHelper) 
	{ 
	
	}

	public async Task<GezagResponse?> GetGezag(IEnumerable<string> burgerservicenummer)
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

		return await GetResultFromHttpRequest<GezagResponse>(url, null, HttpMethod.Post, null, requestBody);
	}
}