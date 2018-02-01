namespace ISofA.DAL.Migrations
{
    using ISofA.DAL.Core.Domain;
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
            context.Users.Add(new ISofAUser() { UserName = "Dejan", Email = "Dejan", PasswordHash = "0" });
        }
    }
}
