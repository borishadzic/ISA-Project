using ISofA.DAL.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISofA.Tests.Persistence
{
    public class ISofATestDbContext : ISofADbContext
    {
        public ISofATestDbContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
        }

        public void NukeDatabase()
        {
            Database.Delete();
            Database.Create();
        }
    }
}
