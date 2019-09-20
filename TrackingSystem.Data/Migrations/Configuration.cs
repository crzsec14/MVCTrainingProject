namespace TrackingSystem.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using TrackingSystem.Data.Interfaces;
    using TrackingSystem.Data.Models;
    using TrackingSystem.Data.Services;

    internal sealed class Configuration : DbMigrationsConfiguration<TrackingSystem.Data.Database.TrackingSystemDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            //To add the data
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(TrackingSystem.Data.Database.TrackingSystemDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            IPasswordHasher hasher = new PasswordHasher();

            context.Users.AddOrUpdate(new User
            {
                Id = 1,
                Username = "admin",
                Password = hasher.Hash("admin"),
                UserType = Enums.UserType.Admin,
                Attempts = 0,
                CreatedBy = 0,
                CreatedDate = DateTime.Now,
                Status = Enums.Status.None
            });
            context.Users.AddOrUpdate(new User
            {
                Id = 2,
                Username = "sysadmin",
                Password = hasher.Hash("admin"),
                UserType = Enums.UserType.Admin,
                Attempts = 0,
                CreatedBy = 0,
                CreatedDate = DateTime.Now,
                Status = Enums.Status.None
            });
            context.Users.AddOrUpdate(new User
            {
                Id = 3,
                Username = "user",
                Password = hasher.Hash("admin"),
                UserType = Enums.UserType.Admin,
                Attempts = 0,
                CreatedBy = 0,
                CreatedDate = DateTime.Now,
                Status = Enums.Status.None
            });
            base.Seed(context);
        }
    }
}

