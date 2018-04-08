using ISofA.DAL.Core.Domain;
using ISofA.SL.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISofA.Tests.Unit.Student_3.StageService
{
    [TestClass]
    public class StageService_Remove : StageServiceTest
    {

        [TestInitialize]
        public void Initialize()
        {
            Init();
        }

        [TestMethod]
        public void StageService_Remove_Ok()
        {
            // Arrange
            var theater = _unitOfWork.Theaters.Add(new Theater() { Name = "Arena Cineplex" });
            var stage = _unitOfWork.Stages.Add(new Stage() { TheaterId = theater.TheaterId, Name = "A1" });
            _unitOfWork.SaveChanges();

            // Act            
            _stageService.Remove(stage.StageId);
            var stages = _stageService.GetAll(theater.TheaterId);
            // Assert
            Assert.AreEqual(stages.Count(), 0);
        }

        [TestMethod]
        public void StageService_Remove_StageNotFound()
        {
            // Arrange
            // Act                        
            // Assert
            Assert.ThrowsException<StageNotFoundException>(() => _stageService.Remove(1));
        }
    }
}
