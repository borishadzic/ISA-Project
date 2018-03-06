using ISofA.DAL.Core.Domain;
using ISofA.SL.DTO;
using System;
using System.Collections.Generic;

namespace ISofA.SL.Services
{
    public interface IItemService
    {
        IEnumerable<ItemDTO> GetItemsForTheater(int theaterId);
        IEnumerable<ItemDTO> GetBoughtItemsForTheater(int theaterId);
        ItemDTO GetItem(Guid itemId);
        ItemDTO AddItem(int theaterId, Item item);
        ItemDTO UpdateItem(Guid itemId, Item update);
        bool BuyItems(IEnumerable<Item> items, string userId);
        void RemoveItem(Guid itemId);
    }
}
