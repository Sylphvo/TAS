using System.ComponentModel.DataAnnotations;

namespace TAS.Helpers
{
    public class RubberAgent
	{
		[Key]
		public long AgentId { get; set; } //Khóa định danh đại lý
        public string? AgentCode { get; set; } //Khóa định danh đại lý
		public string? AgentName { get; set; } //Tên đại lý
		public string? OwnerName { get; set; } //Chủ sở hữu/Người đại diện
		public string? TaxCode { get; set; } // Mã số thuế
		public string? AgentAddress { get; set; } // Địa chỉ đại lý 

        public bool IsActive { get; set; } = true;// trạng thái đại lý 
        public DateTime CreatedAt { get; set; }//thời gian tạo
		public string? CreatedBy { get; set; }//người tạo 
		public DateTime? UpdatedAt { get; set; }//thời gian cập nhật
		public string? UpdatedBy { get; set; }//người cập nhật
    }
}
