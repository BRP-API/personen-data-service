using Rvig.Api.Gezag.ResponseModels;
using Rvig.Api.Gezag.RequestModels;
using Microsoft.Extensions.Options;
using Rvig.Data.Base.WebApi.Options;
using Rvig.HaalCentraalApi.Shared.Helpers;
using Microsoft.AspNetCore.Http;

namespace Rvig.Data.Base.Gezag.Repositories;

public interface IRepoGezagsrelatie
{
	Task<IEnumerable<Gezagsrelatie>?> GetGezagsrelaties(string? burgerservicenummer);
}
public class RepoGezagsrelatie : RepoWebApiBase, IRepoGezagsrelatie
{
	public RepoGezagsrelatie(IHttpContextAccessor httpContextAccessor, IOptions<WebApiOptions> webApiOptions, ILoggingHelper loggingHelper) : base(httpContextAccessor, webApiOptions, loggingHelper)
	{
	}

	public Task<IEnumerable<Gezagsrelatie>?> GetGezagsrelaties(string? burgerservicenummer)
	{
		if (string.IsNullOrWhiteSpace(burgerservicenummer))
		{
			return Task.FromResult(Enumerable.Empty<Gezagsrelatie>());
		}
		var requestBody = new GezagRequest
		{
			Bsn = burgerservicenummer
		};

		var url = _webApiOptions.Value.Url + "/opvragenBevoegdheidTotGezag";
		return GetResultFromHttpRequest<IEnumerable<Gezagsrelatie>>(url, null, HttpMethod.Post, null, requestBody);
	}
}