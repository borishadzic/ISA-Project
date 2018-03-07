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
    public class ISofADbContext : IdentityDbContext<ISofAUser>
    {
        public ISofADbContext()
            : base("ISofADb", throwIfV1Schema: false)
        {
        }

        public DbSet<Theater> Theaters { get; set; }
        public DbSet<Stage> Stages { get; set; }
        public DbSet<Play> Plays { get; set; }
        public DbSet<Projection> Projections { get; set; }
        public DbSet<Seat> Seats { get; set; }
		public DbSet<FriendRequest> FriendRequests { get; set; }

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

            modelBuilder.Entity<Theater>()
                .HasMany(x => x.Stages)
                .WithRequired(x => x.Theater)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Theater>()
                .HasMany(x => x.Repertoire)
                .WithRequired(x => x.Theater)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Theater>()
                .HasMany(x => x.Projections)
                .WithRequired(x => x.Theater)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Stage>()
                .HasMany(x => x.Projections)
                .WithRequired(x => x.Stage)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Play>()
                .HasMany(x => x.Projections)
                .WithRequired(x => x.Play)
                .WillCascadeOnDelete(false);

			modelBuilder.Entity<ISofAUser>()
				.HasMany(x => x.Friends)
				.WithMany()
				.Map(m =>
				{
					m.MapLeftKey("Id");
					m.MapRightKey("FriendId");
					m.ToTable("Friends");
				});

            modelBuilder.Entity<ISofAUser>()
                .HasMany(x => x.Reservations)
                .WithRequired(x => x.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ISofAUser>()
                .HasMany(x => x.BoughtItems)
                .WithOptional(x => x.Buyer)
                .WillCascadeOnDelete(false);

			modelBuilder.Entity<FriendRequest>()
				.HasRequired(x => x.Reciever)
				.WithMany(x => x.FriendRequestsRecieved)
				.HasForeignKey(x => x.RecieverId)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<FriendRequest>()
				.HasRequired(x => x.Sender)
				.WithMany(x => x.FriendRequestsSent)
				.HasForeignKey(x => x.SenderId)
				.WillCascadeOnDelete(false);

            modelBuilder.Entity<Theater>()
                .HasMany(x => x.Items)
                .WithRequired(x => x.Theater)
                .HasForeignKey(x => x.TheaterId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Theater>()
                .HasMany(x => x.UserItems)
                .WithRequired(x => x.Theater)
                .HasForeignKey(x => x.TheaterId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ISofAUser>()
                .HasMany(x => x.UserItems)
                .WithRequired(x => x.ISofaUser)
                .HasForeignKey(x => x.ISofAUserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ISofAUser>()
                .HasMany(x => x.UserBids)
                .WithRequired(x => x.Bidder)
                .HasForeignKey(x => x.BidderId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserItem>()
                .HasMany(x => x.Bids)
                .WithRequired(x => x.UserItem)
                .HasForeignKey(x => x.UserItemId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserItem>()
                .HasOptional(x => x.HighestBidder)
                .WithMany()
                .HasForeignKey(x => x.HighestBidderId)
                .WillCascadeOnDelete(false);
        }

        public static ISofADbContext Create()
        {
            return new ISofADbContext();
        }
    }
}
