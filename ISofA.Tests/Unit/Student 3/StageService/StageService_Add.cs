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
    public class StageService_Add : StageServiceTest
    {

        [TestInitialize]
        public void Initialize()
        {
            Init();
        }

        [TestMethod]
        public void StageService_Add_Ok()
        {
            // Arrange
            var theater = _unitOfWork.Theaters.Add(new Theater() { Name = "Arena Cineplex" });
            _unitOfWork.SaveChanges();

            // Act            
            var stage = new Stage() { Name = "A1" };
            _stageService.Add(theater.TheaterId, stage);
            var stages = _stageService.GetAll(theater.TheaterId);
            // Assert
            Assert.AreEqual(stages.Count(), 1);
        }

        [TestMethod]
        public void StageService_Add_TheaterNotFound()
        {
            // Arrange
            // Act                        
            // Assert
            Assert.ThrowsException<TheaterNotFoundException>(() => _stageService.Add(1, new Stage() { Name = "A1" }));
        }
    }
}
