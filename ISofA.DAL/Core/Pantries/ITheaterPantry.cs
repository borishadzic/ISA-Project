using ISofA.DAL.Core.Domain;
using System.Collections.Generic;

namespace ISofA.DAL.Core.Pantries
{
    public interface ITheaterPantry : IPantry<Theater>
    {
        Theater GetTheaterWithAdmins(int theaterId);
        Theater GetTheaterWithFanZoneAdmins(int theaterId);
        IEnumerable<ISofAUser> GetTheaterAdmins(int theaterId);
        IEnumerable<ISofAUser> GetTheaterFanZoneAdmins(int theaterId);
    }
}
