using ISofA.DAL.Core.Domain;
using ISofA.SL.Implementations;
using ISofA.Tests.Persistence;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISofA.Tests.Unit.Student_2.ItemTest
{
    [TestClass]
    public class ItemService_Buy
    {
        private TestUnitOfWork _unitOfWork;
        private ItemService _itemService;

        public ItemService_Buy()
        {
            _unitOfWork = new TestUnitOfWork();
            _itemService = new ItemService(_unitOfWork, null);
        }

        [TestInitialize]
        public void Initialize()
        {
            _unitOfWork.NukeDatabase();
        }

        [TestMethod]
        public void Items_Buy_2_Ok()
        {
            // Arrange
            var user = Utils.GetTestUser(1);
            _unitOfWork.Users.Add(user);

            var theater = new Theater { Name = "Arena" };
            _unitOfWork.Theaters.Add(theater);
            var item1 = new Item { Name = "Test item 1", TheaterId = theater.TheaterId };
            var item2 = new Item { Name = "Test item 2", TheaterId = theater.TheaterId };
            var item3 = new Item { Name = "Test item 3", TheaterId = theater.TheaterId };
            _unitOfWork.Items.AddRange(new Item[] { item1, item2, item3 });
            _unitOfWork.SaveChanges();

            // Act
            bool success = _itemService.BuyItems(new Item[] { item1, item2 }, user.Id);
            Assert.IsTrue(success);

            var afterBuy = _itemService.GetItemsForTheater(theater.TheaterId);
            Assert.AreEqual(1, afterBuy.Count());

            var bought = _itemService.GetBoughtItemsForTheater(theater.TheaterId);
            Assert.AreEqual(2, bought.Count());
        }

        [TestMethod]
        public void Items_Buy_2_Fail()
        {
            // Arrange
            var user = Utils.GetTestUser(1);
            _unitOfWork.Users.Add(user);

            var theater = new Theater { Name = "Arena" };
            _unitOfWork.Theaters.Add(theater);
            var item1 = new Item { Name = "Test item 1", TheaterId = theater.TheaterId };
            var item2 = new Item { Name = "Test item 2", TheaterId = theater.TheaterId };
            var item3 = new Item { Name = "Test item 3", TheaterId = theater.TheaterId };
            _unitOfWork.Items.AddRange(new Item[] { item1, item2, item3 });
            _unitOfWork.SaveChanges();

            // Act
            bool success = _itemService.BuyItems(new Item[] { item1, item1 }, user.Id);
            Assert.IsFalse(success);
        }
    }
}
