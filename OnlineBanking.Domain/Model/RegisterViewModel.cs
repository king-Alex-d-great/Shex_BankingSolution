

using System;
using System.ComponentModel.DataAnnotations;
using OnlineBanking.Domain.Enumerators;

namespace OnlineBanking.Domain.Model
{
   public class RegisterViewModel
    {
        public Guid Id { get; set; }
        public int UserId { get; set; }
        public int CustomerId { get; set; }
        
      //  [Required(ErrorMessage ="Account Number required")]
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }

        [Required(ErrorMessage = "Specify account type")]
        public AccountType AccountType { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; } = null;
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; } = null;
        [Required(ErrorMessage = "Email required, this will serve as your username")]

        public string Email { get; set; }
        [Required(ErrorMessage = "Password required")]

        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm password to proceed")]

        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "First name required")]

        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name required")]

        public string LastName { get; set; }
    }
}
