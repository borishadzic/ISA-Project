using ISofA.DAL.Core.Domain;
using ISofA.DAL.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISofA.DAL.Persistence.Repositories
{
    public class UserRepository : IUserRepository
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
