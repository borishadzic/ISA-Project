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
        IEnumerable<SeatDTO> GetProjectionSeats(int projectionId);
        IEnumerable<SpeedSeatListElementDTO> GetSpeedSeats(int theaterId);

        SeatDTO AddSpeedSeat(int projectionId, Seat seat);
        SeatDTO AddVIPSeat(int theaterId, int playId, int stageId, int projectionId, Seat seat);
        SeatDTO AddReservation(int theaterId, int playId, int stageId, int projectionId, string userId, Seat seat);

        void CancelReservation(int theaterId, int playId, int stageId, int projectionId, string userId, int seatRow, int seatColumn);        
        void RemoveSeat(int theaterId, int playId, int stageId, int projectionId, int seatRow, int seatColumn);
		IEnumerable<SeatDTO> AddMultipleReservations(int theaterId, int projectionId, string userIds, IEnumerable<Seat> seats);
		IEnumerable<SeatDTO> GetUserReservations(string userId);
	}
}