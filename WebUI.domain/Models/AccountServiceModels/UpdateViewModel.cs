

using OnlineBanking.Domain.Enumerators;

namespace WebUI.domain.Model
{
    public class UpdateViewModel 
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
        public AccountType? AccountType { get; set; }
    }
}