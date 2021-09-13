
using System.ComponentModel.DataAnnotations;

namespace OnlineBanking.Domain.Model
{
    public class LoginViewModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

    }
}