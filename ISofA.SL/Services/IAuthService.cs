using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISofA.SL.Services
{
    public interface IAuthService
    {
        bool IsFanZoneAdmin(string userId, int theaterId);
        bool IsTheaterAdmin(string userId, int theaterId);
    }
}
