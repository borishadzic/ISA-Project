using ISofA.DAL.Core.Domain;
using ISofA.DAL.Core.Pantries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;

namespace ISofA.DAL.Persistence.Pantries
{
    public class BidPantry : Pantry<Bid>, IBidPantry
    {
        public BidPantry(ISofADbContext context) : base(context)
        {
        }

        public Bid GetBidWithUserItem(Guid userItemId, string bidderId)
        {
            return Context.Bids
                .Where(x => x.UserItemId == userItemId && x.BidderId == bidderId)
                .Include(x => x.UserItem)
                .FirstOrDefault();
        }

        public void SaveOrUpdateBid(Bid bid)
        {
            var foundBid = Context.Set<Bid>().FirstOrDefault(x => x.UserItemId == bid.UserItemId && x.BidderId == bid.BidderId);

            if (foundBid != null)
            {
                foundBid.BidAmount = bid.BidAmount;
                foundBid.BidDate = bid.BidDate;
            }
            else
            {
                Context.Entry(bid).State = EntityState.Added;
            }
        }
    }
}
