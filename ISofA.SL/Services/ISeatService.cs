using ISofA.DAL.Core.Domain;
using ISofA.SL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISofA.SL.Services
{
    public interface ISeatService
    {
        IEnumerable<SeatDTO> GetProjectionSeats(int theaterId, int playId, int stageId, int projectionId);
        SeatDTO SetSpeedSeat(int theaterId, int playId, int stageId, int projectionId, Seat seat);
        SeatDTO SetVIPSeat(int theaterId, int playId, int stageId, int projectionId, int seatRow, int seatColumn);
        SeatDTO SetReservedSeat(int theaterId, int playId, int stageId, int projectionId, int seatRow, int seatColumn);
        void CancelReservation(int theaterId, int playId, int stageId, int projectionId, int seatRow, int seatColumn);
        void RemoveSeat(int theaterId, int playId, int stageId, int projectionId, int seatRow, int seatColumn);

    }
}