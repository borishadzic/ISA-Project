using System;
using System.Linq;
using ISofA.DAL.Core.Domain;
using ISofA.SL.Exceptions;
using ISofA.Tests.DI;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ISofA.Tests.Unit.Student_3.PlayService
{
    [TestClass]
    public class PlayService_Get : PlayServiceTest
    {
        
        [TestInitialize]
        public void Initialize()
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
            var plays = _playService.GetAll(theater.TheaterId);

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
            var plays = _playService.GetAll(theater.TheaterId);

            // Assert
            Assert.AreEqual(1, plays.Count());
        }

        [TestMethod]
        public void PlayService_Get_All_TheaterNotFound()
        {
            // Arrange
            // Act               
            // Assert
            Assert.ThrowsException<TheaterNotFoundException>(() => _playService.GetAll(0));
        }

        [TestMethod]
        public void PlayService_Get_1()
        {
            // Arrange
            var theater = _unitOfWork.Theaters.Add(new Theater() { Name = "Arena Cineplex" });
            var play1 = _unitOfWork.Plays.Add(new Play() { TheaterId = theater.TheaterId, Name = "Movie 1" });
            _unitOfWork.SaveChanges();

            // Act
            var play = _playService.Get(play1.PlayId);

            // Assert
            Assert.AreEqual(play1.Name, play.Name);
            Assert.AreEqual(play1.TheaterId, play.TheaterId);
        }        

        [TestMethod]
        public void PlayService_Get_1_PlayNotFound()
        {
            // Arrange
            var theater = _unitOfWork.Theaters.Add(new Theater() { Name = "Arena Cineplex" });
            _unitOfWork.SaveChanges();
            // Act               
            // Assert
            var ex = Assert.ThrowsException<PlayNotFoundException>(() => _playService.Get(0));
        }
    }
}
