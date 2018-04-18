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
    public class UserItemService_Approve
    {
        private TestUnitOfWork _unitOfWork;
        private UserItemService _userItemService;

        public UserItemService_Approve()
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
        public void UserItems_Approve_2_Ok()
        {
            // Arrange
            var theater1 = new Theater { Name = "Arena" };
            var theater2 = new Theater { Name = "SNP" };
            _unitOfWork.Theaters.AddRange(new Theater[] { theater1, theater2 });
            _unitOfWork.SaveChanges();

            var user1 = Utils.GetTestUser(1);
            var user2 = Utils.CreateFanZoneAdmin(2, theater1.TheaterId);
            _unitOfWork.Users.AddRange(new ISofAUser[] { user1, user2 });
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
            var approved1 = _userItemService.ApproveItem(theater1.TheaterId, userItem1.UserItemId);
            var approved2 = _userItemService.ApproveItem(theater1.TheaterId, userItem2.UserItemId);

            // Assert
            var approved = _unitOfWork.UserItems
                .Find(x => x.TheaterId == theater1.TheaterId  && x.Approved == true);
            var notApproved = _unitOfWork.UserItems.Find(x => x.TheaterId == theater1.TheaterId && x.Approved == null);

            Assert.AreEqual(2, approved.Count());
            Assert.AreEqual(0, notApproved.Count());
        }
    }
}
