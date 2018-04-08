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

        public Theater GetTheaterWithAdmins(int theaterId)
        {
            return Context.Theaters
                .Where(t => t.TheaterId == theaterId)
                .Include(t => t.Admins)
                .FirstOrDefault();
        }
    }
}
