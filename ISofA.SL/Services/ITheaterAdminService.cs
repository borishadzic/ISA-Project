using ISofA.SL.DTO;
using System.Collections.Generic;

namespace ISofA.SL.Services
{
    public interface ITheaterAdminService
    {
        IEnumerable<ISofAUserDTO> GetTheaterAdmins(int theaterId);
        ISofAUserDTO AddTheaterAdmin(int theaterId, string userId);
        void RemoveTheaterAdmin(int theaterId, string theaterAdminId);
    }
}
