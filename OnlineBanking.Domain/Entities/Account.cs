using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using OnlineBanking.Domain.Enumerators;
using OnlineBanking.Domain.Interfaces;

namespace OnlineBanking.Domain.Entities
{
   public class Account: IEntity
    {
       
        public Guid Id { get; set; }
        /*public string FirstName { get; set; }
        public string LastName { get; set; }*/
        public bool? IsActive { get; set; }
        //public int? UserId { get; set; }
        //public int? CustomerId { get; set; }
        //public Customer Customer { get; set; }
        public string AccountNumber { get; set; }
        public decimal? Balance { get; set; }
        public AccountType? AccountType { get; set; }        
        /*public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }*/
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

    }
}
