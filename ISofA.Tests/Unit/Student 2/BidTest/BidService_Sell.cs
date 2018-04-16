using ISofA.DAL.Core.Domain;
using ISofA.SL.Implementations;
using ISofA.Tests.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISofA.Tests.Unit.Student_2.BidTest
{
    [TestClass]
    public class BidService_Sell
    {
        private TestUnitOfWork _unitOfWork;
        private BidService _bidService;

        public BidService_Sell()
        {
            _unitOfWork = new TestUnitOfWork();
            _bidService = new BidService(_unitOfWork);
        }

        [TestInitialize]
        public void Initialize()
        {
            _unitOfWork.NukeDatabase();
        }

        [TestMethod]
        public void UserItem_Sell_Item_Ok()
        {
            // Arrange
            var theater = new Theater { Name = "Arena" };
            _unitOfWork.Theaters.Add(theater);
            _unitOfWork.SaveChanges();

            var user1 = Utils.GetTestUser(1);
            var user2 = Utils.GetTestUser(2);
            var user3 = Utils.GetTestUser(3);
            _unitOfWork.Users.AddRange(new[] { user1, user2, user3 });
            _unitOfWork.SaveChanges();

            var userItem = new UserItem
            {
                TheaterId = theater.TheaterId,
                ISofAUserId = user1.Id,
                ExpirationDate = DateTime.Now.AddHours(2),
                Approved = true,
                Sold = false
            };
            _unitOfWork.UserItems.Add(userItem);
            _unitOfWork.SaveChanges();

            var bid1 = new Bid { Bidder = user2, UserItem = userItem, BidDate = DateTime.Now, BidAmount = 50 };
            var bid2 = new Bid { Bidder = user3, UserItem = userItem, BidDate = DateTime.Now, BidAmount = 60 };
            _unitOfWork.Bids.AddRange(new[] { bid1, bid2 });
            _unitOfWork.SaveChanges();

            // Act
            var soldItem = _bidService.SellItem(user1.Id, userItem.UserItemId, bid2.BidderId);

            // Assert
            var foundItem = _unitOfWork.UserItems.Get(userItem.UserItemId);

            Assert.IsNotNull(soldItem);
            Assert.IsTrue(foundItem.Sold);
        }

        [TestMethod]
        public void UserItem_Sell_Item_Expired()
        {
            // Arrange
            var theater = new Theater { Name = "Arena" };
            _unitOfWork.Theaters.Add(theater);
            _unitOfWork.SaveChanges();

            var user1 = Utils.GetTestUser(1);
            var user2 = Utils.GetTestUser(2);
            var user3 = Utils.GetTestUser(3);
            _unitOfWork.Users.AddRange(new[] { user1, user2, user3 });
            _unitOfWork.SaveChanges();

            var userItem = new UserItem
            {
                TheaterId = theater.TheaterId,
                ISofAUserId = user1.Id,
                ExpirationDate = DateTime.Now.AddHours(-2),
                Approved = true,
                Sold = false
            };
            _unitOfWork.UserItems.Add(userItem);
            _unitOfWork.SaveChanges();

            var bid1 = new Bid { Bidder = user2, UserItem = userItem, BidDate = DateTime.Now, BidAmount = 50 };
            var bid2 = new Bid { Bidder = user3, UserItem = userItem, BidDate = DateTime.Now, BidAmount = 60 };
            _unitOfWork.Bids.AddRange(new[] { bid1, bid2 });
            _unitOfWork.SaveChanges();

            // Act
            var soldItem = _bidService.SellItem(user1.Id, userItem.UserItemId, bid2.BidderId);

            // Assert
            var foundItem = _unitOfWork.UserItems.Get(userItem.UserItemId);

            Assert.IsNull(soldItem);
            Assert.IsFalse(foundItem.Sold);
        }

        [TestMethod]
        public void UserItem_Sell_Item_Sold()
        {
            // Arrange
            var theater = new Theater { Name = "Arena" };
            _unitOfWork.Theaters.Add(theater);
            _unitOfWork.SaveChanges();

            var user1 = Utils.GetTestUser(1);
            var user2 = Utils.GetTestUser(2);
            var user3 = Utils.GetTestUser(3);
            _unitOfWork.Users.AddRange(new[] { user1, user2, user3 });
            _unitOfWork.SaveChanges();

            var userItem = new UserItem
            {
                TheaterId = theater.TheaterId,
                ISofAUserId = user1.Id,
                ExpirationDate = DateTime.Now.AddHours(2),
                Approved = true,
                Sold = true
            };
            _unitOfWork.UserItems.Add(userItem);
            _unitOfWork.SaveChanges();

            var bid1 = new Bid { Bidder = user2, UserItem = userItem, BidDate = DateTime.Now, BidAmount = 50 };
            var bid2 = new Bid { Bidder = user3, UserItem = userItem, BidDate = DateTime.Now, BidAmount = 60 };
            _unitOfWork.Bids.AddRange(new[] { bid1, bid2 });
            _unitOfWork.SaveChanges();

            // Act
            var soldItem = _bidService.SellItem(user1.Id, userItem.UserItemId, bid2.BidderId);

            // Assert
            var foundItem = _unitOfWork.UserItems.Get(userItem.UserItemId);

            Assert.IsNull(soldItem);
        }

        [TestMethod]
        public void UserItem_Sell_Item_Not_Approved()
        {
            // Arrange
            var theater = new Theater { Name = "Arena" };
            _unitOfWork.Theaters.Add(theater);
            _unitOfWork.SaveChanges();

            var user1 = Utils.GetTestUser(1);
            var user2 = Utils.GetTestUser(2);
            var user3 = Utils.GetTestUser(3);
            _unitOfWork.Users.AddRange(new[] { user1, user2, user3 });
            _unitOfWork.SaveChanges();

            var userItem = new UserItem
            {
                TheaterId = theater.TheaterId,
                ISofAUserId = user1.Id,
                ExpirationDate = DateTime.Now.AddHours(2),
                Approved = null,
                Sold = false
            };
            _unitOfWork.UserItems.Add(userItem);
            _unitOfWork.SaveChanges();

            var bid1 = new Bid { Bidder = user2, UserItem = userItem, BidDate = DateTime.Now, BidAmount = 50 };
            var bid2 = new Bid { Bidder = user3, UserItem = userItem, BidDate = DateTime.Now, BidAmount = 60 };
            _unitOfWork.Bids.AddRange(new[] { bid1, bid2 });
            _unitOfWork.SaveChanges();

            // Act
            var soldItem = _bidService.SellItem(user1.Id, userItem.UserItemId, bid2.BidderId);

            // Assert
            var foundItem = _unitOfWork.UserItems.Get(userItem.UserItemId);

            Assert.IsNull(soldItem);
            Assert.IsFalse(foundItem.Sold);
        }
    }
}
