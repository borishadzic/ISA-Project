using ISofA.DAL.Core.Domain;
using ISofA.SL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ISofA.SL.Services
{
    public interface IUserItemService
    {
        IEnumerable<UserItemDTO> GetItemsForTheater(int theaterId);
        IEnumerable<UserItemDTO> GetSoldItems(int theaterId);
        IEnumerable<UserItemDTO> GetAwaitingItemsForTheater(int theaterId);
        UserItemDetailDTO GetItem(Guid userItemId);
        UserItemDTO AddItem(int theaterId, string userId, UserItem userItem);
        Task<UserItemDTO> SetImageAsync(string userId, Guid userItemId, HttpPostedFile file);
        UserItemDTO ApproveItem(int theaterId, Guid userItemId, UserItem userItem);
        UserItemDTO SellItem(string userId, Guid userItemId, Bid bid);
        void RemoveItem(int theaterId, Guid userItemId);
    }
}
