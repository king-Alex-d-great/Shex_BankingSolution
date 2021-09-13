

using System;
using System.ComponentModel.DataAnnotations;
using OnlineBanking.Domain.Entities;
using OnlineBanking.Domain.Enumerators;

namespace OnlineBanking.Domain.Model
{
   public class RegisterViewModel
    {
        public Guid Id { get; set; }
        public int UserId { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        [Required(ErrorMessage = "Specify account type")]
        public AccountType AccountType { get; set; }   
        
        [Required(ErrorMessage = "Email required, this will serve as your username")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm password to proceed")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Pasword")]
        [Compare("Password", ErrorMessage ="Password dont match")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "First name required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name required")]
        public string LastName { get; set; } 
        public string AccountNumber { get; set; }
        public decimal? Balance { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; } = DateTime.Now;
        public string CreatedBy { get; set; } = "Alex King";
        public string UpdatedBy { get; set; } = "Shola nejo";
        public bool IsActive { get; set; } = true;
    }
}
