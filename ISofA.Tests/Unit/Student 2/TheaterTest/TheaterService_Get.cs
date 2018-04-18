using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISofA.Tests.Persistence;
using ISofA.DAL.Core.Domain;
using ISofA.SL.Implementations;

namespace ISofA.Tests.Unit.Student_2.TheaterTest
{
    [TestClass]
    public class TheaterService_Get
    {
        private TheaterService _theaterService;
        private TestUnitOfWork _unitOfWork;

        public TheaterService_Get()
        {
            _unitOfWork = new TestUnitOfWork();
            _theaterService = new TheaterService(_unitOfWork);
        }

        [TestInitialize]
        public void TestInitialize()
        {
            _unitOfWork.NukeDatabase();
        }

        [TestMethod]
        public void Theater_Get_All_Cinemas_2()
        {
            // Arrange
            _unitOfWork.Theaters.Add(new Theater { Name = "Arena Cineplex", Type = TheaterType.Cinema });
            _unitOfWork.Theaters.Add(new Theater { Name = "Sinestar", Type = TheaterType.Cinema });
            _unitOfWork.SaveChanges();

            // Act
            var theaters = _theaterService.GetAllCinemas();

            // Assert
            Assert.AreEqual(2, theaters.Count());
        }

        [TestMethod]
        public void Theater_Get_All_Play_Theaters_1()
        {
            // Arrange
            _unitOfWork.Theaters.Add(new Theater { Name = "Arena Cineplex", Type = TheaterType.Cinema });
            _unitOfWork.Theaters.Add(new Theater { Name = "SNP", Type = TheaterType.Play });
            _unitOfWork.SaveChanges();

            // Act
            var theaters = _theaterService.GetAllPlayTheaters();

            // Assert
            Assert.AreEqual(1, theaters.Count());
        }

        [TestMethod]
        public void Theater_Get_All_4()
        {
            // Arrange
            _unitOfWork.Theaters.Add(new Theater { Name = "Arena Cineplex", Type = TheaterType.Cinema });
            _unitOfWork.Theaters.Add(new Theater { Name = "SNP", Type = TheaterType.Play });
            _unitOfWork.Theaters.Add(new Theater { Name = "Pozorište Mladih", Type = TheaterType.Play });
            _unitOfWork.Theaters.Add(new Theater { Name = "Sinestar", Type = TheaterType.Cinema });
            _unitOfWork.SaveChanges();

            // Act
            var theaters = _theaterService.GetAll();

            // Assert
            Assert.AreEqual(4, theaters.Count());
        }

        [TestMethod]
        public void Theater_Get_1_Ok()
        {
            // Arrange
            var newTheater = new Theater { Name = "Arena Cineplex", Type = TheaterType.Cinema };
            _unitOfWork.Theaters.Add(newTheater);
            _unitOfWork.SaveChanges();

            // Act
            var theater = _theaterService.Get(newTheater.TheaterId);

            // Assert
            Assert.AreEqual(newTheater.Name, theater.Name);
        }

        [TestMethod]
        public void Theater_Get_1_Null()
        {
            // Act
            var theater = _theaterService.Get(1);

            // Assert
            Assert.IsNull(theater);
        }

    }
}
