﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TAS.Models
{
	[Table("RubberOrderSummary")]
	public class RubberOrderSummary
	{
		[Key]
		public long OrderId { get; set; } // Mã tự tăng

		[Required]
		public string OrderCode { get; set; } = default!; // Mã đơn hàng

		[Required]
		public string OrderName { get; set; } = default!; // Tên đơn hàng

		// Level 1 - Đại lý
		public long AgentId { get; set; }
		public string? AgentCode { get; set; }
		public string? AgentName { get; set; }

		// Level 2 - Nhà vườn
		public long FarmId { get; set; }
		public string? FarmCode { get; set; }
		public string? FarmerName { get; set; }

		// Level 3 - Thông tin nhập cao su (RubberIntake)
		public long IntakeId { get; set; }
		public decimal TotalWeightKg { get; set; } // Tổng số kg thu mua
		public decimal? PricePerKg { get; set; } // Giá theo kg
		public decimal? TotalAmount { get; set; } // Thành tiền = kg * giá

		// Level xác định thứ bậc (1 = đơn hàng, 2 = đại lý, 3 = nhà vườn)
		public int SortOrder { get; set; }
		public int Level { get; set; }

		// Trạng thái và thời gian
		public bool IsActive { get; set; } = true;
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
		public string? CreatedBy { get; set; }
	}
	public class RubberOrderSummaryReuqest
	{
		public long OrderId { get; set; } // Mã tự tăng
		public string OrderCode { get; set; } = default!; // Mã đơn hàng
		public string OrderName { get; set; } = default!; // Tên đơn hàng

		// Level 1 - Đại lý
		public long AgentId { get; set; }
		public string? AgentCode { get; set; }
		public string? AgentName { get; set; }

		// Level 2 - Nhà vườn
		public long FarmId { get; set; }
		public string? FarmCode { get; set; }
		public string? FarmerName { get; set; }

		// Level 3 - Thông tin nhập cao su (RubberIntake)
		public long IntakeId { get; set; }
		public decimal TotalWeightKg { get; set; } // Tổng số kg thu mua
		public decimal? PricePerKg { get; set; } // Giá theo kg
		public decimal? TotalAmount { get; set; } // Thành tiền = kg * giá

		// Level xác định thứ bậc (1 = đơn hàng, 2 = đại lý, 3 = nhà vườn)
		public int SortOrder { get; set; }

		// Trạng thái và thời gian
		public bool IsActive { get; set; } = true;
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
		public string? CreatedBy { get; set; }
		public string? SortIdList { get; set; }
		public bool? IsOpenChild { get; set; }

	}
}
