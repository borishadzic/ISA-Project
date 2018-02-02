using ISofA.DAL.Core;
using ISofA.DAL.Core.Domain;
using ISofA.SL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISofA.SL.Implementations
{
    public class AuthService :  IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuthService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool IsFanZoneAdmin(string userId, int theaterId)
        {
            return _unitOfWork.UserPantry.Get(userId)
                .FanZoneTheaters
                .Where(t => t.TheaterId == theaterId).FirstOrDefault() != null;
        }

        public bool IsTheaterAdmin(string userId, int theaterId)
        {
            return _unitOfWork.UserPantry.Get(userId)
                .AdminTheaters
                .Where(t => t.TheaterId == theaterId).First() != null;
        }
    }
}
