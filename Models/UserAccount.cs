namespace TAS.Models
{
	public class UserAccount
	{
		public Guid UserId { get; set; }
		public string Email { get; set; } = default!;
		public byte[] PasswordHash { get; set; } = default!;
		public byte[] PasswordSalt { get; set; } = default!;
		public string HashAlgo { get; set; } = "PBKDF2";
		public int HashIter { get; set; } = 150_000;
		public int FailedAccessCount { get; set; }
		public DateTime? LockoutEnd { get; set; }
		public string? TwoFactorSecret { get; set; }
		public bool TwoFactorEnabled { get; set; }
		public bool IsActive { get; set; } = true;
	}

}
