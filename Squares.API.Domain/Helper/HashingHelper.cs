using System;
using System.Security.Cryptography;
using System.Text;

namespace Squares.API.Domain.Helper
{
    public static class HashingHelper
    {
        /// <summary>
        /// This method is for encrypting the password with salt
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        public static string Encryption(string password,string salt)
        {
            if (string.IsNullOrWhiteSpace(password))
                return "";
            password += salt;

            return HashPassword(password);
        }

        /// <summary>
        /// This method is for hashing saltPassword
        /// </summary>
        /// <param name="saltPassword"></param>
        /// <returns></returns>
       private static string HashPassword(string saltPassword)
        {
            using var sha = SHA256.Create();

            var asBytes = Encoding.Default.GetBytes(saltPassword);

            var hashed = sha.ComputeHash(asBytes);

            return Convert.ToBase64String(hashed);
        }
    }
}
