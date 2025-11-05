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
					intakeId,
					farmCode,
					farmerName,
					rubberKg,
					tscPercent,
					drcPercent,
					finishedProductKg,
					centrifugeProductKg,
					status,
					timeDate_Person = ISNULL(UpdatePerson, RegisterPerson),
					timeDate = CONVERT(VARCHAR,ISNULL(UpdateDate, RegisterDate),111) + ' ' + CONVERT(VARCHAR(5),ISNULL(UpdateDate, RegisterDate), 108)
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
				DECLARE @FarmCode nvarchar(200) = " + rubberIntakeRequest.farmCode + @",
						@FarmerName nvarchar(200) = " + rubberIntakeRequest.farmerName + @",
						@RubberKg decimal(12,3) = " + rubberIntakeRequest.rubberKg + @",
						@TSCPercent decimal(5,2) = " + rubberIntakeRequest.tscPercent + @",
						@DRCPercent decimal(5,2) = " + rubberIntakeRequest.drcPercent + @",
						@FinishedProductKg decimal(12,3) = " + rubberIntakeRequest.finishedProductKg + @",
						@CentrifugeProductKg decimal(12,3) = " + rubberIntakeRequest.centrifugeProductKg + @",
						@Status bit = " + rubberIntakeRequest.status + @",
						@RegisterPerson nvarchar(50) = " + rubberIntakeRequest.farmCode + @";
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
					FarmCode = x.farmCode,
					FarmerName = x.farmerName,
					RubberKg = x.rubberKg ?? 0m,
					TSCPercent = x.tscPercent ?? 0m,
					DRCPercent = x.drcPercent ?? 0m,
					FinishedProductKg = x.finishedProductKg ?? 0m,
					CentrifugeProductKg = x.centrifugeProductKg ?? 0m,
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
		public int ApproveDataRubber(int intakeId, int status)
		{
			try
			{
				string sql = @"
				UPDATE RubberIntake SET Status = "+ status + @" WHERE IntakeId = "+ intakeId + @"" ;
				dbHelper.Execute(sql);
				return 1;
			}
			catch (Exception ex)
			{
				return 0;
			}
		}
	}
}
