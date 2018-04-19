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
        private readonly IEmailService _emailService;

        public BidService(IUnitOfWork unitOfWork, IEmailService emailService) : base(unitOfWork)
        {
            _emailService = emailService;
        }

        public UserItemDetailDTO AddBid(Guid userItemId, string userId, Bid bid)
        {
            var userItem = UnitOfWork.UserItems.GetUserItemWithBids (userItemId);

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
            var bid = UnitOfWork.Bids.Get(userItemId, userId);

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

        public async Task<UserItemDTO> SellItemAsync(string userItemOwnerId, Guid userItemId, string bidderId)
        {
            var userItem = UnitOfWork.UserItems.GetUserItemWithBids(userItemId, userItemOwnerId);
            var bid = userItem?.Bids.FirstOrDefault(x => x.BidderId == bidderId);

            if (userItem == null || bid == null || !CheckCondition(userItem))
            {
                return null;
            }

            userItem.Sold = true;
            userItem.HighestBid = bid.BidAmount;
            UnitOfWork.SaveChanges();

            await _emailService.UserItemSoldNotification(userItem, bid);

            return new UserItemDTO(userItem);
        }

        private bool CheckCondition(UserItem userItem)
        {
            return userItem.Approved == true
                && userItem.Sold == false
                && DateTime.Compare(userItem.ExpirationDate, DateTime.Now) > 0;
        }
    }
}
