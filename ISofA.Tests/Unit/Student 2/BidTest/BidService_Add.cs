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
    public class BidService_Add
    {
        private TestUnitOfWork _unitOfWork;
        private BidService _bidService;

        public BidService_Add()
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
        public void Bids_Add_2_Ok()
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

            var userItem = new UserItem {
                TheaterId = theater.TheaterId,
                ISofAUserId = user1.Id,
                ExpirationDate = DateTime.Now.AddHours(2),
                Approved = true,
                Sold = false
            };
            _unitOfWork.UserItems.Add(userItem);
            _unitOfWork.SaveChanges();

            // Act
            _bidService.AddBid(userItem.UserItemId, user2.Id, new Bid { BidAmount = 50 });
            _bidService.AddBid(userItem.UserItemId, user3.Id, new Bid { BidAmount = 60 });

            // Assert
            var bids = _unitOfWork.Bids.Find(x => x.UserItemId == userItem.UserItemId);

            Assert.AreEqual(2, bids.Count());
        }

        [TestMethod]
        public void Bids_Add_2_SameUser()
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

            // Act
            _bidService.AddBid(userItem.UserItemId, user2.Id, new Bid { BidAmount = 50 });
            _bidService.AddBid(userItem.UserItemId, user3.Id, new Bid { BidAmount = 60 });
            _bidService.AddBid(userItem.UserItemId, user2.Id, new Bid { BidAmount = 70 });

            // Assert
            var bids = _unitOfWork.Bids.Find(x => x.UserItemId == userItem.UserItemId);

            Assert.AreEqual(2, bids.Count());
        }

        [TestMethod]
        public void Bids_Add_Not_Enough()
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

            // Act
            _bidService.AddBid(userItem.UserItemId, user2.Id, new Bid { BidAmount = 50 });
            _bidService.AddBid(userItem.UserItemId, user3.Id, new Bid { BidAmount = 60 });
            _bidService.AddBid(userItem.UserItemId, user2.Id, new Bid { BidAmount = 65 });

            // Assert
            var foundUserItem = _unitOfWork.UserItems.Get(userItem.UserItemId);

            Assert.AreEqual(60, foundUserItem.HighestBid);
        }

        [TestMethod]
        public void Bids_Add_1()
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

            // Act
            _bidService.AddBid(userItem.UserItemId, user2.Id, new Bid { BidAmount = 50 });
            _bidService.AddBid(userItem.UserItemId, user2.Id, new Bid { BidAmount = 60 });
            _bidService.AddBid(userItem.UserItemId, user2.Id, new Bid { BidAmount = 70 });

            // Assert
            var foundUserItem = _unitOfWork.UserItems.Get(userItem.UserItemId);
            var bids = _unitOfWork.Bids.Find(x => x.UserItemId == userItem.UserItemId);

            Assert.AreEqual(1, bids.Count());
            Assert.AreEqual(70, foundUserItem.HighestBid);
        }

        [TestMethod]
        public void Bids_Add_Expired()
        {
            // Arrange
            var theater = new Theater { Name = "Arena" };
            _unitOfWork.Theaters.Add(theater);
            _unitOfWork.SaveChanges();

            var user1 = Utils.GetTestUser(1);
            _unitOfWork.Users.AddRange(new[] { user1 });
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

            // Act
            _bidService.AddBid(userItem.UserItemId, user1.Id, new Bid { BidAmount = 50 });

            // Assert
            var bids = _unitOfWork.Bids.Find(x => x.UserItemId == userItem.UserItemId);
            Assert.AreEqual(0, bids.Count());
        }

        [TestMethod]
        public void Bids_Add_Not_Approved()
        {
            // Arrange
            var theater = new Theater { Name = "Arena" };
            _unitOfWork.Theaters.Add(theater);
            _unitOfWork.SaveChanges();

            var user1 = Utils.GetTestUser(1);
            _unitOfWork.Users.AddRange(new[] { user1 });
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

            // Act
            _bidService.AddBid(userItem.UserItemId, user1.Id, new Bid { BidAmount = 50 });

            // Assert
            var bids = _unitOfWork.Bids.Find(x => x.UserItemId == userItem.UserItemId);
            Assert.AreEqual(0, bids.Count());
        }

        [TestMethod]
        public void Bids_Add_Sold()
        {
            // Arrange
            var theater = new Theater { Name = "Arena" };
            _unitOfWork.Theaters.Add(theater);
            _unitOfWork.SaveChanges();

            var user1 = Utils.GetTestUser(1);
            _unitOfWork.Users.AddRange(new[] { user1 });
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

            // Act
            _bidService.AddBid(userItem.UserItemId, user1.Id, new Bid { BidAmount = 50 });

            // Assert
            var bids = _unitOfWork.Bids.Find(x => x.UserItemId == userItem.UserItemId);
            Assert.AreEqual(0, bids.Count());
        }
    }
}
