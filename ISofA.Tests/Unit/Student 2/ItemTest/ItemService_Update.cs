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
    public class ItemService_Update
    {
        private TestUnitOfWork _unitOfWork;
        private ItemService _itemService;

        public ItemService_Update()
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
        public void Item_Update_Ok()
        {
            // Arrange
            var theater = new Theater { Name = "Arena" };
            _unitOfWork.Theaters.Add(theater);
            var item1 = new Item { Name = "Test item 1", TheaterId = theater.TheaterId };
            _unitOfWork.Items.Add(item1);
            _unitOfWork.SaveChanges();

            // Act
            var updated = _itemService.UpdateItem(theater.TheaterId, item1.ItemId, new Item
            {
                Name = "New name 1"
            });

            var updateGet = _unitOfWork.Items.Get(item1.ItemId);

            Assert.IsNotNull(updated);
            Assert.AreEqual("New name 1", updateGet.Name);
        }

        [TestMethod]
        public void Item_Update_Null()
        {
            // Arrange
            var theater = new Theater { Name = "Arena" };
            _unitOfWork.Theaters.Add(theater);
            var item1 = new Item { Name = "Test item 1", TheaterId = theater.TheaterId };
            _unitOfWork.Items.Add(item1);
            _unitOfWork.SaveChanges();

            // Act
            var updated = _itemService.UpdateItem(theater.TheaterId, Guid.NewGuid(), new Item
            {
                Name = "New name 1"
            });

            var updated2 = _itemService.UpdateItem(2, item1.ItemId, new Item
            {
                Name = "New name 1"
            });

            Assert.IsNull(updated);
            Assert.IsNull(updated2);
        }
    }
}
