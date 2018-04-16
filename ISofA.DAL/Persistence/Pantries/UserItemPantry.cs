using ISofA.DAL.Core.Domain;
using ISofA.DAL.Core.Pantries;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public UserItem GetUserItemWithBids(int theaterId, Guid userItemId)
        {
            return Context.UserItems
                .Where(x => x.TheaterId == theaterId && x.UserItemId == userItemId)
                .Include(x => x.Bids)
                .FirstOrDefault();
        }

        public UserItem GetUserItemWithBids(Guid userItemId)
        {
            return Context.UserItems
                .Where(x => x.UserItemId == userItemId)
                .Include(x => x.Bids)
                .FirstOrDefault();
        }
    }
}
