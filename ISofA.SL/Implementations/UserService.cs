using ISofA.SL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISofA.SL.DTO;
using ISofA.DAL.Core;
using ISofA.DAL.Core.Domain;

namespace ISofA.SL.Implementations
{
	public class UserService : Service, IUserService
	{
		private readonly IFriendRequestService _friendRequestService;
		public UserService(IUnitOfWork unitOfWork, IFriendRequestService _friendRequestService) : base(unitOfWork)
		{
			this._friendRequestService = _friendRequestService;
		}

		public ISofAUserDTO GetUserProfile(string userId)
		{
			return UnitOfWork.Users.Find(x => x.Id == userId).Select(x => new ISofAUserDTO(x)).FirstOrDefault();
		}

		public IEnumerable<ISofAUserDTO> GetNonFriendUsers(string userId)
		{
			var friendsId = _friendRequestService.GetFriends(userId).Select(x=> x.Id);

			return UnitOfWork.Users.Find(x => !friendsId.Contains(x.Id) && x.Id!=userId).Select(x => new ISofAUserDTO(x));
			
		}


		public IEnumerable<ISofAUserDTO> GetUsers(string userId, string name)
		{
			var friendsId = _friendRequestService.GetFriends(userId).Select(x => x.Id);

			return UnitOfWork.Users.Find(x => !friendsId.Contains(x.Id) && x.Id != userId && (x.Name.ToLower().StartsWith(name.ToLower()) || x.Surname.ToLower().StartsWith(name.ToLower()) || x.Email.ToLower().StartsWith(name.ToLower()))).Select(x => new ISofAUserDTO(x));
		}

        public void ChangeUserDetails(string userId, string Name, string Surname, string City, string PhoneNumber)
        {
            ISofAUser user = UnitOfWork.Users.Get(userId);
            if (!Name.Equals("") && Name!=null)
                user.Name = Name;
            if (!Surname.Equals("") && Surname!=null)
                user.Surname = Surname;
            if (!City.Equals("") && City != null)
                user.City = City;
            if (!PhoneNumber.Equals("") && PhoneNumber!=null)
                user.PhoneNumber = PhoneNumber;
            UnitOfWork.SaveChanges();
        }
    }
}