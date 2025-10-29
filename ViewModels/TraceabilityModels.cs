using TAS.Helpers;
using TAS.Models;
using TAS.TagHelpers;

namespace TAS.ViewModels
{
	public class TraceabilityModels
	{
		ConnectDbHelper dbHelper = new ConnectDbHelper();
		public TraceabilityModels()
		{

		}
		// Model
		public async Task<List<RubberOrderSummaryReuqest>> GetTraceabilityAsync(CancellationToken ct = default)
		{
			var sql = @"
				-- Tạo bảng tạm với cấu trúc rõ ràng
				CREATE TABLE #TempOrder (
					Id INT,
					ParentId INT NULL,
					SortOrder INT,
					OrderCode NVARCHAR(50) NULL,
					OrderName NVARCHAR(200) NULL,
					AgentCode NVARCHAR(200) NULL,
					AgentName NVARCHAR(200) NULL,
					FarmCode NVARCHAR(200) NULL,
					FarmerName NVARCHAR(200) NULL,
					WeightKg DECIMAL(18,2) NULL,
					TotalAmount DECIMAL(18,2) NULL,
					SortIdList NVARCHAR(200) NULL,
					IsOpenChild bit NULL
				);

				-- Level 1: Đơn hàng
				INSERT INTO #TempOrder (Id, ParentId, SortOrder, OrderCode, OrderName, AgentCode, AgentName, FarmCode, FarmerName, WeightKg, TotalAmount, SortIdList, IsOpenChild)
				VALUES (1, NULL, 1, 'ORD20251029', N'đơn hàng 1', NULL, NULL, NULL, NULL, NULL, NULL, 'ORD20251029', 1);

				-- Level 2: Đại lý
				INSERT INTO #TempOrder (Id, ParentId, SortOrder, OrderCode, OrderName, AgentCode, AgentName, FarmCode, FarmerName, WeightKg, TotalAmount, SortIdList, IsOpenChild)
				SELECT 
					ROW_NUMBER() OVER (ORDER BY A.AgentId) + 1 AS Id,
					1 AS ParentId,
					2 AS SortOrder,
					NULL AS OrderCode,
					NULL AS OrderName,
					A.AgentCode,
					A.OwnerName AS AgentName,
					NULL AS FarmCode,
					NULL AS FarmerName,
					NULL AS WeightKg,
					NULL AS TotalAmount,
					'ORD20251029' + '__' + A.AgentCode AS SortIdList,
					IsOpenChild = 1
				FROM RubberAgent A;

				-- Level 3: Nhà vườn
				INSERT INTO #TempOrder (Id, ParentId, SortOrder, OrderCode, OrderName, AgentCode, AgentName, FarmCode, FarmerName, WeightKg, TotalAmount, SortIdList, IsOpenChild)
				SELECT 
					ROW_NUMBER() OVER (ORDER BY F.FarmId) + 100 AS Id,
					2 AS ParentId,
					3 AS SortOrder,
					NULL AS OrderCode,
					NULL AS OrderName,
					Agent.AgentCode,
					NULL AS AgentName,
					NULL AS FarmCode,
					F.FarmerName,
					I.TSCPercent AS WeightKg,
					I.TSCPercent * 10 AS TotalAmount,
					'ORD20251029' + '__' + Agent.AgentCode + '__' + F.FarmCode AS SortIdList,
					IsOpenChild = 0
				FROM RubberFarm F
				LEFT JOIN RubberIntake I ON F.FarmCode = I.FarmCode
				LEFT JOIN RubberAgent Agent ON Agent.AgentCode = F.AgentCode

				-- Kết quả
				SELECT Id, ParentId, SortOrder, OrderCode, OrderName, AgentCode,  AgentName, FarmCode, FarmerName, WeightKg, TotalAmount, SortIdList, IsOpenChild
				FROM #TempOrder
				ORDER BY OrderCode DESC, AgentCode;
				DROP TABLE #TempOrder;
			";
			return await dbHelper.QueryAsync<RubberOrderSummaryReuqest>(sql);
		}
	}
}
