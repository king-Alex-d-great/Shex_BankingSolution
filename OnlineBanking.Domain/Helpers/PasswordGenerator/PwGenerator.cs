using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace OnlineBanking.Domain.Helpers.PasswordGenerator
{
    class PwGenerator
    {
        public void GeneratePassword(char firstname, char lastname)
        {
            var midFourCharacters = (RandomNumberGenerator.GetInt32(10, 99)).ToString() + (RandomNumberGenerator.GetInt32(10, 99)).ToString();
        }
    }
}
