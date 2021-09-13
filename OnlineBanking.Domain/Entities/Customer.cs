using System;
using System.Collections.Generic;
using System.Text;
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

    }
}
