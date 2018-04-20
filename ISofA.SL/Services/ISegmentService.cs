using ISofA.DAL.Core.Domain;
using ISofA.SL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISofA.SL.Services
{
    public interface ISegmentService
    {
        IEnumerable<SpeedSeatListElementDTO> GetDiscountTickets(int theaterId);
        void Create(int projectionId, Seat seat);
        void ReserveDiscountTicket(int projectionId, Seat seat, string userId);
    }
}
