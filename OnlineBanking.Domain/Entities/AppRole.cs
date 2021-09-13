using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using OnlineBanking.Domain.Interfaces;

namespace OnlineBanking.Domain.Entities
{
    class AppRole : IdentityRole, IEntity
    {
        
    }
}
