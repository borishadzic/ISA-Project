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
        IEnumerable<UserItemDTO> GetSoldItems(int theaterId);
        IEnumerable<UserItemDTO> GetItemsForTheater(int theaterId);
        IEnumerable<UserItemDTO> GetUserItems(string userId);
        IEnumerable<UserItemDTO> GetAwaitingItemsForTheater(int theaterId);
        UserItemDetailDTO GetItem(int theaterId, Guid userItemId);
        UserItemDTO AddItem(int theaterId, string userId, UserItem userItem);
        Task<UserItemDTO> SetImageAsync(string userId, int theaterId, Guid userItemId, HttpPostedFile file);
        UserItemDTO ApproveItem(int theaterId, Guid userItemId);
        void RemoveItem(int theaterId, Guid userItemId);
    }
}
