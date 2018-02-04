using ISofA.DAL.Core;
using ISofA.DAL.Core.Domain;
using ISofA.DAL.Core.Pantries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISofA.DAL.Persistence.Pantries
{
    public class SeatPantry : Pantry<Seat>, ISeatPantry
    {
        public SeatPantry(IISofADbContext context) : base(context)
        {
        }
    }
}
