using Rvig.Data.Base.Postgres.Repositories;
using Rvig.HaalCentraalApi.Shared.Interfaces;

namespace Rvig.Data.Base.Postgres.Services;

public class HealthCheckDatabaseConnectionService(IRvigDbHealthCheckRepo rvigDbHealthCheckRepo)
	: IHealthCheckDatabaseConnectionService
{
    public async Task<int> CheckDatabaseConnection()
	{
		var result = await rvigDbHealthCheckRepo.SendSimpleQuery();

		return !string.IsNullOrWhiteSpace(result) ? 0 : 1;
	}
}
