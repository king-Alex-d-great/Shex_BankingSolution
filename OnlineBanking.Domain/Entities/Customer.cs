using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using OnlineBanking.Domain.Enumerators;

namespace OnlineBanking.Domain.Entities
{
  public  class Customer
    {
       
        public int Id { get; set; }

        [Required, MinLength(4), MaxLength(50)]
        public string FirstName { get; set; }
        
        [Required, MinLength(4), MaxLength(50)]
        public string LastName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public DateTime Birthday { get; set; }

        [Required]
        public Gender Gender { get; set; }

    }
}
