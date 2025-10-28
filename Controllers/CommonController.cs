//using Microsoft.AspNetCore.Mvc;
//using TAS.Helpers;
//using TAS.TagHelpers;

//namespace TAS.Controllers
//{
//	public class CommonController : Controller
//	{
//		ConnectDbHelper dbHelper = new ConnectDbHelper();
//		public async Task<List<RubberAgent>> ComboAgent()
//		{
//			var sql = "SELECT * FROM RubberAgent";
//			return await dbHelper.QueryAsync<RubberAgent>(sql);
//		}
//	}
//}
