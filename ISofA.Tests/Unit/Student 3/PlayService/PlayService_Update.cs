using ISofA.DAL.Core.Domain;
using ISofA.SL.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace ISofA.Tests.Unit.Student_3.PlayService
{
    [TestClass]
    public class PlayService_Update : PlayServiceTest
    {

        [TestInitialize]
        public void Initialize()
        {
            Init();
        }

        [TestMethod]
        public void PlayService_Update_Ok()
        {
            // Arrange
            var theater = _unitOfWork.Theaters.Add(new Theater() { Name = "Arena Cineplex" });
            var play1 = _unitOfWork.Plays.Add(new Play() { TheaterId = theater.TheaterId, Name = "Movie 1", Active = true });
            _unitOfWork.SaveChanges();

            // Act            
            _playService.Update(play1.PlayId, new Play() { Name = "Movie 2", Active = false });
            var dto = _playService.Get(play1.PlayId);
            // Assert
            Assert.AreEqual(dto.Name, "Movie 2");
            Assert.AreEqual(dto.Active, true);            
        }

        [TestMethod]
        public void PlayService_Update_PlayNotFound()
        {
            // Arrange
            // Act                        
            // Assert
            Assert.ThrowsException<PlayNotFoundException>(() => _playService.Update(1, new Play()));
        }

        public void PlayService_Update_PlayNull()
        {
            // Arrange
            // Act                        
            // Assert
            Assert.ThrowsException<BadRequestException>(() => _playService.Update(1, null));
        }

    }
}
