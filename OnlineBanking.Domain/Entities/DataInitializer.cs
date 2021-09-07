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
                     
                 },

                 new QuotesViewModel
                 {
                     Id = 2,
                     Quote = "Just flow with the moment",
                     Author = "Annonymous",
                 },

                 new QuotesViewModel
                 {
                     Id = 3,
                     Quote = "The end will justify the beginning",
                     Author = "Annonymous",
                 },

            };
            modelBuilder.Entity<Customer>().HasData(customers);
        }


    }
}
