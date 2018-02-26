using System.Collections.Generic;
using System.Linq;
using ISofA.DAL.Core;
using ISofA.DAL.Core.Domain;
using ISofA.SL.DTO;
using ISofA.SL.Services;

namespace ISofA.SL.Implementations
{
    public class TheaterAdminService : Service, ITheaterAdminService
    {
        public TheaterAdminService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public ISofAUserDTO AddTheaterAdmin(int theaterId, string userId)
        {
            var user = UnitOfWork.Users.Get(userId);
            var theater = UnitOfWork.Theaters.Get(theaterId);

            if (theater != null && user != null && !theater.TheaterAdmins.Contains(user))
            {
                theater.TheaterAdmins.Add(user);
                UnitOfWork.SaveChanges();
                return new ISofAUserDTO(user);
            }

            return null;
        }

        public IEnumerable<ISofAUserDTO> GetTheaterAdmins(int theaterId)
        {
            return UnitOfWork.Theaters.GetTheaterAdmins(theaterId).Select(x => new ISofAUserDTO(x));
        }

        public void RemoveTheaterAdmin(int theaterId, string theaterAdminId)
        {
            var theater = UnitOfWork.Theaters.GetTheaterWithAdmins(theaterId);
            var theaterAdmin = theater.TheaterAdmins.Where(x => x.Id == theaterAdminId).FirstOrDefault();

            if (theater != null && theaterAdmin != null)
            {
                theater.TheaterAdmins.Remove(theaterAdmin);
                UnitOfWork.SaveChanges();
            }
        }
    }
}
