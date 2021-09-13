using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using OnlineBanking.Domain.Enumerators;
using OnlineBanking.Domain.Interfaces;

namespace OnlineBanking.Domain.Entities
{
    public class User : IdentityUser, IEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool? IsActive { get; set; }
        public int? AccountId { get; set; }
        public int? CustomerId { get; set; }
        public Customer Customer { get; set; }
        public Account Account { get; set; }               
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }        
    }
}
