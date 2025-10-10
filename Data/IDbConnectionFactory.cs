using System.Data;

namespace TAS.Data
{
	public interface IDbConnectionFactory
	{
		Task<IDbConnection> OpenAsync(CancellationToken ct = default);
	}
}
