using ISofA.DAL.Core;
using ISofA.DAL.Core.Domain;
using ISofA.DAL.Core.Pantries;
using System.Collections.Generic;
using System.Linq;

namespace ISofA.DAL.Persistence.Pantries
{
    public class UserPantry : Pantry<ISofAUser>, IUserPantry
    {
        public UserPantry(ISofADbContext context) : base(context)
        {
        }
    }
}
