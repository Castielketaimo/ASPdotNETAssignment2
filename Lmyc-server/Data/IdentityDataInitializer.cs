using Lmyc_server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lmyc_server.Data
{
    public class IdentityDataInitializer
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                string[] roleNames = { "Admin", "Member"};

                foreach (var roleName in roleNames)
                {
                    await EnsureRole(serviceProvider, null, roleName);
                }

                var admin = new ApplicationUser
                {
                    UserName = "a@a.a",
                    Email = "a@a.a"
                };

                var adminId = await EnsureUser(serviceProvider, admin, "P@$$w0rd");
                await EnsureRole(serviceProvider, adminId, "Admin");

                // Look for any boats in the database
                if (context.Boat.Any())
                {
                    return; // DB have been seeded
                }

                var boats = GetBoats();
                foreach (Boat b in boats)
                {
                    context.Boat.Add(b);
                }

                context.SaveChanges();
            }
        }



        private static async Task<IdentityResult> EnsureRole(IServiceProvider serviceProvider, string uid, string role)
        {
            IdentityResult identityResult = null;
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            if (!await roleManager.RoleExistsAsync(role))
            {
                identityResult = await roleManager.CreateAsync(new IdentityRole(role));
            }

            if (uid != null)
            {
                var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();

                var user = await userManager.FindByIdAsync(uid);

                identityResult = await userManager.AddToRoleAsync(user, role);
            }

            return identityResult;
        }


        private static async Task<string> EnsureUser(IServiceProvider serviceProvider, ApplicationUser newUser, string password)
        {
            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();
            var user = await userManager.FindByNameAsync(newUser.UserName);

            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = newUser.UserName,
                    Email = newUser.Email
                };

                await userManager.CreateAsync(user, password);
            }

            return user.Id;
        }
        private static List<Boat> GetBoats()
        {
            List<Boat> boats = new List<Boat>()
            {
                new Boat()
                {
                    BoatName = "Sharqui",
                    LengthInFeet = 25,
                    Make = "C&C",
                    Year = 1981,
                    Picture = "https://patiliyo.com/wp-content/uploads/2017/07/ruyada-kedi-gormek.jpg"
                }
            };
            return boats;
        }
    }
}
