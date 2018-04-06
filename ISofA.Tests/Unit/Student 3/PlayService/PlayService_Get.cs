using System;
using System.Linq;
using ISofA.DAL.Core.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ISofA.Tests.Unit.Student_3.PlayService
{
    [TestClass]
    public class PlayService_Get : PlayServiceTest
    {
        
        [TestInitialize]
        public void PlayService_Get_Initialize()
        {
            Init();
        }

        [TestMethod]
        public void PlayService_Get_All_0()
        {
            // Arrange
            var theater = _unitOfWork.Theaters.Add(new Theater() { Name = "Arena Cineplex" });
            _unitOfWork.SaveChanges();

            // Act
            var plays = _playService.GetRepertoire(theater.TheaterId);

            // Assert
            Assert.AreEqual(0, plays.Count());
        }

        [TestMethod]
        public void PlayService_Get_All_1()
        {
            // Arrange
            var theater = _unitOfWork.Theaters.Add(new Theater() { Name = "Arena Cineplex" });
            var play1 = _unitOfWork.Plays.Add(new Play() { TheaterId = theater.TheaterId, Name = "Movie 1" });
            _unitOfWork.SaveChanges();

            // Act
            var plays = _playService.GetRepertoire(theater.TheaterId);

            // Assert
            Assert.AreEqual(1, plays.Count());
        }

        [TestMethod]
        public void PlayService_Get_All_TheaterNotFound()
        {
            // Arrange
            // Act   
            var ex = Assert.ThrowsException<Exception>(() => _playService.GetRepertoire(0));
            // Assert            
            Assert.AreEqual(nameof(Theater), "Theater");
        }

        [TestMethod]
        public void PlayService_Get_1()
        {
            // Arrange
            var theater = _unitOfWork.Theaters.Add(new Theater() { Name = "Arena Cineplex" });
            var play1 = _unitOfWork.Plays.Add(new Play() { TheaterId = theater.TheaterId, Name = "Movie 1" });
            _unitOfWork.SaveChanges();

            // Act
            var play = _playService.Get(theater.TheaterId, play1.PlayId);

            // Assert
            Assert.AreEqual(play1.Name, play.Name);
            Assert.AreEqual(play1.TheaterId, play.TheaterId);
        }

        [TestMethod]
        public void PlayService_Get_1_TheaterNotFound()
        {
            // Arrange
            // Act   
            var ex = Assert.ThrowsException<Exception>(() => _playService.Get(0, 0));
            // Assert            
            Assert.AreEqual(nameof(Theater), "Theater");
        }

        [TestMethod]
        public void PlayService_Get_1_PlayNotFound()
        {
            // Arrange
            var theater = _unitOfWork.Theaters.Add(new Theater() { Name = "Arena Cineplex" });
            _unitOfWork.SaveChanges();
            // Act   
            var ex = Assert.ThrowsException<Exception>(() => _playService.Get(theater.TheaterId, 0));
            // Assert            
            Assert.AreEqual(nameof(Play), "Play");
        }
    }
}
