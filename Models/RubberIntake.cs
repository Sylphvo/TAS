using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TAS.Models
{
	public class RubberIntakeDb
	{
		[Key]
		public Guid Id { get; set; } = Guid.NewGuid();

		// Mã nhà vườn
		[Required, StringLength(200)]
		public string FarmCode { get; set; } = string.Empty;

		// Tên nhà vườn
		[Required, StringLength(200)]
		public string FarmerName { get; set; } = string.Empty;

		// KG
		[Column(TypeName = "decimal(12,3)")]
		public decimal? Kg { get; set; }

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

		// meta
		public DateTime? IntakeDate { get; set; }
		[StringLength(50)] public string? BatchCode { get; set; }
	}
	public class RubberIntakeRequest
	{
	
		public int? RowNo { get; set; } // STT
		public string FarmCode { get; set; } = string.Empty;  // Mã nhà vườn
		public string FarmerName { get; set; } = string.Empty;  // Tên nhà vườn
		public decimal? Kg { get; set; }    // KG
		public decimal? TSCPercent { get; set; }// TSC (%)
		public decimal? DRCPercent { get; set; }// DRC (%)
		public decimal? FinishedProductKg { get; set; }// Thành Phẩm (kg)
		public decimal? CentrifugeProductKg { get; set; }// Thành Phẩm Ly Tâm (kg)
		public DateTime? IntakeDate { get; set; }// meta
	}
}
