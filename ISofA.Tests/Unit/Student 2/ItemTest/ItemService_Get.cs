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
    public class ItemService_Get
    {
        private TestUnitOfWork _unitOfWork;
        private ItemService _itemService;

        public ItemService_Get()
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
        public void Items_Get_All_2()
        {
            var theater = new Theater { Name = "Arena" };
            _unitOfWork.Theaters.Add(theater);
            _unitOfWork.Items.Add(new Item { Name = "Test item", TheaterId = theater.TheaterId });
            _unitOfWork.Items.Add(new Item { Name = "Test2 item", TheaterId = theater.TheaterId });
            _unitOfWork.SaveChanges();

            var items = _itemService.GetItemsForTheater(theater.TheaterId);

            Assert.AreEqual(2, items.Count());
        }

        [TestMethod]
        public void Items_Get_One_Ok()
        {
            var theater = new Theater { Name = "Arena" };
            _unitOfWork.Theaters.Add(theater);
            var newItem = new Item { Name = "Test item", TheaterId = theater.TheaterId };
            _unitOfWork.Items.Add(newItem);
            _unitOfWork.SaveChanges();

            var item = _itemService.GetItem(theater.TheaterId, newItem.ItemId);

            Assert.AreEqual(newItem.Name, item.Name);
            Assert.AreEqual(newItem.TheaterId, item.TheaterId);
        }

        [TestMethod]
        public void Items_Get_One_Null()
        {
            var theater = new Theater { Name = "Arena" };
            _unitOfWork.Theaters.Add(theater);
            var newItem = new Item { Name = "Test item", TheaterId = theater.TheaterId };
            _unitOfWork.Items.Add(newItem);
            _unitOfWork.SaveChanges();

            var item1 = _itemService.GetItem(2, newItem.ItemId);
            var item2 = _itemService.GetItem(theater.TheaterId, Guid.NewGuid());

            Assert.IsNull(item1);
            Assert.IsNull(item2);
        }
    }
}
