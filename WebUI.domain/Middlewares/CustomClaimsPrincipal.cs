﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebUI.domain.Middlewares
{
    public static class CustomClaimsPrincipal
    {
        public static string GetUserName (this ClaimsPrincipal user)
        {
            //claims are like properties associated with a user??
           return user.FindFirst(ClaimTypes.Email)?.Value;            
        }
        
        public static string GetUserId (this ClaimsPrincipal user)
        {
            
            return user.FindFirst(ClaimTypes.NameIdentifier)?.Value;            
        }
        
        public static IEnumerable<string> GetUserRoles (this ClaimsPrincipal user)
        {
            //claims are like properties associated with a user??
           return user.Claims.Where(a => a.Type == ClaimTypes.Role).Select(a => a.Value);
                       
        }


    }
}
