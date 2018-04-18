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
    public class TheaterService_Update
    {
        private TestUnitOfWork _unitOfWork;
        private TheaterService _theaterService;

        public TheaterService_Update()
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
        public void Theater_Update_Ok()
        {
            // Arrange
            var theater = new Theater
            {
                Name = "Arena cineplex",
                Latitude = 15,
                Longitude = 15,
                Type = TheaterType.Cinema
            };
            _unitOfWork.Theaters.Add(theater);
            _unitOfWork.SaveChanges();

            // Act
            var updateAct = new Theater
            {
                Name = "SNP",
                Latitude = 30,
                Longitude = 30,
                Type = TheaterType.Play
            };

            var updated = _theaterService.Update(theater.TheaterId, updateAct);

            var afterUpdateCinemas = _theaterService.GetAllCinemas();
            var afterUpdatePlays = _theaterService.GetAllPlayTheaters();

            // Assert
            Assert.AreEqual(updateAct.Name, updated.Name);
            Assert.AreEqual(updateAct.Latitude, updated.Latitude);
            Assert.AreEqual(updateAct.Longitude, updated.Longitude);
            Assert.AreEqual(0, afterUpdateCinemas.Count());
            Assert.AreEqual(1, afterUpdatePlays.Count());
        }

        [TestMethod]
        public void Theater_Update_Null()
        {
            // Arrange
            var theater = new Theater
            {
                Name = "Arena cineplex",
                Latitude = 15,
                Longitude = 15,
                Type = TheaterType.Cinema
            };

            // Act
            var updated = _theaterService.Update(1, theater);

            // Assert
            Assert.IsNull(updated);
        }

    }
}
