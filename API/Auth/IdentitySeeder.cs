using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace API.Auth
{

    // This class is responsible for seeding the database with an initial admin user and role
    public static class IdentitySeeder
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager,
        // RoleManager is used to manage roles in ASP.NET Identity

        RoleManager<IdentityRole> roleManager)
        {
            // Check if the admin role exists, if not create it
            if(!await roleManager.RoleExistsAsync("Admin"))
            {
                // Create the admin role
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }
            // Check if the admin user exists
            var admin = await userManager.FindByNameAsync("Thangi");
            //if the admin user does not exist, create it and assign the admin role
            if (admin == null)
            {
                // Create the admin user
                admin = new ApplicationUser
                {
                    UserName = "Thangi",
                    Email ="thatflythangi@gmail.com"
                };
                // Create the admin user with a password and assign the admin role
                await userManager.CreateAsync(admin, "Thangi@12");
                // Assign the admin role to the user so that they have the necessary permissions to access admin-only features
                await userManager.AddToRoleAsync(admin,"Admin");
            }
        }
    }
}