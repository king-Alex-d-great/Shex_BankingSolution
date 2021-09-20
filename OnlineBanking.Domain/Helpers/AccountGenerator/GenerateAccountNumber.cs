using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace OnlineBanking.Domain.Helpers.AccountGenerator
{
    public static class AccountNumberGenerator
    {
        public static string GenerateAccountNumber()
        {
            //start:
            var milisecndns = string.Format("{0:000}", DateTime.Now.Millisecond);
            var year = DateTime.Now.ToString("yy");
            var month = string.Format("{0:00}", DateTime.Now.Month);
            var day = (RandomNumberGenerator.GetInt32(100, 999)).ToString();

            /*var rr = new string[] { milisecndns, year, month, day };
            var count = 0;
            var newList = new string[4] ;

            while(count < rr.Length)
            {
                start:
                var random = RandomNumberGenerator.GetInt32(1, 4);
                for(var I = 0; I < newList.Length; I++)
                {                    
                    if (newList[I] == rr[random])
                    {
                        goto start;
                    }
                    newList[I] = rr[random];
                }                
            }*/
            var accountNumber = year + month + milisecndns + day; //randomize
                                                                  //check length
                                                                  //check recurrence

            return accountNumber;
        }
    }
}
