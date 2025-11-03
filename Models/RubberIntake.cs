using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TAS.Models
{
	[Table("RubberIntake")]
	public class RubberIntakeDb
	{
		[Key]
		public long IntakeId { get; set; }

		// Mã nhà vườn
		[Required, StringLength(200)]
		public string FarmCode { get; set; } = string.Empty;

		// Tên nhà vườn
		[Required, StringLength(200)]
		public string FarmerName { get; set; } = string.Empty;

		// KG
		[Column(TypeName = "decimal(12,3)")]
		public decimal? RubberKg { get; set; }

		// TSC (%)
		[Column(TypeName = "decimal(5,2)")]
		public decimal? TSCPercent { get; set; }

		// DRC (%)
		[Column(TypeName = "decimal(5,2)")]
		public decimal? DRCPercent { get; set; }

		// Thành Phẩm (kg)
		[Column(TypeName = "decimal(12,3)")]
		public decimal? FinishedProductKg { get; set; }

		// Thành Phẩm Ly Tâm (kg)
		[Column(TypeName = "decimal(12,3)")]
		public decimal? CentrifugeProductKg { get; set; }

        // Trạng thái  nhập
        [Column(TypeName = "bit")]
		public int? Status { get; set; }

        public DateTime? RegisterDate { get; set; }// thời gian tạo

        [StringLength(50)]
        public string? RegisterPerson { get; set; } // Người tạo

        public DateTime? UpdateDate { get; set; }// thời gian cập nhật

        [StringLength(50)]
        public string? UpdatePerson { get; set; } // Người cập nhật

    }
	public class RubberIntakeRequest
	{
	
		public int? RowNo { get; set; } // STT
		public string FarmCode { get; set; } = string.Empty;  // Mã nhà vườn
		public string FarmerName { get; set; } = string.Empty;  // Tên nhà vườn
		public decimal? RubberKg { get; set; }    // KG
		public decimal? TSCPercent { get; set; }// TSC (%)
		public decimal? DRCPercent { get; set; }// DRC (%)
		public decimal? FinishedProductKg { get; set; }// Thành Phẩm (kg)
		public decimal? CentrifugeProductKg { get; set; }// Thành Phẩm Ly Tâm (kg)
		public int? Status { get; set; }
		public DateTime? RegisterDate { get; set; }
		public string? RegisterPerson { get; set; }
		public DateTime? UpdateDate { get; set; }
		public string? UpdatePerson { get; set; }

	}
}
