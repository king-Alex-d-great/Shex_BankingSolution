using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using OnlineBanking.Domain.Entities;
using OnlineBanking.Domain.Enumerators;

namespace WebUI.domain.Model
{
    public class EnrollCustomerViewModel
    {
        [MinLength(4), MaxLength(50)]
        [Required]
        public string FirstName { get; set; }
        [Required]
        [MinLength(4), MaxLength(50)]
        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public AccountType AccountType { get; set; }
        [Required]
        public DateTime? Birthday { get; set; }
        public Gender Gender { get; set; }

        [BindNever]
        public ReadOnlyCustomerProps ReadOnlyCustomerProps { get; set; }

    }
}