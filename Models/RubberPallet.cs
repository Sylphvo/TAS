using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TAS.Models
{
	[Table("RubberPallets")]
	public class RubberPalletDb
	{
		[Key]
		public long PalletId { get; set; } // identity

		[Required]
		public long OrderId { get; set; }   // FK -> RubberOrderSummary.OrderId

		[Required, StringLength(50)]
		public string PalletCode { get; set; } = default!; // VD: ORD001-P001

		public int PalletNo { get; set; } // 1..n

		[Column(TypeName = "decimal(12,3)")]
		public decimal WeightKg { get; set; }// Trọng lượng kg

		public bool IsActive { get; set; } = true;// trạng thái pallet
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;//thời gian tạo
		[StringLength(50)]
		public string? CreatedBy { get; set; }//người tạo
	}
	public class RubberPalletRequest
	{
		public int? RowNo { get; set; } // identity
		public long PalletId { get; set; } // identity
		public long OrderId { get; set; }   // FK -> RubberOrderSummary.OrderId
		public string PalletCode { get; set; } = default!; // VD: ORD001-P001
		public int PalletNo { get; set; } // 1..n
		public decimal WeightKg { get; set; }// Trọng lượng kg
		public bool IsActive { get; set; } = true;// trạng thái pallet
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;//thời gian tạo
		public string? CreatedBy { get; set; }//người tạo
	}
}
