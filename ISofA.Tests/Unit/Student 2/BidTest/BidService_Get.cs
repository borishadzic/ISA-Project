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
    public class BidService_Get
    {
        private TestUnitOfWork _unitOfWork;
        private BidService _bidService;

        public BidService_Get()
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
        public void Bids_Get__All_3()
        {
            // Arrange
            var theater = new Theater { Name = "Arena" };
            _unitOfWork.Theaters.Add(theater);
            _unitOfWork.SaveChanges();

            var user1 = Utils.GetTestUser(1);
            var user2 = Utils.GetTestUser(2);
            var user3 = Utils.GetTestUser(3);
            var user4 = Utils.GetTestUser(4);
            _unitOfWork.Users.AddRange(new[] { user1, user2, user3, user4 });
            _unitOfWork.SaveChanges();

            var userItem1 = new UserItem
            {
                TheaterId = theater.TheaterId,
                ISofAUserId = user1.Id,
                ExpirationDate = DateTime.Now.AddHours(2),
                Approved = true,
                Sold = false
            };
            var userItem2 = new UserItem
            {
                TheaterId = theater.TheaterId,
                ISofAUserId = user1.Id,
                ExpirationDate = DateTime.Now.AddHours(2),
                Approved = true,
                Sold = false
            };
            _unitOfWork.UserItems.AddRange(new[] { userItem1, userItem2 });
            _unitOfWork.SaveChanges();

            var bid1 = new Bid { Bidder = user2, UserItem = userItem1, BidDate = DateTime.Now, BidAmount = 50 };
            var bid2 = new Bid { Bidder = user3, UserItem = userItem1, BidDate = DateTime.Now, BidAmount = 60 };
            var bid3 = new Bid { Bidder = user4, UserItem = userItem1, BidDate = DateTime.Now, BidAmount = 70 };
            var bid4 = new Bid { Bidder = user1, UserItem = userItem2, BidDate = DateTime.Now, BidAmount = 50 };
            _unitOfWork.Bids.AddRange(new[] { bid1, bid2, bid3, bid4 });
            _unitOfWork.SaveChanges();

            // Act
            var bids1 = _bidService.GetAll(userItem1.UserItemId);
            var bids2 = _bidService.GetAll(userItem2.UserItemId);
            var bids3 = _bidService.GetAll(Guid.NewGuid());

            // Arrange
            Assert.AreEqual(1, bids2.Count());
            Assert.AreEqual(3, bids1.Count());
            Assert.AreEqual(0, bids3.Count());
        }

        [TestMethod]
        public void Bids_Get_One_Ok()
        {
            // Arrange
            var theater = new Theater { Name = "Arena" };
            _unitOfWork.Theaters.Add(theater);
            _unitOfWork.SaveChanges();

            var user1 = Utils.GetTestUser(1);
            _unitOfWork.Users.AddRange(new[] { user1 });
            _unitOfWork.SaveChanges();

            var userItem1 = new UserItem
            {
                TheaterId = theater.TheaterId,
                ISofAUserId = user1.Id,
                ExpirationDate = DateTime.Now.AddHours(2),
                Approved = true,
                Sold = false
            };
            _unitOfWork.UserItems.AddRange(new[] { userItem1 });
            _unitOfWork.SaveChanges();

            var bid1 = new Bid { BidderId = user1.Id, UserItemId = userItem1.UserItemId, BidDate = DateTime.Now, BidAmount = 50 };
            _unitOfWork.Bids.AddRange(new[] { bid1 });
            _unitOfWork.SaveChanges();

            // Act
            var foundBid = _bidService.Get(userItem1.UserItemId, user1.Id);

            // Arrange
            Assert.IsNotNull(foundBid);
            Assert.AreEqual(foundBid.BidAmount, bid1.BidAmount);
        }

        [TestMethod]
        public void Bids_Get_One_Null()
        {
            // Arrange
            var theater = new Theater { Name = "Arena" };
            _unitOfWork.Theaters.Add(theater);
            _unitOfWork.SaveChanges();

            var user1 = Utils.GetTestUser(1);
            _unitOfWork.Users.AddRange(new[] { user1 });
            _unitOfWork.SaveChanges();

            var userItem1 = new UserItem
            {
                TheaterId = theater.TheaterId,
                ISofAUserId = user1.Id,
                ExpirationDate = DateTime.Now.AddHours(2),
                Approved = true,
                Sold = false
            };
            _unitOfWork.UserItems.AddRange(new[] { userItem1 });
            _unitOfWork.SaveChanges();

            var bid1 = new Bid { BidderId = user1.Id, UserItemId = userItem1.UserItemId, BidDate = DateTime.Now, BidAmount = 50 };
            _unitOfWork.Bids.AddRange(new[] { bid1 });
            _unitOfWork.SaveChanges();

            // Act
            var foundBid = _bidService.Get(userItem1.UserItemId, Guid.NewGuid().ToString());

            // Arrange
            Assert.IsNull(foundBid);
        }
    }
}
