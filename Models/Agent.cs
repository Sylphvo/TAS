namespace TAS.Models
{
    public class Agent
    {
        public long AgentId { get; set; }
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? ShortName { get; set; }
        public string? TaxCode { get; set; }
        public string? LicenseNo { get; set; }
        public string? ContactName { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? AddressLine { get; set; }
        public string? Ward { get; set; }
        public string? District { get; set; }
        public string? Province { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public string? BankName { get; set; }
        public string? BankAccountNo { get; set; }
        public string? BankAccountName { get; set; }
        public bool IsActive { get; set; } = true;
        public string? Note { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }

        public ICollection<Garden> Gardens { get; set; } = new List<Garden>();
    }
}
