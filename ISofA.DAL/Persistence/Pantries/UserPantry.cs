using ISofA.DAL.Core;
using ISofA.DAL.Core.Domain;
using ISofA.DAL.Core.Pantries;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ISofA.DAL.Persistence.Pantries
{
    public class UserPantry : Pantry<ISofAUser>, IUserPantry
    {
        public UserPantry(ISofADbContext context) : base(context)
        {
        }

		public ISofAUser GetUserWithFriendRequests(string UserId)
		{
			return Context.Users
				.Where(t => t.Id == UserId)
				.Include(t => t.FriendRequestsRecieved.Select(f=> f.Sender))
				.FirstOrDefault();
		}

		public ISofAUser GetUserWithFriends(string UserId)
		{
			return Context.Users
				.Where(t => t.Id == UserId)
				.Include(t=>t.Friends)
				.FirstOrDefault();
		}

        public void UpdateUser(ISofAUser user)
        {
            bool exists = Context.Users.Any(x => x.Id == user.Id);

            if (exists)
            {
                Context.Entry<ISofAUser>(user).State = EntityState.Modified;
            }
        }
    }
}
