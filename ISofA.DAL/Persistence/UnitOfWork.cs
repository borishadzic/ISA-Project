using ISofA.DAL.Core;
using ISofA.DAL.Core.Domain;
using ISofA.DAL.Core.Pantries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISofA.DAL.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private IISofADbContext _context;

        public IUserPantry UserPantry { get; }

        public UnitOfWork(IISofADbContext context, IUserPantry userPantry)
        {
            _context = context;
            UserPantry = userPantry;
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
