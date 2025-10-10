namespace TAS.Data
{
	public interface IDbExecutor
	{
		Task<List<T>> QueryProcAsync<T>(string proc, object? args = null, CancellationToken ct = default);
		Task<int> ExecProcAsync(string proc, object? args = null, CancellationToken ct = default);
	}
}
