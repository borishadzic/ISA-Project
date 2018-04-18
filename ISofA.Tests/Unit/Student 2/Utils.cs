using ISofA.DAL.Core.Domain;
using ISofA.Tests.Persistence;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISofA.Tests.Unit.Student_2
{
    public class Utils
    {
        public static ISofAUser GetTestUser(int id)
        {
            return new ISofAUser
            {
                Name = $"Test User {id}",
                Email = $"test{id}@test.com",
                EmailConfirmed = true,
                UserName = $"Test User {id}"
            };
        }

        public static ISofAUser CreateFanZoneAdmin(int id, int theaterId)
        {
            var user = GetTestUser(id);
            user.AdminOfTheaterId = theaterId;
            user.ISofAUserRole = ISofAUserRole.FanZoneAdmin;
            return user;
        }

        public static ISofAUser CreateTheaterAdmin(int id, int theaterId)
        {
            var user = GetTestUser(id);
            user.AdminOfTheaterId = theaterId;
            user.ISofAUserRole = ISofAUserRole.TheaterAdmin;
            return user;
        }

        public static UserManager<ISofAUser> GetUserManager()
        {
            var store = new UserStore<ISofAUser>(new ISofATestDbContext("ISofATestDb"));
            return new UserManager<ISofAUser>(store);
        }
    }
}
