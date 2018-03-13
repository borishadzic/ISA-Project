using ISofA.DAL.Core;
using ISofA.DAL.Core.Domain;
using ISofA.SL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISofA.SL.Implementations
{
    public class BidService : Service, IBidService
    {
        public BidService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Bid AddBid(Guid userItemId, string userId, Bid bid)
        {
            var userItem = UnitOfWork.UserItems.Get(userItemId);

            if (userItem == null || userItem.HighestBid < bid.BidAmount || !CheckCondition(userItem))
            {
                return null;
            }

            bid.BidderId = userId;
            bid.UserItemId = userItemId;
            bid.BidDate = DateTime.Now;
            UnitOfWork.Bids.Add(bid);

            userItem.HighestBid = bid.BidAmount;
            UnitOfWork.SaveChanges();

            return bid;
        }

        public Bid Get(Guid userItemId, string userId)
        {
            return UnitOfWork.Bids.Get(new { userItemId, userId });
        }

        public IEnumerable<Bid> GetAll(Guid userItemId)
        {
            return UnitOfWork.Bids.Find(x => x.UserItemId == userItemId);
        }

        private bool CheckCondition(UserItem userItem)
        {
            return userItem.Approved == true
                && userItem.Sold == false
                && userItem.ExpirationDate < DateTime.Now;
        }
    }
}
