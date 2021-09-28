using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using OnlineBanking.Domain.Enumerators;

namespace OnlineBanking.Domain.Entities
{
    public class Transaction
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
        public TransactionMode TransactionMode { get; set; }
        public TransactionType TransactionType { get; set; }
        [Column(TypeName = "decimal(38,2)")]
        public decimal Amount { get; set; }
        public DateTime TimeStamp { get; set; }
        public bool IsSuccessful { get; set; }
        public string SenderAccount { get; set; }
        public string ReceiverAccount{ get; set; }
    }
}
