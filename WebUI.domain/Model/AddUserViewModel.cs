using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.domain.Model
{
    public class AddUserViewModel
    {        

        [MinLength(4), MaxLength(50)]
        public string FirstName { get; set; }

        [MinLength(4), MaxLength(50)]
        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }
    }
}
