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
    public class AdminService_Get
    {
        private TestUnitOfWork _unitOfWork;
        private AdminService _adminService;

        public AdminService_Get()
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
        public void AdminsFanZone_Get_All_2() {
            // Arrange
            var theater1 = new Theater { Name = "Arena" };
            var theater2 = new Theater { Name = "SNP" };
            _unitOfWork.Theaters.Add(theater1);
            _unitOfWork.Theaters.Add(theater2);
            _unitOfWork.SaveChanges();

            _unitOfWork.Users.Add(Utils.CreateFanZoneAdmin(1, theater1.TheaterId));
            _unitOfWork.Users.Add(Utils.CreateFanZoneAdmin(2, theater1.TheaterId));
            _unitOfWork.Users.Add(Utils.CreateTheaterAdmin(3, theater1.TheaterId));
            _unitOfWork.Users.Add(Utils.CreateFanZoneAdmin(4, theater2.TheaterId));
            _unitOfWork.SaveChanges();

            // Act
            var fanZoneAdmins1 = _adminService.GetFanZoneAdmins(theater1.TheaterId);
            var fanZoneAdmins2 = _adminService.GetFanZoneAdmins(theater2.TheaterId);

            // Assert
            Assert.AreEqual(2, fanZoneAdmins1.Count());
            Assert.AreEqual(1, fanZoneAdmins2.Count());
        }

        [TestMethod]
        public void AdminsTheater_Get_All_2()
        {
            // Arrange
            var theater1 = new Theater { Name = "Arena" };
            var theater2 = new Theater { Name = "SNP" };
            _unitOfWork.Theaters.Add(theater1);
            _unitOfWork.Theaters.Add(theater2);
            _unitOfWork.SaveChanges();

            _unitOfWork.Users.Add(Utils.CreateFanZoneAdmin(1, theater1.TheaterId));
            _unitOfWork.Users.Add(Utils.CreateFanZoneAdmin(2, theater1.TheaterId));
            _unitOfWork.Users.Add(Utils.CreateTheaterAdmin(3, theater2.TheaterId));
            _unitOfWork.Users.Add(Utils.CreateTheaterAdmin(4, theater2.TheaterId));
            _unitOfWork.SaveChanges();

            // Act
            var theaterAdmins1 = _adminService.GetTheaterAdmins(theater1.TheaterId);
            var theaterAdmins2 = _adminService.GetTheaterAdmins(theater2.TheaterId);

            // Arrange
            Assert.AreEqual(0, theaterAdmins1.Count());
            Assert.AreEqual(2, theaterAdmins2.Count());
        }

        [TestMethod]
        public void AdminsFanZone_Get_All_3()
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
            _unitOfWork.Users.Add(Utils.CreateTheaterAdmin(4, theater2.TheaterId));
            _unitOfWork.SaveChanges();

            // Act
            var admins1 = _adminService.GetAdmins(theater1.TheaterId);
            var admins2 = _adminService.GetAdmins(theater2.TheaterId);

            // Assert
            Assert.AreEqual(3, admins1.Count());
            Assert.AreEqual(1, admins2.Count());
        }
    }
}
