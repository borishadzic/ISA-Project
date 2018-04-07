using ISofA.DAL.Core;
using ISofA.SL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ISofA.SL.Implementations
{
    public abstract class IdentityService : Service
    {
        public ClaimsIdentity _identity { get; set; }

        public IdentityService(IUnitOfWork unitOfWork, IIdentity identity) : base(unitOfWork)
        {
            _identity = identity as ClaimsIdentity;
        }
    }
}
