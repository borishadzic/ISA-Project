using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISofA.DAL.Core
{
    public interface IDbContext
    {
        int SaveChanges();
    }
}
