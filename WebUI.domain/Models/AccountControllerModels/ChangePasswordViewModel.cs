using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebUI.domain.Models.AccountControllerModels
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Password required")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Password required")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; } 
    }
}
