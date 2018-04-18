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
    public class UserItemService_Add
    {
        private TestUnitOfWork _unitOfWork;
        private UserItemService _userItemService;

        public UserItemService_Add()
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
        public void UserItems_Add_2_Ok()
        {
            // Arrange
            var theater1 = new Theater { Name = "Arena" };
            _unitOfWork.Theaters.Add(theater1);
            _unitOfWork.SaveChanges();

            var user1 = Utils.GetTestUser(1);
            _unitOfWork.Users.Add(user1);
            _unitOfWork.SaveChanges();

            // Act
            _userItemService.AddItem(theater1.TheaterId, user1.Id, new UserItem
            {
                ExpirationDate = DateTime.Now.AddHours(2),
            });

            _userItemService.AddItem(theater1.TheaterId, user1.Id, new UserItem
            {
                ExpirationDate = DateTime.Now.AddHours(2),
            });

            // Assert
            var items = _unitOfWork.UserItems
                .Find(x => x.TheaterId == theater1.TheaterId && x.ISofAUserId == user1.Id);

            Assert.AreEqual(2, items.Count());
        }

        [TestMethod]
        public void UserItems_Add_1_Ok()
        {
            // Arrange
            var theater1 = new Theater { Name = "Arena" };
            var theater2 = new Theater { Name = "SNP" };
            _unitOfWork.Theaters.Add(theater1);
            _unitOfWork.Theaters.Add(theater2);
            _unitOfWork.SaveChanges();

            var user1 = Utils.GetTestUser(1);
            _unitOfWork.Users.Add(user1);
            _unitOfWork.SaveChanges();

            // Act
            _userItemService.AddItem(theater1.TheaterId, user1.Id, new UserItem
            {
                ExpirationDate = DateTime.Now.AddHours(2),
            });

            _userItemService.AddItem(theater1.TheaterId, user1.Id, new UserItem
            {
                ExpirationDate = DateTime.Now.AddHours(2),
            });

            _userItemService.AddItem(theater2.TheaterId, user1.Id, new UserItem
            {
                ExpirationDate = DateTime.Now.AddHours(2),
            });

            // Assert
            var items = _unitOfWork.UserItems
                .Find(x => x.TheaterId == theater2.TheaterId);

            Assert.AreEqual(1, items.Count());
        }
    }
}
