using System;
using System.ComponentModel.DataAnnotations;
using OnlineBanking.Domain.Enumerators;
using OnlineBanking.Domain.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineBanking.Domain.Entities
{
   public class Account: IEntity
    {
        [Key]
        public Guid Id { get; set; }

        [MinLength(10)]
        [MaxLength(10)]
        public string AccountNumber { get; set; }

        [Column(TypeName = "decimal(38,2)")]
        public decimal Balance { get; set; }
        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public AccountType AccountType { get; set; }

        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }
       
    }
}
