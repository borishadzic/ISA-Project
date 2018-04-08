using ISofA.DAL.Core.Domain;
using ISofA.SL.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISofA.Tests.Unit.Student_3.ProjectionService
{
    [TestClass]
    public class ProjectionService_Add : ProjectionServiceTest
    {

        [TestInitialize]
        public void Initialize()
        {
            Init();
        }

        [TestMethod]
        public void ProjectionService_Add_Ok()
        {
            // Arrange
            var theater = _unitOfWork.Theaters.Add(new Theater() { Name = "Arena Cineplex" });
            var stage = _unitOfWork.Stages.Add(new Stage() { TheaterId = theater.TheaterId, Name = "A1" });
            var play = _unitOfWork.Plays.Add(new Play() { TheaterId = theater.TheaterId, Name = "Movie 1", Active = true });
            _unitOfWork.Projections.Add(new Projection() { PlayId = play.PlayId, StageId = stage.StageId, StartTime = DateTime.Today.AddDays(-1) });
            _unitOfWork.SaveChanges();

            // Act            
            var projection = new Projection() { PlayId = play.PlayId, StageId = stage.StageId, StartTime = DateTime.Today.AddDays(1) };
            _projectionService.Add(theater.TheaterId, projection);
            var projections = _projectionService.GetProjectionsForPlay(play.PlayId, DateTime.Today);
            // Assert
            Assert.AreEqual(projections.Count(), 1);
        }

        [TestMethod]
        public void ProjectionService_Add_TheaterNotFound()
        {
            // Arrange
            // Act                        
            // Assert
            Assert.ThrowsException<TheaterNotFoundException>(() => _projectionService.Add(1, new Projection()));
        }

        [TestMethod]
        public void ProjectionService_Add_PlayNotFound()
        {
            // Arrange
            var theater = _unitOfWork.Theaters.Add(new Theater() { Name = "Arena Cineplex" });
            var stage = _unitOfWork.Stages.Add(new Stage() { TheaterId = theater.TheaterId, Name = "A1" });
            _unitOfWork.SaveChanges();

            var projection = new Projection() { PlayId = 1, StageId = stage.StageId, StartTime = DateTime.Today.AddDays(1) };
            // Act            
            // Assert
            Assert.ThrowsException<PlayNotFoundException>(() => _projectionService.Add(theater.TheaterId, projection));
        }

        [TestMethod]
        public void ProjectionService_Add_StageNotFound()
        {
            // Arrange
            var theater = _unitOfWork.Theaters.Add(new Theater() { Name = "Arena Cineplex" });
            var play = _unitOfWork.Plays.Add(new Play() { TheaterId = theater.TheaterId, Name = "Movie 1", Active = true });
            _unitOfWork.SaveChanges();

            var projection = new Projection() { PlayId = play.PlayId, StageId = 1, StartTime = DateTime.Today.AddDays(1) };
            // Act            
            // Assert
            Assert.ThrowsException<StageNotFoundException>(() => _projectionService.Add(theater.TheaterId, projection));
        }

        [TestMethod]
        public void ProjectionService_Add_ProjectionNull()
        {
            // Arrange
            // Act                        
            // Assert
            Assert.ThrowsException<BadRequestException>(() => _projectionService.Add(1, null));
        }

        [TestMethod]
        public void ProjectionService_Add_BadRequest()
        {
            // Arrange
            var theater1 = _unitOfWork.Theaters.Add(new Theater() { Name = "Arena Cineplex" });
            _unitOfWork.SaveChanges();
            var theater2 = _unitOfWork.Theaters.Add(new Theater() { Name = "Bioskop 2" });
            _unitOfWork.SaveChanges();
            var stage1 = _unitOfWork.Stages.Add(new Stage() { TheaterId = theater1.TheaterId, Name = "A1" });
            var play1 = _unitOfWork.Plays.Add(new Play() { TheaterId = theater1.TheaterId, Name = "Movie 1", Active = true });
            var stage2 = _unitOfWork.Stages.Add(new Stage() { TheaterId = theater2.TheaterId, Name = "A2" });
            var play2 = _unitOfWork.Plays.Add(new Play() { TheaterId = theater2.TheaterId, Name = "Movie 2", Active = true });
            _unitOfWork.SaveChanges();
            // Act                        
            // Assert
            var projection1 = new Projection() { StageId = stage1.StageId, PlayId = play2.PlayId, StartTime = DateTime.Today.AddDays(1) };
            var projection2 = new Projection() { StageId = stage2.StageId, PlayId = play1.PlayId, StartTime = DateTime.Today.AddDays(1) };
            Assert.ThrowsException<BadRequestException>(() => _projectionService.Add(1, projection1));
            Assert.ThrowsException<BadRequestException>(() => _projectionService.Add(1, projection2));
        }
    }
}
