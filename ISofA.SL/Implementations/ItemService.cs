using ISofA.DAL.Core;
using ISofA.DAL.Core.Domain;
using ISofA.SL.DTO;
using ISofA.SL.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ISofA.SL.Implementations
{
    public class ItemService : Service, IItemService
    {
        // TODO: Vrati samo one iteme koji nisu prodati
        public ItemService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public ItemDTO AddItem(int theaterId, Item item)
        {
            item.TheaterId = theaterId;
            UnitOfWork.Items.Add(item);
            UnitOfWork.SaveChanges();
            return new ItemDTO(item);
        }

        public bool BuyItem(Guid itemId, string userId)
        {
            var item = UnitOfWork.Items.Get(itemId);

            if (item.BuyerId != null)
            {
                return false;
            }

            item.BuyerId = userId;
            item.BoughtDate = DateTime.Now;
            UnitOfWork.SaveChanges();

            return true;
        }

        public ItemDTO GetItem(Guid itemId)
        {
            var item = UnitOfWork.Items.Get(itemId);

            if (item == null)
            {
                return null;
            }

            return new ItemDTO(item);
        }

        public IEnumerable<ItemDTO> GetItemsForTheater(int theaterId)
        {
            return UnitOfWork.Items.Find(i => i.TheaterId == theaterId && i.BuyerId == null).Select(i => new ItemDTO(i));
        }

        public IEnumerable<ItemDTO> GetBoughtItemsForTheater(int theaterId)
        {
            return UnitOfWork.Items.Find(i => i.TheaterId == theaterId && i.BuyerId != null).Select(i => new ItemDTO(i));
        }

        public void RemoveItem(Guid itemId)
        {
            var item = UnitOfWork.Items.Get(itemId);

            if (item != null)
            {
                UnitOfWork.Items.Remove(item);
                UnitOfWork.SaveChanges();
            }
        }

        public ItemDTO UpdateItem(Guid itemId, Item update)
        {
            var item = UnitOfWork.Items.Get(itemId);

            if (item != null)
            {
                item.Name = update.Name;
                item.Description = update.Description;

                return new ItemDTO(item);
            }

            return null;
        }
    }
}
