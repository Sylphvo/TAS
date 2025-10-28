using TAS.Helpers;
using TAS.TagHelpers;

namespace TAS.ViewModels
{
	public class CommonModels
	{
		ConnectDbHelper dbHelper;
		public CommonModels()
		{
			dbHelper = new ConnectDbHelper();
		}
		// Model
		public async Task<List<RubberAgent>> ComboAgent()
		{
			var listData = new List<RubberAgent>();
			var sql = "SELECT * FROM RubberAgent";
			listData = await dbHelper.QueryAsync<RubberAgent>(sql);

			return listData;
		}
	}
}
