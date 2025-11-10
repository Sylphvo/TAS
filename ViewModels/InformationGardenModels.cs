using TAS.Helpers;
using TAS.Models;
using TAS.TagHelpers;
using static Raven.Database.Indexing.IndexingWorkStats;

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
		public async Task<List<RubberFarmRequest>> GetRubberFarmAsync()
		{
			var sql = @"
				SELECT 
					rowNo = ROW_NUMBER() OVER(ORDER BY A.FarmId ASC),
					A.FarmId,
					A.FarmCode,
					A.AgentCode,
					A.FarmerName,
					B.AgentName,
					A.FarmAddress,
					B.AgentAddress,
					A.Polygon,
					A.Certificates,
					A.TotalAreaHa,
					A.RubberAreaHa,
					A.TotalExploit,
					A.IsActive,
					UpdateBy = ISNULL(A.UpdatePerson, A.RegisterPerson),
					UpdateTime = CONVERT(VARCHAR,ISNULL(A.UpdateDate, A.RegisterDate),111) + ' ' + CONVERT(VARCHAR(5),ISNULL(A.UpdateDate, A.RegisterDate), 108)
				FROM RubberFarm A
				LEFT JOIN RubberAgent B ON A.AgentCode = B.AgentCode


			";
			return await dbHelper.QueryAsync<RubberFarmRequest>(sql);
		}

		public int ImportPolygon(RubberFarmRequest rubberFarmRequest)
		{
			try
			{
				string sql = @"
				UPDATE RubberFarm SET Polygon = N'" + rubberFarmRequest.Polygon + @"' WHERE FarmId = '" + rubberFarmRequest.FarmId + @"'";
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
