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
    public class StageService_Update : StageServiceTest
    {

        [TestInitialize]
        public void Initialize()
        {
            Init();
        }

        [TestMethod]
        public void StageService_Update_Ok()
        {
            // Arrange
            var theater = _unitOfWork.Theaters.Add(new Theater() { Name = "Arena Cineplex" });
            var stage = _unitOfWork.Stages.Add(new Stage() { TheaterId = theater.TheaterId, Name = "A1" });
            _unitOfWork.SaveChanges();

            // Act            
            var dto = _stageService.Update(stage.StageId, new Stage() { Name = "A2" });
            // Assert
            Assert.AreEqual(dto.Name, "A2");
        }

        [TestMethod]
        public void StageService_Update_StageNotFound()
        {
            // Arrange
            // Act                        
            // Assert
            Assert.ThrowsException<StageNotFoundException>(() => _stageService.Update(1, new Stage() { Name = "A2" }));
        }

        [TestMethod]
        public void StageService_Update_StageNull()
        {
            // Arrange
            // Act                        
            // Assert
            Assert.ThrowsException<BadRequestException>(() => _stageService.Update(1, null));
        }
    }
}
