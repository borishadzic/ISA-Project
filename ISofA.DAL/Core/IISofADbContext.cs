using ISofA.DAL.Core.Domain;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISofA.DAL.Core
{
    public interface IISofADbContext : IIdentityDbContext<ISofAUser, IdentityRole, string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>
    {
        DbSet<Theater> Theaters { get; }
    }
}
