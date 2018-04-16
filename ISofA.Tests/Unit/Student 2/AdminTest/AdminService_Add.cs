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
    public class AdminService_Add
    {
        private TestUnitOfWork _unitOfWork;
        private AdminService _adminService;

        public AdminService_Add()
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
        public void AdminsFanZone_Add_2() {
            // Arrange
            var theater = new Theater { Name = "Arena" };
            _unitOfWork.Theaters.Add(theater);

            var user1 = Utils.GetTestUser(1);
            var user2 = Utils.GetTestUser(2);
            var user3 = Utils.GetTestUser(3);
            _unitOfWork.Users.AddRange(new ISofAUser[] { user1, user2, user3 });
            _unitOfWork.SaveChanges();

            // Act
            _adminService.AddFanZoneAdmin(theater.TheaterId, user1);
            _adminService.AddFanZoneAdmin(theater.TheaterId, user2);
            _adminService.AddTheaterAdmin(theater.TheaterId, user3);

            // Assert
            var fanZoneAdmins = _unitOfWork.Users.Find(x => x.AdminOfTheaterId == theater.TheaterId 
                                                         && x.ISofAUserRole == ISofAUserRole.FanZoneAdmin);

            Assert.AreEqual(2, fanZoneAdmins.Count());
        }

        [TestMethod]
        public void AdminsFanZone_Add_1()
        {
            // Arrange
            var theater1 = new Theater { Name = "Arena" };
            var theater2 = new Theater { Name = "SNP" };
            _unitOfWork.Theaters.Add(theater1);

            var user1 = Utils.GetTestUser(1);
            var user2 = Utils.GetTestUser(2);
            var user3 = Utils.GetTestUser(3);
            _unitOfWork.Users.AddRange(new ISofAUser[] { user1, user2, user3 });
            _unitOfWork.SaveChanges();

            // Act
            _adminService.AddFanZoneAdmin(theater1.TheaterId, user1);
            _adminService.AddTheaterAdmin(theater1.TheaterId, user2);
            _adminService.AddFanZoneAdmin(theater2.TheaterId, user3);

            // Assert
            var fanZoneAdmins = _unitOfWork.Users.Find(x => x.AdminOfTheaterId == theater1.TheaterId
                                                         && x.ISofAUserRole == ISofAUserRole.FanZoneAdmin);

            Assert.AreEqual(1, fanZoneAdmins.Count());
        }

        [TestMethod]
        public void AdminsTheater_Add_1()
        {
            // Arrange
            var theater1 = new Theater { Name = "Arena" };
            var theater2 = new Theater { Name = "SNP" };
            _unitOfWork.Theaters.Add(theater1);

            var user1 = Utils.GetTestUser(1);
            var user2 = Utils.GetTestUser(2);
            var user3 = Utils.GetTestUser(3);
            _unitOfWork.Users.AddRange(new ISofAUser[] { user1, user2, user3 });
            _unitOfWork.SaveChanges();

            // Act
            _adminService.AddFanZoneAdmin(theater1.TheaterId, user1);
            _adminService.AddTheaterAdmin(theater1.TheaterId, user2);
            _adminService.AddFanZoneAdmin(theater2.TheaterId, user3);

            // Assert
            var fanZoneAdmins = _unitOfWork.Users.Find(x => x.AdminOfTheaterId == theater1.TheaterId
                                                         && x.ISofAUserRole == ISofAUserRole.TheaterAdmin);

            Assert.AreEqual(1, fanZoneAdmins.Count());
        }
    }
}
