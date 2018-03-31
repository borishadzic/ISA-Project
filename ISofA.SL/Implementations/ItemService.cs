using ISofA.DAL.Core;
using ISofA.DAL.Core.Domain;
using ISofA.SL.DTO;
using ISofA.SL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ISofA.SL.Implementations
{
    public class ItemService : Service, IItemService
    {
        private readonly IUploadService _uploadService;

        // TODO: Vrati samo one iteme koji nisu prodati
        public ItemService(IUnitOfWork unitOfWork, IUploadService uploadService) : base(unitOfWork)
        {
            _uploadService = uploadService;
        }

        public ItemDTO AddItem(int theaterId, Item item)
        {
            item.TheaterId = theaterId;
            UnitOfWork.Items.Add(item);
            UnitOfWork.SaveChanges();
            return new ItemDTO(item);
        }

        public bool BuyItems(IEnumerable<Item> items, string userId)
        {
            if (items == null || items.Count() == 0)
            {
                return false;
            }

            // find items from db that are contained in sent item list
            // and don't haven't been bought yet.
            var filteredItems = UnitOfWork.Items.GetAll()
                .Where(x => x.BuyerId == null && items.Any(i => i.ItemId == x.ItemId));

            // if sizes don't match that means user is trying to buy
            // something that doesn't exist or has been bought previously
            if (items.Count() != filteredItems.Count())
            {
                return false;
            }

            // Update item
            DateTime boughtDate = DateTime.Now;
            foreach (var item in filteredItems)
            {
                item.BuyerId = userId;
                item.BoughtDate = boughtDate;
            }

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
            return UnitOfWork.Items
                .Find(i => i.TheaterId == theaterId && i.BuyerId == null)
                .Select(i => new ItemDTO(i));
        }

        public IEnumerable<ItemDTO> GetBoughtItemsForTheater(int theaterId)
        {
            return UnitOfWork.Items
                .Find(i => i.TheaterId == theaterId && i.BuyerId != null)
                .Select(i => new ItemDTO(i));
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

            if (item == null)
            {
                return null;
            }

            item.Name = update.Name;
            item.Description = update.Description;
            item.Price = update.Price;
            UnitOfWork.SaveChanges();

            return new ItemDTO(item);
        }

        public async Task<ItemDTO> SetImageAsync(Guid itemId, HttpPostedFile image)
        {
            var item = UnitOfWork.Items.Get(itemId);

            if (item == null)
            {
                return null;
            }

            item.ImageUrl = await _uploadService.UploadImageAsync(image);

            return new ItemDTO(item);
        }
    }
}
