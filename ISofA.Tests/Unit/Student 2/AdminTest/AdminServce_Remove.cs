using ISofA.DAL.Core.Domain;
using ISofA.SL.Implementations;
using ISofA.Tests.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISofA.Tests.Unit.Student_2.AdminTest
{
    [TestClass]
    public class AdminServce_Remove
    {
        private TestUnitOfWork _unitOfWork;
        private AdminService _adminService;

        public AdminServce_Remove()
        {
            _unitOfWork = new TestUnitOfWork();
            _adminService = new AdminService(_unitOfWork);
        }

        [TestInitialize]
        public void Initialize()
        {
            _unitOfWork.NukeDatabase();
        }

        [TestMethod]
        public void AdminsFanZone_Remove_1_Ok()
        {
            // Arrange
            var theater1 = new Theater { Name = "Arena" };
            var theater2 = new Theater { Name = "SNP" };
            _unitOfWork.Theaters.Add(theater1);
            _unitOfWork.Theaters.Add(theater2);
            _unitOfWork.SaveChanges();

            var user1 = Utils.CreateFanZoneAdmin(1, theater1.TheaterId);
            _unitOfWork.Users.Add(user1);
            _unitOfWork.Users.Add(Utils.CreateFanZoneAdmin(2, theater1.TheaterId));
            _unitOfWork.Users.Add(Utils.CreateTheaterAdmin(3, theater1.TheaterId));
            _unitOfWork.Users.Add(Utils.CreateFanZoneAdmin(4, theater2.TheaterId));
            _unitOfWork.SaveChanges();

            // Act
            _adminService.RemoveAdmin(theater1.TheaterId, user1.Id);

            // Assert
            var admins = _unitOfWork.Users.Find(x => x.AdminOfTheaterId == theater1.TheaterId);

            Assert.AreEqual(2, admins.Count());
        }

        [TestMethod]
        public void AdminsFanZone_Remove_0()
        {
            // Arrange
            var theater1 = new Theater { Name = "Arena" };
            var theater2 = new Theater { Name = "SNP" };
            _unitOfWork.Theaters.Add(theater1);
            _unitOfWork.Theaters.Add(theater2);
            _unitOfWork.SaveChanges();

            _unitOfWork.Users.Add(Utils.CreateFanZoneAdmin(1, theater1.TheaterId));
            _unitOfWork.Users.Add(Utils.CreateFanZoneAdmin(2, theater1.TheaterId));
            _unitOfWork.Users.Add(Utils.CreateTheaterAdmin(3, theater1.TheaterId));
            var user4 = Utils.CreateFanZoneAdmin(4, theater2.TheaterId);
            _unitOfWork.Users.Add(user4);
            _unitOfWork.SaveChanges();

            // Act
            _adminService.RemoveAdmin(theater1.TheaterId, user4.Id);

            // Assert
            var admins = _unitOfWork.Users.Find(x => x.AdminOfTheaterId == theater1.TheaterId);

            Assert.AreEqual(3, admins.Count());
        }

    }
}
