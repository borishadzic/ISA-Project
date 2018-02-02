using ISofA.DAL.Core;
using ISofA.DAL.Core.Domain;
using ISofA.DAL.Core.Pantries;
using System.Collections.Generic;
using System.Linq;

namespace ISofA.DAL.Persistence.Pantries
{
    public class UserPantry : IUserPantry
    {
        private IISofADbContext _context;

        public UserPantry(IISofADbContext context)
        {
            _context = context;
        }

        public IEnumerable<ISofAUser> Get()
        {
            return _context.Users.ToList();

        }

        public ISofAUser Get(string id)
        {
            return _context.Users.Where(x => x.Id.Equals(id)).FirstOrDefault();
        }
    }
}
