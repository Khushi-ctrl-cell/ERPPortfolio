using System;
using System.Security.Cryptography;
using System.Text;

namespace ERPPortfolio.Helpers
{
    public static class PasswordHasher
    {
        public static string HashPassword(string password)
        {
            var saltBytes = new byte[16];
            using (var random = RandomNumberGenerator.Create())
            {
                random.GetBytes(saltBytes);
            }

            using (var sha256 = SHA256.Create())
            {
                var passwordBytes = Encoding.UTF8.GetBytes(password);
                var combinedBytes = new byte[saltBytes.Length + passwordBytes.Length];
                Buffer.BlockCopy(saltBytes, 0, combinedBytes, 0, saltBytes.Length);
                Buffer.BlockCopy(passwordBytes, 0, combinedBytes, saltBytes.Length, passwordBytes.Length);

                var hashedBytes = sha256.ComputeHash(combinedBytes);
                return string.Format("{0}:{1}", Convert.ToBase64String(saltBytes), Convert.ToBase64String(hashedBytes));
            }
        }

        public static bool VerifyPassword(string password, string storedHash)
        {
            if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(storedHash))
            {
                return false;
            }

            var parts = storedHash.Split(':');
            if (parts.Length != 2)
            {
                return false;
            }

            var saltBytes = Convert.FromBase64String(parts[0]);
            var expectedHash = parts[1];

            using (var sha256 = SHA256.Create())
            {
                var passwordBytes = Encoding.UTF8.GetBytes(password);
                var combinedBytes = new byte[saltBytes.Length + passwordBytes.Length];
                Buffer.BlockCopy(saltBytes, 0, combinedBytes, 0, saltBytes.Length);
                Buffer.BlockCopy(passwordBytes, 0, combinedBytes, saltBytes.Length, passwordBytes.Length);

                var currentHash = Convert.ToBase64String(sha256.ComputeHash(combinedBytes));
                return string.Equals(currentHash, expectedHash, StringComparison.Ordinal);
            }
        }
    }
}
