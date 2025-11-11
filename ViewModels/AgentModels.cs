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
			var sql = @"SELECT * FROM RubberAgent";
			return await dbHelper.QueryAsync<RubberAgent>(sql);
		}
		public int AddOrUpdateRubberAgent(RubberAgent rubberAgent)
		{
			try
			{
				if (rubberAgent == null)
				{
					throw new ArgumentNullException(nameof(rubberAgent), "Input data cannot be null.");
				}
				var sql = @"
				IF EXISTS (SELECT 1 FROM RubberAgent WHERE AgentId = @AgentId)
				BEGIN
					UPDATE RubberAgent SET
					AgentCode        = @AgentCode,
					AgentName		= @AgentName,
					OwnerName      = @OwnerName,
					TaxCode		= @TaxCode,

					IsActive        = @IsActive,
					UpdateDate      = GETDATE(),
					UpdatePerson    = @UpdatePerson
					WHERE AgentId = @AgentId
					SELECT 0;
				END
				ELSE
				BEGIN
					INSERT INTO RubberAgent
					(AgentCode, AgentName, OwnerName, TaxCode,
						IsActive, RegisterDate, RegisterPerson)
					VALUES
					(@AgentCode, @AgentName, @OwnerName, @TaxCode,
						@Certificates, @TotalAreaHa, @RubberAreaHa,
						@IsActive, GETDATE(), @RegisterPerson)
					SELECT SCOPE_IDENTITY() AS NewAgentId;
				END";
				// With this line:
				var lstResult = dbHelper.Execute(sql, new
				{
					AgentCode		= rubberAgent.AgentCode,
					AgentName		= rubberAgent.AgentName,
					OwnerName		= rubberAgent.OwnerName,
					AgentAddress	= rubberAgent.AgentAddress,
					TaxCode			= rubberAgent.TaxCode,
					IsActive		= rubberAgent.IsActive,
					UpdatePerson	= _userManage.Name,
					RegisterPerson	= _userManage.Name,
					AgentId = rubberAgent.AgentId
				});
				return lstResult;
			}
			catch (Exception ex)
			{
				return 0;
			}
		}
	}
}
