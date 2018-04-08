using ISofA.DAL.Core.Domain;
using ISofA.SL.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace ISofA.Tests.Unit.Student_3.PlayService
{
    [TestClass]
    public class PlayService_Remove : PlayServiceTest
    {

        [TestInitialize]
        public void Initialize()
        {
            Init();
        }

        [TestMethod]
        public void PlayService_Remove_Ok()
        {
            // Arrange
            var theater = _unitOfWork.Theaters.Add(new Theater() { Name = "Arena Cineplex" });
            var play1 = _unitOfWork.Plays.Add(new Play() { TheaterId = theater.TheaterId, Name = "Movie 1" });
            _unitOfWork.SaveChanges();

            // Act            
            _playService.Remove(play1.PlayId);
            var dto = _playService.Get(play1.PlayId);
            // Assert
            Assert.AreEqual(dto.Active, false);
        }

        [TestMethod]
        public void PlayService_Remove_PlayNotFound()
        {
            // Arrange
            // Act                        
            // Assert
            Assert.ThrowsException<PlayNotFoundException>(() => _playService.Remove(1));
        }

    }
}
