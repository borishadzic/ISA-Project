using ISofA.DAL.Core.Pantries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISofA.DAL.Core
{
    public interface IUnitOfWork
    {
        IUserPantry UserPantry { get; }

        int SaveChanges();

    }
}
