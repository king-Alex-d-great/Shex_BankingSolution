using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OnlineBanking.Domain.Entities;
using OnlineBanking.Domain.Enumerators;

namespace WebUI.domain.Model
{
    public class EnrollCustomerViewModel
    {
        public EnrollCustomerViewModel()
        {

        }

        [MinLength(4), MaxLength(50)]
        public string FirstName { get; set; }

        [MinLength(4), MaxLength(50)]
        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public AccountType AccountType { get; set; }

        public DateTime Birthday { get; set; }

        public Gender Gender { get; set; }
        
    }
}