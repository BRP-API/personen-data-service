using Dapper;
using Microsoft.Extensions.Options;
using Npgsql;
using Rvig.HaalCentraalApi.Shared.Exceptions;
using Rvig.HaalCentraalApi.Shared.Options;
using static Dapper.SqlMapper;

namespace Rvig.Data.Base.Postgres.Repositories;
public abstract class PostgresRepoBase(IOptions<DatabaseOptions> databaseOptions)
{
	protected readonly IOptions<DatabaseOptions> _databaseOptions = databaseOptions;

    public NpgsqlConnection GetConnection()
	{
		return new NpgsqlConnection(_databaseOptions.Value.ConnectionString);
	}

	protected async Task<List<TDataObject>> GetDataViaDapper<TDataObject>(string queryBase, DynamicParameters dynamicParameters, string whereCondition)
	{
		var query = string.Format(queryBase, whereCondition);

		return (await DapperQueryAsync<TDataObject>(query, dynamicParameters)).ToList();
	}

	protected Task<IEnumerable<TDataObject>> DapperQueryAsync<TDataObject>(string? query, DynamicParameters? dynamicParameters = null)
	{
		return DapperQueryAsync<TDataObject>(GetConnection(), query, dynamicParameters);
	}

	private static Task<IEnumerable<TDataObject>> DapperQueryAsync<TDataObject>(NpgsqlConnection connection, string? query, DynamicParameters? dynamicParameters = null)
	{
		try
		{
			if (dynamicParameters != null)
			{
				return connection.QueryAsync<TDataObject>(query, dynamicParameters);
			}

			return connection.QueryAsync<TDataObject>(query);
		}
		catch (NpgsqlException npgEx)
		{
			throw new ServiceUnavailableException(npgEx.Message, npgEx);
		}
	}

	protected static async Task OpenConnectionAndLog(NpgsqlConnection connection)
	{
		try
		{
			await connection.OpenAsync();
		}
		catch(NpgsqlException npgEx)
		{
			throw new ServiceUnavailableException(npgEx.Message, npgEx);
		}
	}
}