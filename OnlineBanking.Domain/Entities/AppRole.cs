using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using OnlineBanking.Domain.Interfaces;

namespace OnlineBanking.Domain.Entities
{
   public class AppRole: IdentityRole, IEntity
    {
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
