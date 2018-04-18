using ISofA.DAL.Core.Domain;
using ISofA.SL.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ISofA.SL.Services
{
    public interface IBidService
    {
        IEnumerable<BidDTO> GetAll(Guid userItemId);
        BidDTO Get(Guid userItemId, string userId);
        UserItemDetailDTO AddBid(Guid userItemId, string userId, Bid bid);
        Task<UserItemDTO> SellItemAsync(string userItemOwnerId, Guid userItemId, string bidderId);
    }
}
