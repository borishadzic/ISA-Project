using ISofA.DAL.Core.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace ISofA.Tests.Unit.Student_3.PlayService
{
    [TestClass]
    public class PlayService_Add : PlayServiceTest
    {

        [TestInitialize]
        public void PlayService_Add_Initialize()
        {
            Init();
        }

        [TestMethod]
        public void PlayService_Add_1()
        {
            // Arrange
            var theater = _unitOfWork.Theaters.Add(new Theater() { Name = "Arena Cineplex" });
            _unitOfWork.SaveChanges();

            // Act            
            var play = _playService.Add(theater.TheaterId, new Play() { Name = "Movie 1", TheaterId = theater.TheaterId });
            var plays = _playService.GetRepertoire(theater.TheaterId);
            // Assert
            Assert.AreEqual(plays.Count(), 1);
        }
    }
}
