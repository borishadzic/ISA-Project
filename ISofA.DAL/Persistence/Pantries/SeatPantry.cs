using ISofA.DAL.Core;
using ISofA.DAL.Core.Domain;
using ISofA.DAL.Core.Pantries;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISofA.DAL.Persistence.Pantries
{
    public class SeatPantry : Pantry<Seat>, ISeatPantry
    {
        public SeatPantry(ISofADbContext context) : base(context)
        {
        }

        public IEnumerable<Seat> GetSpeedSeats(int theaterId, int workStart)
        {
            var theater = Context.Theaters.Find(theaterId);
            var startTime = DateTime.Today.AddHours(workStart / 60).AddMinutes(workStart % 60);
            return Context.Seats
                .Include(x => x.Play)
                .Include(x => x.Projection)
                .Include(x => x.Stage)
                .Where(x => x.TheaterId == theaterId && x.State == SeatState.Speed && x.Projection.StartTime >= startTime);
        }
    }
}
