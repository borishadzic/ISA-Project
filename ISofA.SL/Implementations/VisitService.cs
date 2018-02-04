using ISofA.DAL.Core;
using ISofA.DAL.Core.Domain;
using ISofA.DAL.Core.Pantries;
using ISofA.SL.DTO;
using ISofA.SL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISofA.SL.Implementations
{
    public class VisitService : Service, IVisitService
    {
        public VisitService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IEnumerable<object> GetUserVisits(int theaterId, string userId)
        {
            return UnitOfWork.Pantry<Seat>()
                .Find(x => x.TheaterId == theaterId && x.UserId.Equals(userId)
                && x.Projection.StartTime.AddMinutes(x.Projection.Play.DurationMins) <= DateTime.UtcNow)
                .Select(x => new SpeedSeatListElementDTO(x)); ;
        }
    }
}
