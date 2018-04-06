using ISofA.DAL.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISofA.Tests.Persistence
{
    public interface ITestUnitOfWork : IUnitOfWork
    {
        void NukeDatabase();
    }
}
