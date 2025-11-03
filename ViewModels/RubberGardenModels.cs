using Raven.Imports.Newtonsoft.Json.Linq;
using System.Globalization;
using TAS.Helpers;
using TAS.Models;
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
		public async Task<List<RubberIntakeRequest>> GetRubberFarmAsync()
		{
			var sql = @"
				SELECT 
					rowNo = ROW_NUMBER() OVER(ORDER BY IntakeId ASC),
					FarmCode,
					FarmerName,
					RubberKg,
					TSCPercent,
					DRCPercent,
					FinishedProductKg,
					CentrifugeProductKg,
					Status,
					RegisterDate,
					RegisterPerson,
					UpdateDate,
					UpdatePerson
				FROM RubberIntake
			";
			return await dbHelper.QueryAsync<RubberIntakeRequest>(sql);
		}


		public int AddOrUpdateRubber(RubberIntakeRequest rubberIntakeRequest)
		{
			try { 
				if (rubberIntakeRequest == null)
				{
					throw new ArgumentNullException(nameof(rubberIntakeRequest), "Input data cannot be null.");
				}
				var sql = @"
				DECLARE @FarmCode nvarchar(200) = " + rubberIntakeRequest.FarmCode + @",
						@FarmerName nvarchar(200) = " + rubberIntakeRequest.FarmerName + @",
						@RubberKg decimal(12,3) = " + rubberIntakeRequest.RubberKg + @",
						@TSCPercent decimal(5,2) = " + rubberIntakeRequest.TSCPercent + @",
						@DRCPercent decimal(5,2) = " + rubberIntakeRequest.DRCPercent + @",
						@FinishedProductKg decimal(12,3) = " + rubberIntakeRequest.FinishedProductKg + @",
						@CentrifugeProductKg decimal(12,3) = " + rubberIntakeRequest.CentrifugeProductKg + @",
						@Status bit = " + rubberIntakeRequest.Status + @",
						@RegisterPerson nvarchar(50) = " + rubberIntakeRequest.FarmCode + @";
				IF NOT EXISTS ()
				BEGIN
					INSERT INTO RubberIntake
					(
						FarmCode, FarmerName,
						RubberKg, TSCPercent, DRCPercent,
						FinishedProductKg, CentrifugeProductKg,
						Status,
						RegisterDate, RegisterPerson
					)
					VALUES
					(
						@FarmCode, @FarmerName,
						@RubberKg, @TSCPercent, @DRCPercent,
						@FinishedProductKg, @CentrifugeProductKg,
						@Status,
						GETDATE(), @RegisterPerson
					);
				END
				ELSE 
				BEGIN 
					UPDATE RubberIntake
					SET
						FarmCode            = @FarmCode,
						FarmerName          = @FarmerName,
						RubberKg                  = @RubberKg,
						TSCPercent          = @TSCPercent,
						DRCPercent          = @DRCPercent,
						FinishedProductKg   = @FinishedProductKg,
						CentrifugeProductKg = @CentrifugeProductKg,
						Status              = @Status,
						UpdateDate          = SYSUTCDATETIME(),
						UpdatePerson        = @UpdatePerson,
						IntakeDate          = @IntakeDate
					WHERE Id = @Id;
				END
				SELECT 1;		
				";
				// With this line:
				int resultData = dbHelper.Execute(sql);
				return resultData;
			}
			catch(Exception ex)
			{
				return 0;
			}
			
		}
		public int ImportListData(List<RubberIntakeRequest> lstRubberIntakeRequest)
		{
			try
			{
				const string sql = @"
				INSERT INTO RubberIntake
				(FarmCode, FarmerName, RubberKg, TSCPercent, DRCPercent,
					FinishedProductKg, CentrifugeProductKg, Status, RegisterDate, RegisterPerson)
				VALUES
				(@FarmCode, @FarmerName, @RubberKg, @TSCPercent, @DRCPercent,
					@FinishedProductKg, @CentrifugeProductKg, @Status, GETDATE(), @RegisterPerson);";

				dbHelper.Execute(sql, lstRubberIntakeRequest.Select(x => new {
					FarmCode = x.FarmCode,
					FarmerName = x.FarmerName,
					RubberKg = x.RubberKg ?? 0m,
					TSCPercent = x.TSCPercent ?? 0m,
					DRCPercent = x.DRCPercent ?? 0m,
					FinishedProductKg = x.FinishedProductKg ?? 0m,
					CentrifugeProductKg = x.CentrifugeProductKg ?? 0m,
					Status = 0,
					RegisterPerson = "admin"
				}));
				return 1;
			}
			catch(Exception ex)
			{
				return 0;
			}
		}
	}
}
