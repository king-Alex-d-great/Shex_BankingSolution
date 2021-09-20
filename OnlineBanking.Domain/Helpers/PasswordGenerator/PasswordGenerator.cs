using System;
using System.Security.Cryptography;

namespace OnlineBanking.Domain.Helpers.PasswordGenerator
{
    public static class PasswordGenerator
    {
        public static string GeneratePassword(char fName, char lName)
        {
            var firstThreeDgites = string.Format("{0:000}", (int)fName);
            var lastThreeDgites = string.Format("{0:000}", (int)lName);

            var specialXter = ((char)RandomNumberGenerator.GetInt32(33, 47)).ToString();
            var specialXter2 = ((char)RandomNumberGenerator.GetInt32(91, 96)).ToString();
            var smallLetter1 = ((char)RandomNumberGenerator.GetInt32(97, 122)).ToString();
            var smallLetter2 = ((char)RandomNumberGenerator.GetInt32(97, 122)).ToString();
            var bigLetter1 = ((char)RandomNumberGenerator.GetInt32(65, 90)).ToString();
            var bigLetter2 = ((char)RandomNumberGenerator.GetInt32(65, 90)).ToString();
            var password = firstThreeDgites + smallLetter1 + specialXter + bigLetter1 + smallLetter2 + specialXter2 + bigLetter2 + lastThreeDgites;
            return (password);
        }

       
    }
}
