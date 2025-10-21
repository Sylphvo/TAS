using System.Security.Cryptography;

namespace TAS.TagHelpers
{
	public static class PasswordHelper
	{
		public static bool Verify(string password, byte[] salt, int iter, byte[] expectedHash)
		{
			using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iter, HashAlgorithmName.SHA256);
			var hash = pbkdf2.GetBytes(32);
			return CryptographicOperations.FixedTimeEquals(hash, expectedHash);
		}
		public static (byte[] hash, byte[] salt, int iter) Hash(string password, int iter = 150_000)
		{
			using var rng = RandomNumberGenerator.Create();
			var salt = new byte[16];
			rng.GetBytes(salt);
			using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iter, HashAlgorithmName.SHA256);
			return (pbkdf2.GetBytes(32), salt, iter);
		}
	}
}
