using System.ComponentModel.DataAnnotations;

namespace TAS.Models
{
    public class RubberFarm
	{
		[Key]
		public long FarmId { get; set; }
		public long FarmCode { get; set; }//mã nhà vườn 
		public long AgentCode { get; set; }// mã đại lý liên kết

        // Chủ hộ
		public string? FarmerName { get; set; }
        public string? FarmerPhone { get; set; }
        public string? FarmerAddress { get; set; }
        public string? FarmerMap { get; set; }
		public string? Certificates { get; set; }
		
		public decimal? TotalAreaHa { get; set; }//Tổng diện tích (ha)
		public decimal? RubberAreaHa { get; set; }//Tổng diện tích (ha)
		public decimal? TotalExploit { get; set; }//Tổng Khai thác (kg)

		public bool IsActive { get; set; } = true;// trạng thái đại lý 
		public DateTime CreatedAt { get; set; }//thời gian tạo
		public string? CreatedBy { get; set; }//người tạo 
		public DateTime? UpdatedAt { get; set; }//thời gian cập nhật
		public string? UpdatedBy { get; set; }//người cập nhật
		public RubberAgent? Rubber_Agent { get; set; }            // navigation
	}
}
