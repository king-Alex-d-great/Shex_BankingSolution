using OnlineBanking.Domain.Enumerators;
using System.ComponentModel.DataAnnotations;

namespace WebUI.domain.Models
{
    public class UpdateViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string PhoneNumber { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        [Display(Name = "Profile Picture")]
        public byte[] ProfilePicture { get; set; }
        public string Username { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
        public AccountType? AccountType { get; set; }
    }
}