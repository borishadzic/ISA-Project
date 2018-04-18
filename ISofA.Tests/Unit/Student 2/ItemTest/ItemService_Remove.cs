using ISofA.DAL.Core.Domain;
using ISofA.SL.Implementations;
using ISofA.Tests.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISofA.Tests.Unit.Student_2.ItemTest
{
    [TestClass]
    public class ItemService_Remove
    {
        private TestUnitOfWork _unitOfWork;
        private ItemService _itemService;

        public ItemService_Remove()
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
        public void Item_Remove_2_Ok()
        {
            // Arrange
            var theater = new Theater { Name = "Arena" };
            _unitOfWork.Theaters.Add(theater);
            var item1 = new Item { Name = "Test item 1", TheaterId = theater.TheaterId };
            var item2 = new Item { Name = "Test item 2", TheaterId = theater.TheaterId };
            var item3 = new Item { Name = "Test item 3", TheaterId = theater.TheaterId };
            _unitOfWork.Items.AddRange(new Item[] { item1, item2, item3 });
            _unitOfWork.SaveChanges();

            // Act
            _itemService.RemoveItem(theater.TheaterId, item1.ItemId);
            _itemService.RemoveItem(theater.TheaterId, item2.ItemId);

            // Assert
            Assert.AreEqual(1, _unitOfWork.Items.GetAll().Count());
        }

        [TestMethod]
        public void Item_Remove_0_Ok()
        {
            // Arrange
            var theater = new Theater { Name = "Arena" };
            _unitOfWork.Theaters.Add(theater);
            var item1 = new Item { Name = "Test item 1", TheaterId = theater.TheaterId };
            var item2 = new Item { Name = "Test item 2", TheaterId = theater.TheaterId };
            var item3 = new Item { Name = "Test item 3", TheaterId = theater.TheaterId };
            _unitOfWork.Items.AddRange(new Item[] { item1, item2, item3 });
            _unitOfWork.SaveChanges();

            // Act
            _itemService.RemoveItem(2, item1.ItemId);
            _itemService.RemoveItem(theater.TheaterId, Guid.NewGuid());

            // Assert
            Assert.AreEqual(3, _unitOfWork.Items.GetAll().Count());
        }
    }
}
