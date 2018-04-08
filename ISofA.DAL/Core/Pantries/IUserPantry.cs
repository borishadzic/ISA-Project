using ISofA.DAL.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISofA.DAL.Core.Pantries
{
    public interface IUserPantry : IPantry<ISofAUser>
    {
		ISofAUser GetUserWithFriendRequests(string UserId);
		ISofAUser GetUserWithFriends(string UserId);
        void UpdateUser(ISofAUser user);
	}
}
