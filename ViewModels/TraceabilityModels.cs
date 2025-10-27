using TAS.Helpers;
using TAS.TagHelpers;

namespace TAS.ViewModels
{
	public class TraceabilityModels
	{
		ConnectDbHelper databaseHelper = new ConnectDbHelper();
		public TraceabilityModels(ConnectDbHelper _databaseHelper)
		{
			databaseHelper = _databaseHelper;
		}
		// Model
		public async Task<List<RubberFarm>> GetRubberFarmAsync(CancellationToken ct = default)
		{
			var sql = "SELECT * FROM RubberFarm";
			return await databaseHelper.QueryAsync<RubberFarm>(sql);
		}
	}
}
