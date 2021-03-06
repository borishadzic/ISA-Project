﻿using ISofA.DAL.Core.Domain;
using ISofA.SL.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace ISofA.Tests.Unit.Student_3.PlayService
{
    [TestClass]
    public class PlayService_Add : PlayServiceTest
    {

        [TestInitialize]
        public void Initialize()
        {
            Init();
        }

        [TestMethod]
        public void PlayService_Add_Ok()
        {
            // Arrange
            var theater = _unitOfWork.Theaters.Add(new Theater() { Name = "Arena Cineplex" });
            _unitOfWork.SaveChanges();

            // Act            
            var play = new Play() { Name = "Movie 1" };
            _playService.Add(theater.TheaterId, play);
            var plays = _playService.GetAll(theater.TheaterId);
            // Assert
            Assert.AreEqual(play.Active, true);
            Assert.AreEqual(plays.Count(), 1);
        }

        [TestMethod]
        public void PlayService_Add_TheaterNotFound()
        {
            // Arrange
            // Act                        
            // Assert
            Assert.ThrowsException<TheaterNotFoundException>(() => _playService.Add(1, new Play() { Name = "Movie 1" }));
        }

        [TestMethod]
        public void PlayService_Add_PlayNull()
        {
            // Arrange
            // Act                        
            // Assert
            Assert.ThrowsException<BadRequestException>(() => _playService.Add(1, null));
        }

    }
}
