using ISofA.DAL.Core.Domain;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISofA.DAL.Persistence
{
    public class ISofADbContext : IdentityDbContext<ISofAUser>
    {
        public ISofADbContext()
            : base("ISofADb", throwIfV1Schema: false)
        {
        }

        public static ISofADbContext Create()
        {
            return new ISofADbContext();
        }
    }
}
