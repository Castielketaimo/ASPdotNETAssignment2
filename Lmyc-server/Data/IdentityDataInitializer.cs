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

                //Make the admin
                var dummyAdmin = new ApplicationUser
                {
                    UserName = "a@a.a",
                    Email = "a@a.a"
                };

                var adminId = await EnsureUser(serviceProvider, dummyAdmin, "P@$$w0rd");
                var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();
                var admin = await userManager.FindByIdAsync(adminId);
                await EnsureRole(serviceProvider, adminId, "Admin");
                var boats = GetBoats(admin);

                // Look for any boats in the database
                if (!context.Boat.Any())
                {
                    foreach (Boat b in boats)
                    {
                        context.Boat.Add(b);
                    }
                }
                context.SaveChanges();

                if (!context.Reservation.Any())
                {
                    var reservations = GetReservations(admin, boats.ElementAt(0));
                    foreach (Reservation r in reservations)
                    {
                        context.Reservation.Add(r);
                    }
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
        private static List<Boat> GetBoats(ApplicationUser admin)
        {
            List<Boat> boats = new List<Boat>()
            {
                new Boat()
                {
                    BoatName = "Sharqui",
                    LengthInFeet = 25,
                    Make = "C&C",
                    Year = 1981,
                    Picture = "https://patiliyo.com/wp-content/uploads/2017/07/ruyada-kedi-gormek.jpg",
                    CreatedBy = admin.Id,
                },
                new Boat()
                {
                    BoatName = "BoatyMcBoatFace",
                    LengthInFeet = 25,
                    Make = "PotatoLand",
                    Year = 1995,
                    Picture = "http://tinytimes.com/wp-content/uploads/2014/01/halifax-harbour-theodore-tugboat_1.jpg",
                    CreatedBy = admin.Id
                }
            };
            return boats;
        }

        private static List<Reservation> GetReservations(ApplicationUser admin, Boat boat)
        {
            List<Reservation> reservations = new List<Reservation>()
            {
                new Reservation()
                {
                    Boat = boat,
                    BoatId = boat.BoatId,
                    CreatedBy = admin.Id,
                    User = admin,
                    StartDateTime = DateTime.Now,
                    EndDateTime = DateTime.Now.AddHours(1)
                },
                new Reservation()
                {
                    Boat = boat,
                    BoatId = boat.BoatId,
                    CreatedBy = admin.Id,
                    User = admin,
                    StartDateTime = DateTime.Now.AddHours(2),
                    EndDateTime = DateTime.Now.AddHours(4)
                }
            };
            return reservations;
        }
    }
}
