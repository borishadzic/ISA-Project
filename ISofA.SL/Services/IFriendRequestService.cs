using ISofA.DAL.Core.Domain;
using ISofA.SL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISofA.SL.Services
{
    public interface IFriendRequestService
    {
        IEnumerable<ISofAUserDTO> GetFriendRequests(string UserId);
        bool AcceptFriendRequest(string RecieverId, string SenderId);
        bool DeclineFriendRequest(string RecieverId, string SenderId);
        bool SendFriendRequest(string RecieverId, string SenderId);
    }
}
