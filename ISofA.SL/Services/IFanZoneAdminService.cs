using ISofA.SL.DTO;
using System.Collections.Generic;

namespace ISofA.SL.Services
{
    public interface IFanZoneAdminService
    {
        IEnumerable<ISofAUserDTO> GetFanZoneAdmins(int theaterId);
        ISofAUserDTO AddFanZoneAdmin(int theaterId, string userId);
        void RemoveFanZoneAdmin(int theaterId, string faznZoneAdminId);
    }
}
