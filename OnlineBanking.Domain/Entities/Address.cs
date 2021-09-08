using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace OnlineBanking.Domain.Entities
{
    public class Address
    {
        public int AddressId { get; set; }

        [Required]
        public int PlotNo { get; set; }

        [Required, MaxLength(50), MinLength(3)]
        public string StreetName { get; set; }

        [Required, MaxLength(30), MinLength(3)]
        public string City { get; set; }

        [Required, MaxLength(30), MinLength(3)]
        public string State { get; set; }

        [Required, MaxLength(30), MinLength(3)]
        public string Nationality { get; set; }

        public  int CustomerId { get; set; }

        public Customer Customers { get; set; }
    }
}
