﻿using LM.Domain.Entities;
using LM.Domain.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;


namespace LM.Persistence.DataInitializer
{
    public class UserAndRoleDataInitializer
    {
        public static void SeedData(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<LMUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

        /// <summary>
        /// All default users are seeded into the database in this static method.
        /// </summary>
        /// <param name="userManager"></param>
        private static void SeedUsers(UserManager<LMUser> userManager)
        {
            if (userManager.FindByEmailAsync("admin@lms.com").Result == null)
            {
                LMUser user = new LMUser()
                {
                    FirstName = "Admin",
                    LastName = "Lms",
                    UserName = "admin@lms.com",
                    RegisteredDate = DateTime.Now,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    Email = "admin@lms.com",
                    EmailConfirmed = true,
                    IsActive = true,
                    LockoutEnabled = false
                };

                IdentityResult result = userManager.CreateAsync(user, "Admin_4Lm").Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, CoreConstants.SuperAdmin).Wait();
                }
            }

        }

        /// <summary>
        /// This static method creates all the necessary roles for users of this application.
        /// </summary>
        /// <param name="roleManager"></param>
        private static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync(CoreConstants.SuperAdmin).Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = CoreConstants.SuperAdmin;
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }


            if (!roleManager.RoleExistsAsync(CoreConstants.Customer).Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = CoreConstants.Customer;
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync(CoreConstants.Client).Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = CoreConstants.Client;
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }

        }
    }
}
