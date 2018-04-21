using ISofA.SL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISofA.SL.Services
{
	public interface IUserService
	{
		ISofAUserDTO GetUserProfile(string userId);
		IEnumerable<ISofAUserDTO> GetNonFriendUsers(string userId);
		IEnumerable<ISofAUserDTO> GetUsers(string userId, string name);
        void ChangeUserDetails(string userId, string Name, string Surname, string City, string PhoneNumber);
	}
}
