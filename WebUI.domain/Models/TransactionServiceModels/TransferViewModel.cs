using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using OnlineBanking.Domain.Entities;

namespace WebUI.domain.Models.TransactionServiceModels
{
    public class TransferViewModel
    {
        [Required(ErrorMessage = "Please provide a valid account number")]
       // [Range(typeof(int), "10000000", "999999999", ErrorMessage = "Invalid Account Number! Account Number must contain 10 digits")]

        [MinLength(10, ErrorMessage = "Invalid Account Number! Account Number must contain 10 digits")]
        [MaxLength(10, ErrorMessage = "Invalid Account Number! Account Number must contain 10 digits")]
        public string ReciepientAccountNumber { get; set; }
        [Required(ErrorMessage = "Invalid Amount!")]
        [Range(typeof(decimal), "100000", "900000000000000000", ErrorMessage = "Transfer amount must be between $100000 - $900000000000000000\nAmount less than $100000, Please use the Atm\nAmount over  $900000000000 is not allowed at a go --CBU")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        [BindNever]
        public Customer customer { get; set; }
    }
}
