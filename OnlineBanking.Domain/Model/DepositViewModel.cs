using System;
using System.Collections.Generic;
using System.Text;
using BEZAO_PayDAL.Interfaces;

namespace OnlineBanking.Domain.Model
{
    public class DepositViewModel
    {
        public int RecipientAccountNumber { get; set; }
        public decimal Amount { get; set; }

    }

}
