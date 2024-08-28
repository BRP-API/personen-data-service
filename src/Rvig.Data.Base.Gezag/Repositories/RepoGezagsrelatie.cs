using Rvig.Api.Gezag.ResponseModels;
using Rvig.Api.Gezag.RequestModels;
using Microsoft.Extensions.Options;
using Rvig.Data.Base.WebApi.Options;
using Rvig.HaalCentraalApi.Shared.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace Rvig.Data.Base.Gezag.Repositories;

public interface IRepoGezagsrelatie
{
	Task<IEnumerable<AbstractGezagsrelatie>?> GetGezag(IEnumerable<string> burgerservicenummer);
}
public class RepoGezagsrelatie : RepoWebApiBase, IRepoGezagsrelatie
{
	public RepoGezagsrelatie(IHttpContextAccessor httpContextAccessor, IOptions<WebApiOptions> webApiOptions, ILoggingHelper loggingHelper) : base(httpContextAccessor, webApiOptions, loggingHelper)
	{
	}

	public Task<IEnumerable<AbstractGezagsrelatie>?> GetGezag(IEnumerable<string> burgerservicenummer)
	{
		if (burgerservicenummer.IsNullOrEmpty())
		{
			return Task.FromResult(Enumerable.Empty<AbstractGezagsrelatie>());
		}
		var requestBody = new GezagRequest
		{
			Bsns = burgerservicenummer
		};

		var url = _webApiOptions.Value.Url + "/opvragenBevoegdheidTotGezag";
		return GetResultFromHttpRequest<IEnumerable<AbstractGezagsrelatie>>(url, null, HttpMethod.Post, null, requestBody);
	}
}