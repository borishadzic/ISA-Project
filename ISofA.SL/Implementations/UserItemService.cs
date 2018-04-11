using System;
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
        private readonly IUploadService _uploadService;

        public UserItemService(IUnitOfWork unitOfWork, IUploadService uploadService) : base(unitOfWork)
        {
            _uploadService = uploadService;
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

        public UserItemDetailDTO GetItem(int theaterId, Guid userItemId)
        {
            var userItem = UnitOfWork.UserItems
                .Find(x => x.TheaterId == theaterId && x.UserItemId == userItemId)
                .FirstOrDefault();

            if (userItem == null)
            {
                return null;
            }

            return new UserItemDetailDTO(userItem);
        }

        public IEnumerable<UserItemDTO> GetItemsForTheater(int theaterId)
        {
            return UnitOfWork.UserItems
                .Find(x => x.TheaterId == theaterId && x.Approved == true && DateTime.Compare(x.ExpirationDate, DateTime.Now) > 0)
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
                UnitOfWork.SaveChanges();
            }
        }

        public UserItemDTO ApproveItem(int theaterId, Guid userItemId)
        {
            var item = UnitOfWork.UserItems
                .Find(x => x.TheaterId == theaterId && x.UserItemId == userItemId)
                .FirstOrDefault();

            if (item == null)
            {
                return null;
            }

            item.Approved = true;
            UnitOfWork.SaveChanges();

            return new UserItemDTO(item);
        }

        public async Task<UserItemDTO> SetImageAsync(string userId, int theaterId, Guid userItemId, HttpPostedFile file)
        {
            if (file == null || !file.ContentType.Contains("image"))
            {
                return null;
            }

            var userItem = UnitOfWork.UserItems
                .Find(x => x.TheaterId == theaterId &&  x.UserItemId == userItemId && x.ISofAUserId == userId && x.Approved == null)
                .FirstOrDefault();

            if (userItem == null)
            {
                return null;
            }

            userItem.ImageUrl = await _uploadService.UploadImageAsync(file);
            UnitOfWork.SaveChanges();

            return new UserItemDTO(userItem);
        }
    }
}
