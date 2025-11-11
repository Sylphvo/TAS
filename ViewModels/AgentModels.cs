using TAS.Helpers;
using TAS.Repository;
using TAS.TagHelpers;

namespace TAS.ViewModels
{
	public class AgentModels
	{
		private readonly ICurrentUser _userManage;
		ConnectDbHelper dbHelper = new ConnectDbHelper();
		public AgentModels(ICurrentUser userManage)
		{
			_userManage = userManage;
		}
		// Model
		public async Task<List<RubberAgent>> GetRubberAgentAsync()
		{
			var sql = "SELECT * FROM RubberAgent";
			return await dbHelper.QueryAsync<RubberAgent>(sql);
		}
	}
}
