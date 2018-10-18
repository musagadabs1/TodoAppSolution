using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
//using Microsoft.AspNetCore.WebSockets.Internal;
using Microsoft.Extensions.DependencyInjection;
using TodoApp.Models;

namespace TodoApp
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider services)
        {
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            await EnsureRolesAsync(roleManager);

            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            await EnsureTestAdminAsync(userManager);
        }
        private static async Task EnsureRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            var alreadyExists = await roleManager.RoleExistsAsync(Models.Constants.AdministratorRole);

            if (alreadyExists)
            {
                return;
            }
            await roleManager.CreateAsync(new IdentityRole(Models.Constants.AdministratorRole));
        }

        public static async Task EnsureTestAdminAsync(UserManager<ApplicationUser> userManager)
        {
            var testAdmin = await userManager.Users.Where(x => x.UserName == "musagadabs1@yahoo.com").SingleOrDefaultAsync();
            if (testAdmin !=null)
            {
                return;
            }
            testAdmin = new ApplicationUser
            {
                UserName = "musagadabs1@yahoo.com",
                Email="musagadabs1@yahoo.com"

            };
            await userManager.CreateAsync(testAdmin, "NotSecured123!");
            await userManager.AddToRoleAsync(testAdmin, Constants.AdministratorRole);

        }

    }
}
