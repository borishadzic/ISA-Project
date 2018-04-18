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

        public UserItem GetUserItemWithBids(Guid userItemId)
        {
            return Context.UserItems
                .Where(x => x.UserItemId == userItemId)
                .Include(x => x.Bids)
                .FirstOrDefault();
        }

        public UserItem GetUserItemWithBids(Guid userItemId, int theaterId)
        {
            return Context.UserItems
                .Where(x => x.UserItemId == userItemId && x.TheaterId == theaterId)
                .Include(x => x.Bids)
                .FirstOrDefault();
        }

        public UserItem GetUserItemWithBids(Guid userItemId, string ISofAUserId)
        {
            return Context.UserItems
                .Where(x => x.UserItemId == userItemId && x.ISofAUserId == ISofAUserId)
                .Include(x => x.Bids.Select(b => b.Bidder))
                .FirstOrDefault();
        }
    }
}
