using Dapper;
using System.Data;

namespace TAS.Data
{
	public class DbExecutor : IDbExecutor
	{
		private readonly IDbConnectionFactory _factory;
		public DbExecutor(IDbConnectionFactory factory) => _factory = factory;

		public async Task<List<T>> QueryProcAsync<T>(string proc, object? args = null, CancellationToken ct = default)
		{
			using var conn = await _factory.OpenAsync(ct);
			var cmd = new CommandDefinition(proc, args, commandType: CommandType.StoredProcedure, cancellationToken: ct);
			var rows = await conn.QueryAsync<T>(cmd);
			return rows.AsList();
		}

		public async Task<int> ExecProcAsync(string proc, object? args = null, CancellationToken ct = default)
		{
			using var conn = await _factory.OpenAsync(ct);
			var cmd = new CommandDefinition(proc, args, commandType: CommandType.StoredProcedure, cancellationToken: ct);
			return await conn.ExecuteAsync(cmd);
		}
	}
}
