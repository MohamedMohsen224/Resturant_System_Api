using Microsoft.AspNetCore.Identity;
using Resturant_Api_Core.Entites.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_Api_Reposatry.identity
{
    public static class Seed
    {
        public static async Task SeedData(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (!userManager.Users.Any())
            {
                var roles = new List<IdentityRole>
                {
                    new IdentityRole{Name = "Admin"},
                    new IdentityRole{Name = "User"},
                    new IdentityRole{Name = "Moderator"}
                };
                foreach (var role in roles)
                {
                    await roleManager.CreateAsync(role);
                }
                var admin = new AppUser
                {
                    UserName = "Admin",
                    Email = "Admin@gmail.com"
                    
                };
                await userManager.CreateAsync(admin, "Admin@123");
                await userManager.AddToRoleAsync(admin, "Admin");
            }
        }
    }
}
