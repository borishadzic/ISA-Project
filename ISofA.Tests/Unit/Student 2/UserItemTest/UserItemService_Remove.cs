using ISofA.DAL.Core.Domain;
using ISofA.SL.Implementations;
using ISofA.Tests.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISofA.Tests.Unit.Student_2.UserItemTest
{
    [TestClass]
    public class UserItemService_Remove
    {
        private TestUnitOfWork _unitOfWork;
        private UserItemService _userItemService;

        public UserItemService_Remove()
        {
            _unitOfWork = new TestUnitOfWork();
            _userItemService = new UserItemService(_unitOfWork, null);
        }

        [TestInitialize]
        public void Initialize()
        {
            _unitOfWork.NukeDatabase();
        }

        [TestMethod]
        public void UserItems_Remove_1_Ok()
        {
            // Arrange
            var theater1 = new Theater { Name = "Arena" };
            var theater2 = new Theater { Name = "SNP" };
            _unitOfWork.Theaters.AddRange(new Theater[] { theater1, theater2 });
            _unitOfWork.SaveChanges();

            var user1 = Utils.GetTestUser(1);
            _unitOfWork.Users.Add(user1);
            _unitOfWork.SaveChanges();

            var userItem1 = new UserItem
            {
                TheaterId = theater1.TheaterId,
                ISofAUserId = user1.Id,
                ExpirationDate = DateTime.Now.AddHours(2),
                Approved = null,
                Sold = false
            };

            var userItem2 = new UserItem
            {
                TheaterId = theater1.TheaterId,
                ISofAUserId = user1.Id,
                ExpirationDate = DateTime.Now.AddHours(-2),
                Approved = null,
                Sold = false
            };

            _unitOfWork.UserItems.AddRange(new UserItem[] { userItem1, userItem2 });
            _unitOfWork.SaveChanges();

            // Act
            _userItemService.RemoveItem(theater1.TheaterId, userItem1.UserItemId);

            // Assert
            var foundItem = _unitOfWork.UserItems.Find(x => x.UserItemId == userItem1.UserItemId).FirstOrDefault();

            Assert.IsNull(foundItem);
        }

        [TestMethod]
        public void UserItems_Remove_1_Null()
        {
            // Arrange
            var theater1 = new Theater { Name = "Arena" };
            var theater2 = new Theater { Name = "SNP" };
            _unitOfWork.Theaters.AddRange(new Theater[] { theater1, theater2 });
            _unitOfWork.SaveChanges();

            var user1 = Utils.GetTestUser(1);
            _unitOfWork.Users.Add(user1);
            _unitOfWork.SaveChanges();

            var userItem1 = new UserItem
            {
                TheaterId = theater1.TheaterId,
                ISofAUserId = user1.Id,
                ExpirationDate = DateTime.Now.AddHours(2),
                Approved = false,
                Sold = false
            };

            var userItem2 = new UserItem
            {
                TheaterId = theater1.TheaterId,
                ISofAUserId = user1.Id,
                ExpirationDate = DateTime.Now.AddHours(-2),
                Approved = false,
                Sold = false
            };

            _unitOfWork.UserItems.AddRange(new UserItem[] { userItem1, userItem2 });
            _unitOfWork.SaveChanges();

            // Act
            _userItemService.RemoveItem(theater1.TheaterId, Guid.NewGuid());

            // Assert
            var items = _unitOfWork.UserItems.GetAll();

            Assert.AreEqual(2, items.Count());
        }
    }
}
