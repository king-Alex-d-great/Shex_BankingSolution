using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBanking.Domain.Entities
{
    internal static class DataInitializer
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var customers = new List<Customer>
            {
                 new Customer
                 {
                     Id = 1,
                     FirstName = "Dara",
                     LastName = "Success",
                     Email = "dara@domain.com",
                     Birthday = new DateTime(2000, 06, 21),
                     Gender = Enumerators.Gender.Female
                 },

                 new Customer
                 {
                     Id = 2,
                     FirstName = "Obinna",
                     LastName = "Achara",
                     Email = "obinna@gmail.com",
                     Birthday = new DateTime(2004, 10, 12),
                     Gender = Enumerators.Gender.Male
                 }

            };


            var accounts = new List<Account>
            {
                new Account
                {
                    Id = 1,
                    CustomerId = 1,
                    AccountNumber = 0211979756,
                    Balance =  23_456_782_340,
                    AccountType =Enumerators.AccountType.Saving,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    CreatedBy = "Dara",
                    UpdatedBy = "Dara"
                },

                new Account
                {
                    Id = 2,
                    CustomerId = 2,
                    AccountNumber = 0317092802,
                    Balance = 23_456_782_340,
                    AccountType = Enumerators.AccountType.Saving,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    CreatedBy = "Obinna",
                    UpdatedBy = "Obinna"
                }
            };
            modelBuilder.Entity<Customer>().HasData(customers);
            modelBuilder.Entity<Account>().HasData(accounts);
        }


    }
}
