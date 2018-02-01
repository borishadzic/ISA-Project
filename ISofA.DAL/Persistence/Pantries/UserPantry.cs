using ISofA.DAL.Core.Domain;
using ISofA.DAL.Core.Pantries;
using System.Collections.Generic;
using System.Linq;

namespace ISofA.DAL.Persistence.Pantries
{
    public class UserPantry : IUserPantry
    {
        public IEnumerable<ISofAUser> Get()
        {
            using(var context = new ISofADbContext())
            {
                return context.Users.ToList();
            }
        }
    }
}
