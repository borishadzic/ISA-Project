using ISofA.DAL.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISofA.DAL.Core.Pantries
{
    public interface ITheaterPantry
    {
        IEnumerable<Theater> GetAll();
        Theater Get(int theaterId);
        Theater Add(Theater theater);
    }
}
