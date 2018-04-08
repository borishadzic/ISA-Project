using System.Collections.Generic;
using System.Linq;
using ISofA.DAL.Core;
using ISofA.DAL.Core.Domain;
using ISofA.SL.DTO;
using ISofA.SL.Services;

namespace ISofA.SL.Implementations
{
    public class AdminService : Service, IAdminService
    {
        public AdminService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public ISofAUserDTO AddFanZoneAdmin(int theaterId, ISofAUser user)
        {
            return AddAdmin(theaterId, user, ISofAUserRole.FanZoneAdmin);
        }

        public ISofAUserDTO AddTheaterAdmin(int theaterId, ISofAUser user)
        {
            return AddAdmin(theaterId, user, ISofAUserRole.TheaterAdmin);
        }

        private ISofAUserDTO AddAdmin(int theaterId, ISofAUser user, ISofAUserRole role)
        {
            var theater = UnitOfWork.Theaters.Get(theaterId);

            if (theater != null)
            {
                user.ISofAUserRole = role;
                theater.Admins.Add(user);
                UnitOfWork.Users.UpdateUser(user);
                UnitOfWork.SaveChanges();
                return new ISofAUserDTO(user);
            }

            return null;
        }

        public IEnumerable<ISofAUserDTO> GetAdmins(int theaterId)
        {
            return UnitOfWork.Users.Find(x => x.AdminOfTheaterId == theaterId).Select(x => new ISofAUserDTO(x));
        }

        public IEnumerable<ISofAUserDTO> GetFanZoneAdmins(int theaterId)
        {
            return GetAdmins(ISofAUserRole.FanZoneAdmin, theaterId);
        }

        public IEnumerable<ISofAUserDTO> GetTheaterAdmins(int theaterId)
        {
            return GetAdmins(ISofAUserRole.TheaterAdmin, theaterId);
        }

        private IEnumerable<ISofAUserDTO> GetAdmins(ISofAUserRole role, int theaterId)
        {
            return UnitOfWork.Users
                .Find(x => x.ISofAUserRole == role && x.AdminOfTheaterId == theaterId)
                .Select(x => new ISofAUserDTO(x));
        }

        public void RemoveAdmin(int theaterId, string theaterAdminId)
        {
            var theater = UnitOfWork.Theaters.GetTheaterWithAdmins(theaterId);

            if (theater != null)
            {
                var theaterAdmin = theater.Admins.Where(x => x.Id == theaterAdminId).FirstOrDefault();

                if (theaterAdmin != null)
                {
                    theater.Admins.Remove(theaterAdmin);
                    UnitOfWork.SaveChanges();
                }
                else
                {
                    // TODO: throw bad request admin not found. Admin not found
                }
            }
            else
            {
                // TODO: throw Thater not found exception
            }
        }
    }
}
