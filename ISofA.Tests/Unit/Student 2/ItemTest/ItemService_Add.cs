using ISofA.DAL.Core.Domain;
using ISofA.SL.Implementations;
using ISofA.Tests.Persistence;
using Microsoft.AspNet.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISofA.Tests.Unit.Student_2.ItemTest
{
    [TestClass]
    public class ItemService_Add
    {
        private TestUnitOfWork _unitOfWork;
        private ItemService _itemService;

        public ItemService_Add()
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
        public void ItemService_Add_2_Ok()
        {
            var theater = new Theater { Name = "Arena", Type = TheaterType.Cinema};
            var theater2 = new Theater { Name = "SNP", Type = TheaterType.Play };
            _unitOfWork.Theaters.Add(theater);
            _unitOfWork.Theaters.Add(theater2);
            _unitOfWork.SaveChanges();

            _itemService.AddItem(theater.TheaterId, new Item { Name = "Arena item" });
            _itemService.AddItem(theater.TheaterId, new Item { Name = "Arena item" });
            _itemService.AddItem(theater2.TheaterId, new Item { Name = "SNP item" });

            var theaterItems = _itemService.GetItemsForTheater(theater.TheaterId);
            var theater2Items = _itemService.GetItemsForTheater(theater2.TheaterId);

            Assert.AreEqual(2, theaterItems.Count());
            Assert.AreEqual(1, theater2Items.Count());
        }

    }
}
