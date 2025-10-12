using System.ComponentModel.DataAnnotations;

namespace TAS.Models
{
    public class User
    {
        public long UserId { get; set; }

        [MaxLength(100)] public string Username { get; set; } = "";
        [MaxLength(200)] public string Email { get; set; } = "";
        [MaxLength(20)] public string? Phone { get; set; }

        // PBKDF2/Argon2 hash dạng Base64
        [MaxLength(500)] public string PasswordHash { get; set; } = "";
        [MaxLength(200)] public string PasswordSalt { get; set; } = "";

        public int FailedAccessCount { get; set; }
        public DateTime? LockedUntil { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? LastLoginAt { get; set; }

        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
        public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    }
    public sealed class Role
    {
        public long Id { get; set; }
        [MaxLength(100)] public string Name { get; set; } = ""; // Admin, Agent, User
        [MaxLength(200)] public string? Description { get; set; }

        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
    public sealed class UserRole
    {
        public long UserId { get; set; }
        public User User { get; set; } = null!;
        public long RoleId { get; set; }
        public Role Role { get; set; } = null!;
    }
    public sealed class RefreshToken
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public User User { get; set; } = null!;
        [MaxLength(500)] public string Token { get; set; } = "";
        public DateTime ExpiresAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? RevokedAt { get; set; }
        public bool IsActive => RevokedAt == null && DateTime.UtcNow < ExpiresAt;
    }
    public sealed class LoginRequest
    {
        [Required, MaxLength(100)] public string Username { get; set; } = "";
        [Required, MaxLength(200)] public string Password { get; set; } = "";
        public bool RememberMe { get; set; }
    }

    public sealed class RegisterRequest
    {
        [Required, MaxLength(100)] public string Username { get; set; } = "";
        [Required, EmailAddress, MaxLength(200)] public string Email { get; set; } = "";
        [Required, MaxLength(200)] public string Password { get; set; } = "";
        [Required, Compare(nameof(Password))] public string ConfirmPassword { get; set; } = "";
    }

    public sealed class ChangePasswordRequest
    {
        [Required] public string CurrentPassword { get; set; } = "";
        [Required] public string NewPassword { get; set; } = "";
        [Required, Compare(nameof(NewPassword))] public string ConfirmNewPassword { get; set; } = "";
    }
}
