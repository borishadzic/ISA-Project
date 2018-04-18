using ISofA.DAL.Core.Domain;
using ISofA.SL.Implementations;
using ISofA.Tests.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISofA.Tests.Unit.Student_2.TheaterTest
{
    [TestClass]
    public class TheaterService_Remove
    {
        private TestUnitOfWork _unitOfWork;
        private TheaterService _theaterService;

        public TheaterService_Remove()
        {
            _unitOfWork = new TestUnitOfWork();
            _theaterService = new TheaterService(_unitOfWork);
        }

        [TestInitialize]
        public void Initialize()
        {
            _unitOfWork.NukeDatabase();
        }

        [TestMethod]
        public void Theater_Remove_1()
        {
            // Arrange
            var newTheater = new Theater { Name = "Remove" };
            _unitOfWork.Theaters.Add(newTheater);
            _unitOfWork.SaveChanges();

            // Act
            var beforeDelete = _theaterService.GetAll();
            _theaterService.Remove(newTheater.TheaterId);
            var afterDelete = _theaterService.GetAll();

            // Assert
            Assert.AreEqual(1, beforeDelete.Count());
            Assert.AreEqual(0, afterDelete.Count());
        }

        [TestMethod]
        public void Theater_Remove_Null()
        {
            // Arrange
            var newTheater = new Theater { Name = "Remove" };
            _unitOfWork.Theaters.Add(newTheater);
            _unitOfWork.SaveChanges();

            // Act
            _theaterService.Remove(newTheater.TheaterId);
            var theater = _theaterService.Get(newTheater.TheaterId);

            // Assert
            Assert.IsNull(theater);
        }

    }
}
