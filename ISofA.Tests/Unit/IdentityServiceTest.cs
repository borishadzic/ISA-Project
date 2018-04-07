using ISofA.DAL.Core.Domain;
using ISofA.Tests.Claims;
using ISofA.Tests.DI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace ISofA.Tests.Unit
{
    public class IdentityServiceTest : ServiceTest
    {
        protected ClaimsIdentity _identity;

        protected new void Init()
        {
            base.Init();
            _identity = _diResolver.Resolve<IIdentity>() as ClaimsIdentity;
            _identity.AddISofAUserRoleClaims(ISofAUserRole.SysAdmin, null);
        }
    }
}
