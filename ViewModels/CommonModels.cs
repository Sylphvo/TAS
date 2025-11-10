using Microsoft.AspNetCore.Localization;
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
			try
			{
				var listData = new List<RubberAgent>();
				var sql = @"
					SELECT * FROM RubberAgent
				";
				listData = await dbHelper.QueryAsync<RubberAgent>(sql);
				return listData;
			}
			catch (Exception ex)
			{
				return null;
			}
		}
		public async Task<List<RubberFarmRequest>> ComboFarmCode()
		{
			try
			{
				string sql = @"
					SELECT DISTINCT FarmCode, FarmerName FROM RubberFarm WHERE FarmCode IS NOT NULL
				";
				return await dbHelper.QueryAsync<RubberFarmRequest>(sql);
			}
			catch (Exception ex)
			{
				return null;
			}
		}
	}
}
