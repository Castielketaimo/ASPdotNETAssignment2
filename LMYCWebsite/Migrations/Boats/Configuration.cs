namespace LMYCWebsite.Migrations.Boats
{
    using LMYCWebsite.Data;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<LmycDataLib.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\Boats";
        }

        protected override void Seed(LmycDataLib.Models.ApplicationDbContext context)
        {
            context.boats.AddOrUpdate(
              b => b.BoatId, BoatDummyData.getBoats().ToArray());

            context.SaveChanges();
        }
    }
}
