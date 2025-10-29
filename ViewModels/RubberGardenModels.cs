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
		public async Task<List<RubberFarmRequest>> GetRubberFarmAsync()
		{
			var sql = "SELECT * FROM RubberFarm";
			return await dbHelper.QueryAsync<RubberFarmRequest>(sql);
		}


        //public int AddOrUpdate()
        //{
        //    var sql = "SELECT * FROM RubberFarm";
        //    return await dbHelper.QueryAsync<RubberFarmRequest>(sql);
        //}
    }
}
