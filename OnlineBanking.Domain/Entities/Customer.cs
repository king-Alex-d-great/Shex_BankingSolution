using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OnlineBanking.Domain.Enumerators;
using OnlineBanking.Domain.Interfaces;

namespace OnlineBanking.Domain.Entities
{
  public class Customer: IEntity
    {
      
        public int Id { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }
      
        public DateTime Birthday { get; set; }

        public Gender Gender { get; set; }

        public Guid AccountId { get; set; }

        [ForeignKey(nameof(AccountId))]
        public Account Account { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
