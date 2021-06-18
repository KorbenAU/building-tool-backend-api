using System;
using System.Security.Cryptography;
using System.Text;

namespace Microservice.Services.Encryption
{
    public class Hash
    {
        private const int NewSaltSize = 32;
        
        public static string CreateSalt()
        {
            var rng = new RNGCryptoServiceProvider();
            var buff = new byte[NewSaltSize];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }

        public static string GenerateHash(string input, string salt)
        { 
            var bytes = Encoding.UTF8.GetBytes(input + salt);
            var sha256ManagedString = new SHA256Managed();
            var hash = sha256ManagedString.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        public static bool AreEqual(string plainTextInput, string hashedInput, string salt)
        {
            var hash = GenerateHash(plainTextInput, salt);
            return hash.Equals(hashedInput); 
        }
        
    }
}