
using System.ComponentModel.DataAnnotations;

namespace OnlineBanking.Domain.Model
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Email Required")]
        public string UsernameEmail { get; set; }
        public string Password { get; set; }

    }
}