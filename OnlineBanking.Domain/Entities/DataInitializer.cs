using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace OnlineBanking.Domain.Entities
{
    internal static class DataInitializer
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            /*var accounts = new List<Account>
            {*/
            /*new Account
            {
                AccountNumber = "1234567890",
                AccountType = Enumerators.AccountType.Savings,
                Balance = 234588920,
                FirstName = "King",
                LastName = "Alex",
                Password = "1234abcd",
                ConfirmPassword = "124abcd",
                Email = "alexking@domain.com",
                *//*CreatedAt = DateTime.Now,
                CreatedBy = "King Shola",*//*
                IsActive = true,
                UpdatedAt = DateTime.Now,
                UpdatedBy = "King Shola",
                Customer = new Customer{Id = 1}
            },
            new Account
            {
                AccountNumber = "1234567891",
                AccountType = Enumerators.AccountType.Student,
                Balance = 234588920,
                FirstName = "King",
                LastName = "TMajor",
                Password = "1234abcd",
                ConfirmPassword = "124abcd",
                Email = "tmajorking@domain.com",
                CreatedAt = DateTime.Now,
                CreatedBy = "King",
                IsActive = true,
                UpdatedAt = DateTime.Now,
                UpdatedBy = "King",
                Customer = new Customer{Id = 2}
            }
        };
        modelBuilder.Entity<Customer>().HasData(accounts);*/

            /* }*/
        }
    }
}
