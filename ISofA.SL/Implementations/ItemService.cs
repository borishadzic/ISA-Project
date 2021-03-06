﻿using ISofA.DAL.Core;
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
            // and that don't have buyer.
            var filteredItems = UnitOfWork.Items
                .Find(x => x.BuyerId == null)
                .Where(x => items.Any(i => i.ItemId == x.ItemId));

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

        public ItemDTO GetItem(int theaterId, Guid itemId)
        {
            var item = GetTheaterItem(theaterId, itemId);

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

        public void RemoveItem(int theaterId, Guid itemId)
        {
            var item = GetTheaterItem(theaterId, itemId);

            if (item != null)
            {
                UnitOfWork.Items.Remove(item);
                UnitOfWork.SaveChanges();
            }
        }

        public ItemDTO UpdateItem(int theaterId, Guid itemId, Item update)
        {
            var item = GetTheaterItem(theaterId, itemId);

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

        public async Task<ItemDTO> SetImageAsync(int theaterId, Guid itemId, HttpPostedFile image)
        {
            var item = GetTheaterItem(theaterId, itemId);

            if (item == null)
            {
                return null;
            }

            item.ImageUrl = await _uploadService.UploadImageAsync(image);
            UnitOfWork.SaveChanges();

            return new ItemDTO(item);
        }

        private Item GetTheaterItem(int theaterId, Guid itemId)
        {
            var item = UnitOfWork.Items
                .Find(x => x.TheaterId == theaterId && x.ItemId == itemId)
                .FirstOrDefault();

            return item;
        }
    }
}
