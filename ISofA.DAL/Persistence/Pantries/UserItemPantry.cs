using ISofA.DAL.Core.Domain;
using ISofA.DAL.Core.Pantries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISofA.DAL.Persistence.Pantries
{
    public class UserItemPantry : Pantry<UserItem>, IUserItemPantry
    {
        public UserItemPantry(ISofADbContext context) : base(context)
        {
        }
    }
}
