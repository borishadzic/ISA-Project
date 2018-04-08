namespace ISofA.DAL.Migrations
{
    using ISofA.DAL.Core.Domain;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ISofA.DAL.Persistence.ISofADbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ISofA.DAL.Persistence.ISofADbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            var store = new UserStore<ISofAUser>(context);
            var manager = new UserManager<ISofAUser>(store);

            if (!context.Users.Any(u => u.UserName == "bokih.95@gmail.com"))
            {
                var user = new ISofAUser {
                    UserName = "bokih.95@gmail.com",
                    Email = "bokih.95@gmail.com",
                    EmailConfirmed = true,
                    ISofAUserRole = ISofAUserRole.SysAdmin
                };
                manager.Create(user, "Aa123.");
            }

            if (!context.Users.Any(u => u.UserName == "test@test.rs"))
            {
                var user = new ISofAUser
                {
                    UserName = "test@test.rs",
                    Email = "test@test.rs",
                    EmailConfirmed = true
                };
                manager.Create(user, "Aa123.");
            }
        }
    }
}
