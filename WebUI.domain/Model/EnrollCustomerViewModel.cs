using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OnlineBanking.Domain.Entities;
using OnlineBanking.Domain.Enumerators;

namespace WebUI.domain.Model
{
    public class EnrollCustomerViewModel
    {     

        public AccountType AccountType { get; set; }

        public DateTime Birthday { get; set; }

        public Gender Gender { get; set; }
    }
}