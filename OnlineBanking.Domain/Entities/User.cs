using Microsoft.AspNetCore.Identity;
using OnlineBanking.Domain.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineBanking.Domain.Entities
{
    public class User : IdentityUser, IEntity
    {
        public string FullName { get; set; }
        public bool StillHasDefaultPassword { get; set; }
        [Display(Name = "Profile Picture")]
        public byte[] ProfilePicture { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
