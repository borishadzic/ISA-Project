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
    public class TheaterService_Add
    {
        private TestUnitOfWork _unitOfWork;
        private TheaterService _theaterService;

        public TheaterService_Add()
        {
            _unitOfWork = new TestUnitOfWork();
            _theaterService = new TheaterService(_unitOfWork);
        }

        [TestInitialize]
        public void Initilize()
        {
            _unitOfWork.NukeDatabase();
        }

        [TestMethod]
        public void Theater_Add_1()
        {
            // Arrange
            _theaterService.Add(new Theater { Name = "Arena Cineplex", Type = TheaterType.Cinema});

            // Act
            var theaters = _unitOfWork.Theaters.GetAll();

            // Assert
            Assert.AreEqual(1, theaters.Count());
        }

        [TestMethod]
        public void Theater_Add_2()
        {
            // Arrange
            _theaterService.Add(new Theater { Name = "Arena Cineplex", Type = TheaterType.Cinema });
            _theaterService.Add(new Theater { Name = "Arena Cineplex", Type = TheaterType.Play });

            // Act
            var theaters = _unitOfWork.Theaters.GetAll();

            // Assert
            Assert.AreEqual(2, theaters.Count());
        }
    }
}
