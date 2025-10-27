using TAS.Helpers;
using TAS.TagHelpers;

namespace TAS.ViewModels
{
	public class RubberGardenModels
	{
		ConnectDbHelper dbHelper = new ConnectDbHelper();
		public RubberGardenModels()
		{

		}
		// Model
		public async Task<List<RubberFarm>> GetRubberFarmAsync()
		{
			var sql = "SELECT * FROM RubberFarm";
			return await dbHelper.QueryAsync<RubberFarm>(sql);
		}
	}
}
