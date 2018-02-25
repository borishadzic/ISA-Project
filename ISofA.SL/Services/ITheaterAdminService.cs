using ISofA.DAL.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISofA.SL.Services
{
    public interface ITheaterAdminService
    {
        IEnumerable<ISofAUser> GetTheaterAdmins(int theaterId);
        ISofAUser AddTheaterAdmin(int theaterId, string userId);
        void RemoveTheaterAdmin(int theaterId, string theaterAdminId);
    }
}
