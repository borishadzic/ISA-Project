using ISofA.DAL.Core.Claims;
using ISofA.DAL.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ISofA.Tests.Claims
{
    public static class ClaimsIdentityExtensions
    {
        public static void ClearClaims(this ClaimsIdentity claimsIdentity)
        {
            foreach (var claim in claimsIdentity.Claims)
                claimsIdentity.RemoveClaim(claim);
        }

        public static void AddISofAUserRoleClaims(this ClaimsIdentity claimsIdentity, ISofAUserRole isofaUserRole, int? adminOf)
        {
            claimsIdentity.AddClaim(new Claim(ISofAClaimTypes.ISofAUserRole, ((int)isofaUserRole).ToString()));
            claimsIdentity.AddClaim(new Claim(ISofAClaimTypes.ISofAAdminOf, adminOf.GetValueOrDefault().ToString()));
        }
    }
}
