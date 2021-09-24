using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using OnlineBanking.Domain.Entities;

namespace WebUI.domain.Models.TransactionServiceModels
{
    public class WithdrawViewModel
    {

        [Required(ErrorMessage = "Invalid Amount!")]
        [Range(typeof(decimal), "1000", "900000000000000000",ErrorMessage = "Withdrawal amount must be between $100000 - $900000000000000000\nAmount less than $100000, Please use the Atm\nAmount over  $900000000000 is not allowed at a go --CBU")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount  {get; set;}
        [BindNever]
        public Customer customer{ get; set; }
    }
}
