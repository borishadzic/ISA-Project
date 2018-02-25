using ISofA.DAL.Core.Domain;
using ISofA.DAL.Core.Pantries;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ISofA.DAL.Persistence.Pantries
{
    public class TheaterPantry : Pantry<Theater>, ITheaterPantry
    {
        public TheaterPantry(ISofADbContext context) : base(context)
        {
        }

        public IEnumerable<ISofAUser> GetTheaterAdmins(int theaterId)
        {
            return Context.Theaters
                .Where(t => t.TheaterId == theaterId)
                .Include(t => t.TheaterAdmins)
                .SelectMany(t => t.TheaterAdmins)
                .ToList();
        }

        public IEnumerable<ISofAUser> GetTheaterFanZoneAdmins(int theaterId)
        {
            return Context.Theaters
                .Where(t => t.TheaterId == theaterId)
                .Include(t => t.FanZoneAdmins)
                .SelectMany(t => t.FanZoneAdmins)
                .ToList();
        }

        public Theater GetTheaterWithAdmins(int theaterId)
        {
            return Context.Theaters
                .Where(t => t.TheaterId == theaterId)
                .Include(t => t.TheaterAdmins)
                .FirstOrDefault();
        }

        public Theater GetTheaterWithFanZoneAdmins(int theaterId)
        {
            return Context.Theaters
                .Where(t => t.TheaterId == theaterId)
                .Include(t => t.FanZoneAdmins)
                .FirstOrDefault();
        }
    }
}
