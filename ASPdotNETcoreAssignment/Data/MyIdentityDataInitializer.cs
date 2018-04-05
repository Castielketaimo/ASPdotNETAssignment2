using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPdotNETcoreAssignment.Models
{
    public static class MyIdentityDataInitializer
    {
        public static void SeedData
        (UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

        public static void SeedUsers
        (UserManager<IdentityUser> userManager)
        {
            if (userManager.FindByNameAsync
                ("a").Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "a",
                    Email = "a@a.a"
                };

                IdentityResult result = userManager.CreateAsync
                (user, "P@$$w0rd").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user,
                                        "Admin").Wait();
                }
            }


            if (userManager.FindByNameAsync
                ("m").Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "m",
                    Email = "m@m.m"
                };

                IdentityResult result = userManager.CreateAsync
                (user, "P@$$w0rd").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user,
                                        "Member").Wait();
                }
            }
        }

        public static void SeedRoles
        (RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync
            ("Admin").Result)
            {
                IdentityRole role = new IdentityRole
                {
                    Name = "Admin"
                };
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }


            if (!roleManager.RoleExistsAsync
               ("Member").Result)
            {
                IdentityRole role = new IdentityRole
                {
                    Name = "Member"
                };
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }
        }
    }
}
