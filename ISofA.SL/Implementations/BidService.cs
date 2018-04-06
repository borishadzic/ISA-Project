using ISofA.DAL.Core;
using ISofA.DAL.Core.Domain;
using ISofA.SL.DTO;
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

        public UserItemDetailDTO AddBid(Guid userItemId, string userId, Bid bid)
        {
            var userItem = UnitOfWork.UserItems.Get(userItemId);

            if (userItem == null || !CheckCondition(userItem))
            {
                return null;
            }

            if (userItem.HighestBid != null && userItem.HighestBid + 10 > bid.BidAmount)
            {
                return null;
            }

            bid.BidderId = userId;
            bid.UserItemId = userItemId;
            bid.BidDate = DateTime.Now;
            UnitOfWork.Bids.SaveOrUpdateBid(bid);

            userItem.HighestBid = bid.BidAmount;
            UnitOfWork.SaveChanges();

            return new UserItemDetailDTO(userItem);
        }

        public BidDTO Get(Guid userItemId, string userId)
        {
            var bid = UnitOfWork.Bids.Get(new { userItemId, userId });

            if (bid == null)
            {
                return null;
            }

            return new BidDTO(bid);
        }

        public IEnumerable<BidDTO> GetAll(Guid userItemId)
        {
            return UnitOfWork.Bids.Find(x => x.UserItemId == userItemId).Select(x => new BidDTO(x));
        }

        private bool CheckCondition(UserItem userItem)
        {
            return userItem.Approved == true
                && userItem.Sold == false
                && DateTime.Compare(userItem.ExpirationDate, DateTime.Now) > 0;
        }
    }
}
