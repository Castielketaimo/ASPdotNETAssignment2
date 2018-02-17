namespace LMYCWebsite.Migrations.Boatsandusers
{
    using LmycDataLib.Models;
using LMYCWebsite.Data;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

internal sealed class Configuration : DbMigrationsConfiguration<LmycDataLib.Models.ApplicationDbContext>
{
    public Configuration()
    {
        AutomaticMigrationsEnabled = false;
        MigrationsDirectory = @"Migrations\Boatsandusers";
    }

    protected override void Seed(LmycDataLib.Models.ApplicationDbContext context)
    {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            if (!roleManager.RoleExists("Admin"))
                roleManager.Create(new IdentityRole("Admin"));
            if (!roleManager.RoleExists("Member"))
                roleManager.Create(new IdentityRole("Member"));

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            if (userManager.FindByName("a") == null)
            {
                var user = new ApplicationUser
                {
                    UserName = "a",
                    Email = "a@a.a",
                };
                var result = userManager.Create(user, "P@$$w0rd");

                if (result.Succeeded)
                    userManager.AddToRole(userManager.FindByName("a").Id, "Admin");
               
            }
            if (userManager.FindByName("m") == null)
            {
                var user = new ApplicationUser
                {
                    UserName = "m",
                    Email = "m@m.m",
                };
                var result = userManager.Create(user, "P@$$w0rd");
                if (result.Succeeded)
                    userManager.AddToRole(userManager.FindByName("m").Id, "Member");
                 
            }

            context.boats.AddOrUpdate(
              b => b.BoatId, BoatDummyData.getBoats().ToArray());

        context.SaveChanges();
    }
}
}
