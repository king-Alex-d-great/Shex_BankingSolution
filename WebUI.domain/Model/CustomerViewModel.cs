using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using OnlineBanking.Domain.Enumerators;

namespace WebUI.domain.Models
{
    public class CustomerViewModel
    {

        [Required, MinLength(4), MaxLength(50)]
        public string FirstName { get; set; }

        [Required, MinLength(4), MaxLength(50)]
        public string LastName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        public AccountType AccountType { get; set; }

        [Required]
        public DateTime Birthday { get; set; }

        [Required]
        public Gender Gender { get; set; }

    }
}
