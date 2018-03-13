﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using ISofA.DAL.Core;
using ISofA.DAL.Core.Domain;
using ISofA.SL.DTO;
using ISofA.SL.Services;

namespace ISofA.SL.Implementations
{
    public class UserItemService : Service, IUserItemService
    {
        public UserItemService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public UserItemDTO AddItem(int theaterId, string userId, UserItem userItem)
        {
            var theater = UnitOfWork.Theaters.Get(theaterId);

            if (theater == null)
            {
                return null;
            }

            userItem.ISofAUserId = userId;
            userItem.TheaterId = theaterId;
            UnitOfWork.UserItems.Add(userItem);
            UnitOfWork.SaveChanges();

            return new UserItemDTO(userItem);
        }

        public IEnumerable<UserItemDTO> GetAwaitingItemsForTheater(int theaterId)
        {
            return UnitOfWork.UserItems
                .Find(x => x.TheaterId == theaterId && x.Approved == null)
                .Select(x => new UserItemDTO(x));
        }

        public UserItemDTO GetItem(Guid userItemId)
        {
            var userItem = UnitOfWork.UserItems.Get(userItemId);

            if (userItem == null)
            {
                return null;
            }

            return new UserItemDTO(userItem);
        }

        public IEnumerable<UserItemDTO> GetItemsForTheater(int theaterId)
        {
            return UnitOfWork.UserItems
                .Find(x => x.TheaterId == theaterId && x.Approved == true)
                .Select(x => new UserItemDTO(x));
        }

        public IEnumerable<UserItemDTO> GetSoldItems(int theaterId)
        {
            return UnitOfWork.UserItems
                .Find(x => x.TheaterId == theaterId && x.Sold == true)
                .Select(x => new UserItemDTO(x));
        }

        public void RemoveItem(int theaterId, Guid userItemId)
        {
            var userItem = UnitOfWork.UserItems
                .Find(x => x.TheaterId == theaterId && x.UserItemId == userItemId)
                .FirstOrDefault();

            if (userItem != null)
            {
                UnitOfWork.UserItems.Remove(userItem);
            }
        }

        public UserItemDTO ApproveItem(int theaterId, Guid userItemId, UserItem userItem)
        {
            var item = UnitOfWork.UserItems
                .Find(x => x.TheaterId == theaterId && x.UserItemId == userItemId)
                .FirstOrDefault();

            if (item == null)
            {
                return null;
            }

            item.Approved = userItem.Approved;
            UnitOfWork.SaveChanges();

            return new UserItemDTO(item);
        }

        public UserItemDTO SetImage(string userId, Guid userItemId, HttpPostedFile file)
        {
            if (file == null || !file.ContentType.Contains("image"))
            {
                return null;
            }

            var userItem = UnitOfWork.UserItems
                .Find(x => x.UserItemId == userItemId && x.ISofAUserId == userId && x.Approved == null)
                .FirstOrDefault();

            if (userItem == null)
            {
                return null;
            }

            // TODO: Uploadovanje slike
            // userItem.ImageUrl = ...
            // UnitOfWork.SaveChanges();

            return new UserItemDTO(userItem);
        }

        public UserItemDTO SellItem(string userId, Guid userItemId, Bid bid)
        {
            var userItem = UnitOfWork.UserItems
                .Find(x => x.UserItemId == userItemId && x.ISofAUserId == userId)
                .FirstOrDefault();

            if (userItem == null || !CheckCondition(userItem) || !userItem.Bids.Any(x => x.BidderId == bid.BidderId))
            {
                return null;
            }

            userItem.Sold = true;
            userItem.HighestBid = bid.BidAmount;
            UnitOfWork.SaveChanges();

            SendMail(userItem, bid);

            return new UserItemDTO(userItem);
        }

        private void SendMail(UserItem userItem, Bid bid)
        {
            IEnumerable<ISofAUser> losers = userItem.Bids
                .Where(x => x.BidderId != bid.BidderId)
                .Select(x => x.Bidder);

            ISofAUser winner = userItem.Bids
                .Where(x => x.BidderId == bid.BidderId)
                .Select(x => x.Bidder)
                .First();
        }

        private bool CheckCondition(UserItem userItem)
        {
            return userItem.Approved == true
                && userItem.Sold == false
                && userItem.ExpirationDate < DateTime.Now;
        }
    }
}
