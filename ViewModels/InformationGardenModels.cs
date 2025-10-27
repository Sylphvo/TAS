using TAS.Helpers;
using TAS.TagHelpers;

namespace TAS.ViewModels
{
	public class InformationGardenModels
	{
		ConnectDbHelper dbHelper;
		public InformationGardenModels()
		{
			dbHelper = new ConnectDbHelper();
		}
		// Model
		public async Task<List<RubberFarm>> GetRubberFarmAsync()
		{
			var sql = "SELECT * FROM RubberFarm";
			return await dbHelper.QueryAsync<RubberFarm>(sql);
		}
	}
}
