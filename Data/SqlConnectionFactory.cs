using Microsoft.Data.SqlClient;
using System.Data;

namespace TAS.Data
{
	public class SqlConnectionFactory : IDbConnectionFactory
	{
		private readonly string _cs;
		public SqlConnectionFactory(IConfiguration cfg) =>
			_cs = cfg.GetConnectionString("DefaultConnection")!;

		public async Task<IDbConnection> OpenAsync(CancellationToken ct = default)
		{
			var con = new SqlConnection(_cs);
			await con.OpenAsync(ct);
			return con;
		}
	}
}
