using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISofA.DAL.Core;
using ISofA.DAL.Core.Domain;
using ISofA.SL.DTO;
using ISofA.SL.Services;

namespace ISofA.SL.Implementations
{
    public class FanZoneAdminService : Service, IFanZoneAdminService
    {
        public FanZoneAdminService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public ISofAUserDTO AddFanZoneAdmin(int theaterId, string userId)
        {
            var theater = UnitOfWork.Theaters.GetTheaterWithFanZoneAdmins(theaterId);
            var user = UnitOfWork.Users.Get(userId);

            if (theater != null && user != null && !theater.FanZoneAdmins.Contains(user))
            {
                theater.FanZoneAdmins.Add(user);
                UnitOfWork.SaveChanges();
                return new ISofAUserDTO(user);
            }

            return null;
        }

        public IEnumerable<ISofAUserDTO> GetFanZoneAdmins(int theaterId)
        {
            return UnitOfWork.Theaters.GetTheaterFanZoneAdmins(theaterId).Select(x => new ISofAUserDTO(x));
        }

        public void RemoveFanZoneAdmin(int theaterId, string faznZoneAdminId)
        {
            var theater = UnitOfWork.Theaters.GetTheaterWithFanZoneAdmins(theaterId);

            var fanZoneModerator = theater.FanZoneAdmins
                .Where(x => x.Id == faznZoneAdminId)
                .FirstOrDefault();
            
            if (theater != null && fanZoneModerator != null)
            {
                theater.FanZoneAdmins.Remove(fanZoneModerator);
                UnitOfWork.SaveChanges();
            }
        }
    }
}
