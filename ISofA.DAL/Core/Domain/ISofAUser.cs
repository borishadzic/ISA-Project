using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ISofA.DAL.Core.Domain
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ISofAUser : IdentityUser
    {
		
		public string Name { get; set; }
		public string Surname { get; set; }
		public string City { get; set; }
		public virtual ICollection<ISofAUser> Friends { get; set; }
		public virtual ICollection<FriendRequest> FriendRequestsSent { get; set; }
		public virtual ICollection<FriendRequest> FriendRequestsRecieved { get; set; }
		public virtual ICollection<Theater> FanZoneTheaters { get; set; }
        public virtual ICollection<Theater> AdminTheaters { get; set; }
        public virtual ICollection<Seat> Reservations { get; set; }
        public virtual ICollection<Item> BoughtItems { get; set; }
        public virtual ICollection<UserItem> UserItems { get; set; }
        public virtual ICollection<Bid> UserBids { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ISofAUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }
}