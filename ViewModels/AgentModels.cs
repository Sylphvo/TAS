using TAS.Helpers;
using TAS.TagHelpers;

namespace TAS.ViewModels
{
	public class AgentModels
	{
		ConnectDbHelper dbHelper;
		public AgentModels()
		{
			dbHelper = new ConnectDbHelper();
		}
		// Model
		public async Task<List<RubberAgent>> GetRubberAgentAsync()
		{
			var sql = "SELECT * FROM RubberAgent";
			return await dbHelper.QueryAsync<RubberAgent>(sql);
		}
	}
}
