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
    public class SeatService : Service, ISeatService
    {
        public SeatService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }


        public IEnumerable<SeatDTO> GetProjectionSeats(int projectionId)
        {
            Projection proj = UnitOfWork.Projections.Find(x => x.ProjectionId == projectionId).FirstOrDefault();
            
            return UnitOfWork.Seats
                .Find(x => x.TheaterId == proj.TheaterId && x.PlayId == proj.PlayId && x.StageId == proj.StageId && x.ProjectionId == projectionId)
                .Select(x => new SeatDTO(x));
        }

        public IEnumerable<SpeedSeatListElementDTO> GetSpeedSeats(int theaterId)
        {
            return UnitOfWork.Seats
                .Find(x => x.TheaterId == theaterId && x.State == SeatState.Speed)
                .Select(x => new SpeedSeatListElementDTO(x));
        }

        public SeatDTO AddReservation(int theaterId, int playId, int stageId, int projectionId, string userId, Seat seat)
        {
            Seat speedSeat = UnitOfWork.Seats.Get(theaterId, playId, stageId, projectionId, seat.SeatRow, seat.SeatColumn);
            if (speedSeat == null)
            {
                seat.TheaterId = theaterId;
                seat.PlayId = playId;
                seat.StageId = stageId;
                seat.ProjectionId = projectionId;
                seat.UserId = userId;
                seat.Discount = 0;
                seat.State = SeatState.Reserved;
                seat = UnitOfWork.Seats.Add(seat);
                UnitOfWork.SaveChanges();
                return new SeatDTO(seat);
            }
            else if (speedSeat.UserId == null && speedSeat.State == SeatState.Speed)
            {
                speedSeat.UserId = userId;
                speedSeat.State = SeatState.Reserved;
                UnitOfWork.Modified(speedSeat);
                UnitOfWork.SaveChanges();
                return new SeatDTO(speedSeat);
            }
            else
            {
                // TODO: Throw errors
            }
            return null;
        }
		public IEnumerable<SeatDTO> AddMultipleReservations(int theaterId, int projectionId, string userIds, IEnumerable<Seat> seats)
		{

			var proj = UnitOfWork.Projections.Get(projectionId);


			foreach (Seat seat in seats)
			{
				Seat taken = UnitOfWork.Seats.Find(x => x.ProjectionId == proj.ProjectionId && x.SeatColumn == seat.SeatColumn && x.SeatRow == seat.SeatRow).FirstOrDefault();
				if (taken != null && (taken.State == SeatState.Reserved || taken.State == SeatState.Bought))
				{
					throw new Exception();
				}
			}
			foreach (Seat seat in seats)
			{
				seat.TheaterId = theaterId;
				seat.PlayId = proj.PlayId;
				seat.StageId = proj.StageId;
				seat.ProjectionId = projectionId;
				seat.UserId = userIds;
				seat.Discount = 0;
				seat.State = SeatState.Reserved;
			}

			var seats1 = UnitOfWork.Seats.AddRange(seats);
			UnitOfWork.SaveChanges();

			return UnitOfWork.Seats
			   .Find(x => x.TheaterId == theaterId && x.PlayId == proj.PlayId && x.StageId == proj.StageId && x.ProjectionId == projectionId && x.UserId == userIds)
			   .Select(x => new SeatDTO(x));

		}

		public IEnumerable<SeatDTO> GetUserReservations(string userId)
		{
			return UnitOfWork.Seats.Find(x => x.UserId == userId).Select(x => new SeatDTO(x));
		}

		public SeatDTO AddSpeedSeat(int theaterId, int playId, int stageId, int projectionId, Seat seat)
        {
            if (UnitOfWork.Seats.Get(theaterId, playId, stageId, projectionId, seat.SeatRow, seat.SeatColumn) != null)
                return null; // TODO: ERROR Throw             
            seat.TheaterId = theaterId;
            seat.PlayId = playId;
            seat.StageId = stageId;
            seat.ProjectionId = projectionId;
            seat.UserId = null;
            seat.State = SeatState.Speed;
            seat = UnitOfWork.Seats.Add(seat);
            UnitOfWork.SaveChanges();
            return new SeatDTO(seat);
        }

        public SeatDTO AddVIPSeat(int theaterId, int playId, int stageId, int projectionId, Seat seat)
        {
            if (UnitOfWork.Seats.Get(theaterId, playId, stageId, projectionId, seat.SeatRow, seat.SeatColumn) != null)
                return null; // TODO: ERROR Throw             
            seat.TheaterId = theaterId;
            seat.PlayId = playId;
            seat.StageId = stageId;
            seat.ProjectionId = projectionId;
            seat.UserId = null;
            seat.State = SeatState.Speed;
            seat = UnitOfWork.Seats.Add(seat);
            UnitOfWork.SaveChanges();
            return new SeatDTO(seat);
        }

        public void CancelReservation(int theaterId, int playId, int stageId, int projectionId, string userId, int seatRow, int seatColumn)
        {
            Seat seat = UnitOfWork.Seats.Get(theaterId, playId, stageId, projectionId, seatRow, seatColumn);
            if (seat == null)
                return; // TODO: ERROR Throw
            if (seat.UserId == null || !seat.UserId.Equals(userId))
                return; // TODO: ERROR Throw
            if (seat.State == SeatState.Bought)
                return; // TODO: ERROR Throw
            if (seat.Discount > 0)
            { // TODO: LOW Should return to speed state?
                seat.State = SeatState.Speed;
                seat.UserId = null;
                UnitOfWork.Modified(seat);
            }
            else
            {
                UnitOfWork.Seats.Remove(seat);
            }
            UnitOfWork.SaveChanges();
        }

        public void RemoveSeat(int theaterId, int playId, int stageId, int projectionId, int seatRow, int seatColumn)
        {
            Seat seat = UnitOfWork.Seats.Get(theaterId, playId, stageId, projectionId, seatRow, seatColumn);
            if (seat == null)
                return; // TODO: ERROR Throw
            if (seat.UserId != null)
                return; // TODO: ERROR Throw
            UnitOfWork.Seats.Remove(seat);
            UnitOfWork.SaveChanges();
        }
    }
}