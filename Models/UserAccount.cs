﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TAS.Helpers
{
	public class UserAccount
	{
        [Key]
        public Guid UserId { get; set; }

        [MaxLength(100)]
        public string FirstName { get; set; } = "";

        [MaxLength(100)]
        public string LastName { get; set; } = "";

        [Required, MaxLength(100)]
        public string UserName { get; set; } = "";

        [Required, MaxLength(256), EmailAddress]
        public string Email { get; set; } = default!;

        [NotMapped]                 // không lưu plaintext
        public string Password { get; set; } = default!;

        [Required]                  // lưu hash
        public byte[] PasswordHash { get; set; } = default!;

        [NotMapped]                 // không lưu confirm
        public string ConfirmPassword { get; set; } = "";

        public bool AcceptTerms { get; set; } = true;
        public bool IsActive { get; set; } = true;
        public bool TwoFactorEnabled { get; set; }
        public bool RememberMe { get; set; }

        public DateTime? LogIn { get; set; }
        public DateTime? LogOut { get; set; }
    }
}
