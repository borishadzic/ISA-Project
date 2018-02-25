using ISofA.DAL.Core.Domain;
using System.Collections.Generic;

namespace ISofA.SL.Services
{
    public interface IFanZoneAdminService
    {
        IEnumerable<ISofAUser> GetFanZoneAdmins(int theaterId);
        ISofAUser AddFanZoneAdmin(int theaterId, string userId);
        void RemoveFanZoneAdmin(int theaterId, string faznZoneAdminId);
    }
}
