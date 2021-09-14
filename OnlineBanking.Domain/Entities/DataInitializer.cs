using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineBanking.Domain.Enumerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.Domain.Entities
{
    public static class DataInitializer
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
           
        }

        public static async Task SeedRolesAsync(UserManager<User> userManager, RoleManager<AppRole> roleManager)
        {
            await roleManager.CreateAsync(new AppRole { Name = Roles.SuperAdmin.ToString(), CreatedAt = DateTime.Now, CreatedBy = "Shola Nejo" });
            await roleManager.CreateAsync(new AppRole { Name = Roles.Admin.ToString(), CreatedAt = DateTime.Now, CreatedBy = "Shola Nejo" });
            await roleManager.CreateAsync(new AppRole { Name = Roles.Customer.ToString(), CreatedAt = DateTime.Now, CreatedBy = "Shola Nejo" });
        }

        public static async Task SeedSuperAdminAsync(UserManager<User> userManager, RoleManager<AppRole> roleManager)
        {
            var defaultUser = new User
            {
                UserName = "sholanejo@gmail.com",
                Email = "superadmin@gmail.com",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                FullName = "Shola Nejo",
                PhoneNumber = "213-456-789",
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "Shola-1234");
                    //await userManager.AddToRoleAsync(defaultUser, Roles.Customer.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.SuperAdmin.ToString());
                    //await userManager.AddToRoleAsync(defaultUser, Roles.Admin.ToString());
                }
            }
        }

    }
}
