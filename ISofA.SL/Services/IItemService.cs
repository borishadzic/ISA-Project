using ISofA.DAL.Core.Domain;
using ISofA.SL.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;

namespace ISofA.SL.Services
{
    public interface IItemService
    {
        ItemDTO GetItem(int theaterId, Guid itemId);
        IEnumerable<ItemDTO> GetItemsForTheater(int theaterId);
        IEnumerable<ItemDTO> GetBoughtItemsForTheater(int theaterId);
        ItemDTO AddItem(int theaterId, Item item);
        Task<ItemDTO> SetImageAsync(int theaterId, Guid itemId, HttpPostedFile image);
        ItemDTO UpdateItem(int theaterId, Guid itemId, Item update);
        void RemoveItem(int theaterId, Guid itemId);
        bool BuyItems(IEnumerable<Item> items, string userId);
    }
}
