using ISofA.DAL.Core;
using ISofA.DAL.Core.Domain;
using ISofA.SL.DTO;
using ISofA.SL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ISofA.SL.Implementations
{
    public class FriendRequestService : Service, IFriendRequestService
    {
        public FriendRequestService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

		public IEnumerable<ISofAUserDTO> GetFriends(string UserId)
		{
			var user = UnitOfWork.Users.GetUserWithFriendRequests(UserId);


			return user.FriendRequestsRecieved
				.Where(x => x.Accepted == true)
				.Select(x => x.Sender)
				.Select(x => new ISofAUserDTO(x))
				.Concat(
					user.FriendRequestsSent
					.Where(x => x.Accepted == true)
					.Select(x => x.Reciever)
					.Select(x => new ISofAUserDTO(x))
				);
		}

		public bool RemoveFriend(string UserId, string FriendId)
		{
			var friendRequest = UnitOfWork.FriendRequests.Get(new object[] { UserId, FriendId });
			var friendRequest2 = UnitOfWork.FriendRequests.Get(new object[] { FriendId, UserId });

			if (friendRequest != null)
			{
				UnitOfWork.FriendRequests.Remove(friendRequest);
				UnitOfWork.SaveChanges();
				return true;
			}
			else if (friendRequest2 != null)
			{
				UnitOfWork.FriendRequests.Remove(friendRequest2);
				UnitOfWork.SaveChanges();
				return true;
			}

			return false;
		}

		public bool AcceptFriendRequest(string RecieverId, string SenderId)
        {
            var friendRequest = UnitOfWork.FriendRequests.Get(new object[] {SenderId, RecieverId});
            var sender = UnitOfWork.Users.Get(SenderId);
            var reciever = UnitOfWork.Users.GetUserWithFriends(RecieverId);
            if (friendRequest == null)
                return false;
			friendRequest.Accepted = true;
			//UnitOfWork.FriendRequests.Remove(friendRequest);
            
			//reciever.Friends.Add(sender);
			UnitOfWork.SaveChanges();
            return true;
        }

        public bool DeclineFriendRequest(string RecieverId, string SenderId)
        {
            var friendRequest = UnitOfWork.FriendRequests.Get(new object[] { SenderId, RecieverId });
            var sender = UnitOfWork.Users.Get(SenderId);
            var reciever = UnitOfWork.Users.Get(RecieverId);
            if (friendRequest == null)
                return false;
			friendRequest.Accepted = false;
			//UnitOfWork.FriendRequests.Remove(friendRequest);
			UnitOfWork.SaveChanges();
            return true;
        }

        public IEnumerable<ISofAUserDTO> GetFriendRequests(string UserId)
        {
			var user = UnitOfWork.Users.GetUserWithFriendRequests(UserId);
			
            return user.FriendRequestsRecieved.Select(x => x.Sender).Select(x=> new ISofAUserDTO(x));
        }

        public bool SendFriendRequest(string RecieverId, string SenderId)
        {
            var reciever = UnitOfWork.Users.Get(RecieverId);
            if (reciever == null)
                return false;
			UnitOfWork.FriendRequests.Add(new FriendRequest() { SenderId = SenderId, RecieverId = RecieverId });
			UnitOfWork.SaveChanges();
			
			return true;
        }
    }
}
