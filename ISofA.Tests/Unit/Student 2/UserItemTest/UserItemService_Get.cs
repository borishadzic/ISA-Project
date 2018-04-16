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
    public class UserItemService_Get
    {
        private TestUnitOfWork _unitOfWork;
        private UserItemService _userItemService;

        public UserItemService_Get()
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
        public void UserItems_Get_All_1_Expired()
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
                Approved = true,
                Sold = false
            };

            var userItem2 = new UserItem
            {
                TheaterId = theater1.TheaterId,
                ISofAUserId = user1.Id,
                ExpirationDate = DateTime.Now.AddHours(-2),
                Approved = true,
                Sold = false
            };

            _unitOfWork.UserItems.AddRange(new UserItem[] { userItem1, userItem2 });
            _unitOfWork.SaveChanges();

            // Act
            var userItems = _userItemService.GetItemsForTheater(theater1.TheaterId);

            // Assert
            Assert.AreEqual(1, userItems.Count());
        }

        [TestMethod]
        public void UserItems_Get_All_2_Sold()
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
                Approved = true,
                Sold = false
            };

            var userItem2 = new UserItem
            {
                TheaterId = theater1.TheaterId,
                ISofAUserId = user1.Id,
                ExpirationDate = DateTime.Now.AddHours(2),
                Approved = true,
                Sold = false
            };

            var userItem3 = new UserItem
            {
                TheaterId = theater1.TheaterId,
                ISofAUserId = user1.Id,
                ExpirationDate = DateTime.Now.AddHours(2),
                Approved = true,
                Sold = true
            };

            _unitOfWork.UserItems.AddRange(new UserItem[] { userItem1, userItem2, userItem3 });
            _unitOfWork.SaveChanges();

            // Act
            var userItems = _userItemService.GetItemsForTheater(theater1.TheaterId);

            // Assert
            Assert.AreEqual(2, userItems.Count());
        }

        [TestMethod]
        public void UserItems_Get_All_0()
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
                Approved = true,
                Sold = true
            };

            var userItem2 = new UserItem
            {
                TheaterId = theater1.TheaterId,
                ISofAUserId = user1.Id,
                ExpirationDate = DateTime.Now.AddHours(-2),
                Approved = true,
                Sold = false
            };

            var userItem3 = new UserItem
            {
                TheaterId = theater1.TheaterId,
                ISofAUserId = user1.Id,
                ExpirationDate = DateTime.Now.AddHours(-2),
                Approved = false,
                Sold = false
            };

            _unitOfWork.UserItems.AddRange(new UserItem[] { userItem1, userItem2, userItem3 });
            _unitOfWork.SaveChanges();

            // Act
            var userItems = _userItemService.GetItemsForTheater(theater1.TheaterId);

            // Assert
            Assert.AreEqual(0, userItems.Count());
        }

        [TestMethod]
        public void UserItems_Get_All_Awaiting()
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
                Sold = true
            };

            var userItem2 = new UserItem
            {
                TheaterId = theater2.TheaterId,
                ISofAUserId = user1.Id,
                ExpirationDate = DateTime.Now.AddHours(-2),
                Approved = null,
                Sold = false
            };

            var userItem3 = new UserItem
            {
                TheaterId = theater2.TheaterId,
                ISofAUserId = user1.Id,
                ExpirationDate = DateTime.Now.AddHours(-2),
                Approved = null,
                Sold = false
            };

            _unitOfWork.UserItems.AddRange(new UserItem[] { userItem1, userItem2, userItem3 });
            _unitOfWork.SaveChanges();

            // Act
            var userItems1 = _userItemService.GetAwaitingItemsForTheater(theater1.TheaterId);
            var userItems2 = _userItemService.GetAwaitingItemsForTheater(theater2.TheaterId);

            // Assert
            Assert.AreEqual(1, userItems1.Count());
            Assert.AreEqual(2, userItems2.Count());
        }

        [TestMethod]
        public void UserItems_Get_Item_Ok()
        {
            // Arrange
            var theater1 = new Theater { Name = "Arena" };
            _unitOfWork.Theaters.Add(theater1);
            _unitOfWork.SaveChanges();

            var user1 = Utils.GetTestUser(1);
            _unitOfWork.Users.Add(user1);
            _unitOfWork.SaveChanges();

            var userItem1 = new UserItem
            {
                TheaterId = theater1.TheaterId,
                ISofAUserId = user1.Id,
                Name = "Test Name 1",
                ExpirationDate = DateTime.Now.AddHours(2),
                Approved = null,
                Sold = true
            };
            _unitOfWork.UserItems.Add(userItem1);
            _unitOfWork.SaveChanges();

            // Act
            var foundItem = _userItemService.GetItem(theater1.TheaterId, userItem1.UserItemId);

            // Assert
            Assert.IsNotNull(foundItem);
            Assert.AreEqual(userItem1.Name, foundItem.Name);
        }

        [TestMethod]
        public void UserItems_Get_Item_Null()
        {
            // Arrange
            var theater1 = new Theater { Name = "Arena" };
            _unitOfWork.Theaters.Add(theater1);
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
                Sold = true
            };
            _unitOfWork.UserItems.Add(userItem1);
            _unitOfWork.SaveChanges();

            // Act
            var foundItem1 = _userItemService.GetItem(theater1.TheaterId, Guid.NewGuid());
            var foundItem2 = _userItemService.GetItem(2, userItem1.UserItemId);

            // Assert
            Assert.IsNull(foundItem1);
            Assert.IsNull(foundItem2);
        }
    }
}
