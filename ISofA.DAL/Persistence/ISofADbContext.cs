using ISofA.DAL.Core;
using ISofA.DAL.Core.Domain;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISofA.DAL.Persistence
{
    public class ISofADbContext : IdentityDbContext<ISofAUser>, IISofADbContext
    {
        public ISofADbContext()
            : base("ISofADb", throwIfV1Schema: false)
        {
        }

        public DbSet<Theater> Theaters { get; set; }

        public override int SaveChanges()
        {
            return base.SaveChanges(); // TODO: Ne znam dal je potrebno da bi bilo vidljivo interfejsu IISofADbContext
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Theater>()
                .HasMany(u => u.FanZoneAdmins)
                .WithMany(t => t.FanZoneTheaters).Map(cs =>
            {
                cs.MapLeftKey("TheaterId");
                cs.MapRightKey("UserId");
                cs.ToTable("FanZoneAdmins");
            });

            modelBuilder.Entity<Theater>()
                .HasMany(u => u.TheaterAdmins)
                .WithMany(t => t.AdminTheaters).Map(cs =>
                {
                    cs.MapLeftKey("TheaterId");
                    cs.MapRightKey("UserId");
                    cs.ToTable("TheaterAdmins");
            });

        }

        public static ISofADbContext Create()
        {
            return new ISofADbContext();
        }
    }
}
