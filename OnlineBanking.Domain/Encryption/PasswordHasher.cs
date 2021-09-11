using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace OnlineBanking.Domain.Encryption
{
    public static class PasswordHasher
    {
        static string salt = "hashpassword";
        static string _salt;
        static RNGCryptoServiceProvider rand = new RNGCryptoServiceProvider();

        private static string Salt
        {
            get
            {
                var salt = new byte[8];
                rand.GetBytes(salt);
                _salt = BitConverter.ToString(salt).ToLower() + salt;
                return _salt;
            }
        }

        public static string GetSalt()
        {
            var salt = new byte[16];
            rand.GetBytes(salt);
            return BitConverter.ToString(salt).ToLower();
        }
        public static string HashPassword(byte[] password, byte[] salt)
        {
            var securePassword = new Rfc2898DeriveBytes(password, salt, 10000);
            var saltedPassword = Convert.ToBase64String(securePassword.GetBytes(30));
            var Password = saltedPassword + _salt;
            return Password;

        }
    }
}
