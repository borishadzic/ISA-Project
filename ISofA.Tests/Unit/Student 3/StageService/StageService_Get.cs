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
    public class StageService_Get : StageServiceTest
    {

        [TestInitialize]
        public void Initialize()
        {
            Init();
        }

        [TestMethod]
        public void StageService_Get_All_0()
        {
            // Arrange
            var theater = _unitOfWork.Theaters.Add(new Theater() { Name = "Arena Cineplex" });
            _unitOfWork.SaveChanges();

            // Act
            var stages = _stageService.GetAll(theater.TheaterId);

            // Assert
            Assert.AreEqual(0, stages.Count());
        }

        [TestMethod]
        public void StageService_Get_All_1()
        {
            // Arrange
            var theater = _unitOfWork.Theaters.Add(new Theater() { Name = "Arena Cineplex" });
            var stage = _unitOfWork.Stages.Add(new Stage() { TheaterId = theater.TheaterId, Name = "A1" });
            _unitOfWork.SaveChanges();

            // Act
            var stages = _stageService.GetAll(theater.TheaterId);

            // Assert
            Assert.AreEqual(1, stages.Count());
        }

        [TestMethod]
        public void StageService_Get_All_TheaterNotFound()
        {
            // Arrange
            // Act               
            // Assert
            Assert.ThrowsException<TheaterNotFoundException>(() => _stageService.GetAll(0));
        }
    }
}
