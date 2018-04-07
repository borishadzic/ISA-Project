using ISofA.DAL.Core.Claims;
using ISofA.DAL.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ISofA.SL.Claims
{
    public static class ClaimsIdentityExtensions
    {
        private static ISofAUserRole GetISofAUserRole(ClaimsIdentity claimsIdentity)
        {
            return (ISofAUserRole)int.Parse(claimsIdentity.FindFirst(ISofAClaimTypes.ISofAUserRole).Value);
        }

        private static int GetTheaterId(ClaimsIdentity claimsIdentity)
        {
            return int.Parse(claimsIdentity.FindFirst(ISofAClaimTypes.ISofAAdminOf).Value);
        }        

        public static bool IsAuthorizedTheaterAdmin(this ClaimsIdentity claimsIdentity, int theaterId)
        {
            var role = GetISofAUserRole(claimsIdentity);
            var adminOf = GetTheaterId(claimsIdentity);

            switch (role)
            {
                case ISofAUserRole.User:
                    return false;
                case ISofAUserRole.FanZoneAdmin:
                    return false;
                case ISofAUserRole.TheaterAdmin:
                    if (adminOf == theaterId)
                        return true;
                    return false;
                case ISofAUserRole.SysAdmin:
                    return (adminOf == 0) ? true : adminOf == theaterId;
                default:
                    throw new Exception();
            }
        }
    }
}
