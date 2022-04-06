using System;
using System.Security.Cryptography;
using System.Text;

namespace Squares.API.Domain.Helper
{
    public static class CommonMethod
    {
        public static string Encryption(string password,string salt)
        {
            if (string.IsNullOrWhiteSpace(password))
                return "";
            password += salt;

            return HashPassword(password);
        }
       private static string HashPassword(string saltPassword)
        {
            using var sha = SHA256.Create();

            var asBytes = Encoding.Default.GetBytes(saltPassword);

            var hashed = sha.ComputeHash(asBytes);

            return Convert.ToBase64String(hashed);
        }
    }
}
