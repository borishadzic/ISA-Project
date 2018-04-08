using ISofA.DAL.Core.Domain;
using ISofA.SL.DTO;
using System.Collections.Generic;

namespace ISofA.SL.Services
{
    public interface IAdminService
    {
        IEnumerable<ISofAUserDTO> GetAdmins(int theaterId);
        IEnumerable<ISofAUserDTO> GetTheaterAdmins(int theaterId);
        IEnumerable<ISofAUserDTO> GetFanZoneAdmins(int theaterId);
        ISofAUserDTO AddTheaterAdmin(int theaterId, ISofAUser user);
        ISofAUserDTO AddFanZoneAdmin(int theaterId, ISofAUser user);
        void RemoveAdmin(int theaterId, string theaterAdminId);
    }
}
