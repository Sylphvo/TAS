namespace TAS.Models
{
    public class Garden
    {
        public long GardenId { get; set; }
        public long DealerId { get; set; }
        public string Code { get; set; } = null!;
        public string OwnerName { get; set; } = null!;
        public string? OwnerPhone { get; set; }
        public string? OwnerIdNo { get; set; }
        public decimal? AreaHa { get; set; }
        public int? TreesCount { get; set; }
        public short? PlantedYear { get; set; }
        public TapStatus? TapStatus { get; set; }
        public decimal? AvgLatexKgDay { get; set; }
        public string? AddressLine { get; set; }
        public string? Ward { get; set; }
        public string? District { get; set; }
        public string? Province { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public string? IrrigationType { get; set; }
        public string? Certification { get; set; }
        public bool IsActive { get; set; } = true;
        public string? Note { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }

        public Dealer Dealer { get; set; } = null!;
    }
}
