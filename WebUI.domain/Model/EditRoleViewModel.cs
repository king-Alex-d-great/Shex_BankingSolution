using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineBanking.Domain.Model
{
    public class EditRoleViewModel
    {
        public string Id { get; set; }
        [Display(Name = "Role name")]
        [Required]
        public string RoleName { get; set; }
    }
}
