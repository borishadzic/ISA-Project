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

        public SeatDTO ReserveSeat(int theaterId, int playId, int stageId, int projectionId, Seat seat)
        {
            // TODO: ERROR Row and Col in range
            seat.TheaterId = theaterId;
            seat.PlayId = playId;
            seat.StageId = stageId;
            seat.ProjectionId = projectionId;
            seat.Discount = 0;
            seat.State = SeatState.Reserved;
            seat = UnitOfWork.Pantry<Seat>().Add(seat);
            UnitOfWork.SaveChanges();

            return new SeatDTO(seat);
        }

        public IEnumerable<SeatDTO> GetProjectionSeats(int theaterId, int playId, int stageId, int projectionId)
        {
            return UnitOfWork.Pantry<Seat>().Find(x => x.TheaterId == theaterId && x.PlayId == playId && x.StageId == stageId && x.ProjectionId == projectionId)
                .Select(x => new SeatDTO(x));
        }        

        public SeatDTO SetSpeedSeat(int theaterId, int playId, int stageId, int projectionId, Seat seat)
        {
            seat.TheaterId = theaterId;
            seat.PlayId = playId;
            seat.StageId = stageId;
            seat.ProjectionId = projectionId;
            seat.State = SeatState.Speed;
            seat = UnitOfWork.Pantry<Seat>().Add(seat);
            UnitOfWork.SaveChanges();

            return new SeatDTO(seat);
        }

        public SeatDTO SetVIPSeat(int theaterId, int playId, int stageId, int projectionId, int seatRow, int seatColumn)
        {
            // TODO: ERROR Throw if seat reserved
            Seat seat = new Seat()
            {
                TheaterId = theaterId,
                PlayId = playId,
                StageId = stageId,
                ProjectionId = projectionId,
                SeatRow = seatRow,
                SeatColumn = seatColumn,
                State = SeatState.VIP
            };

            seat = UnitOfWork.Pantry<Seat>().Add(seat);
            UnitOfWork.SaveChanges();

            return new SeatDTO(seat);
        }

        public SeatDTO SetReservedSeat(int theaterId, int playId, int stageId, int projectionId, int seatRow, int seatColumn)
        {
            Seat seat = UnitOfWork.Pantry<Seat>().Get(theaterId, playId, stageId, projectionId, seatRow, seatColumn);
            if (seat == null)
            {
                seat = new Seat()
                {
                    TheaterId = theaterId,
                    PlayId = playId,
                    StageId = stageId,
                    ProjectionId = projectionId,
                    SeatRow = seatRow,
                    SeatColumn = seatColumn,
                    State = SeatState.Reserved
                };

                seat = UnitOfWork.Pantry<Seat>().Add(seat);
                UnitOfWork.SaveChanges();
            }

            if (seat.State == SeatState.Speed)
            {
                seat.State = SeatState.SpeedReserved;
                UnitOfWork.Modified(seat);
                UnitOfWork.SaveChanges();
            }
            else
            {
                // TODO: ERROR invalid seat state
            }

            return new SeatDTO(seat);
        }

        public void CancelReservation(int theaterId, int playId, int stageId, int projectionId, int seatRow, int seatColumn)
        {
            Seat seat = UnitOfWork.Pantry<Seat>().Get(theaterId, playId, stageId, projectionId, seatRow, seatColumn);

            if (seat == null)
                return; // TODO: ERROR Throw Not Found

            if (seat.State == SeatState.SpeedReserved) // TODO: LOW Can unreserve speed seat?
            {
                seat.State = SeatState.Speed;
                UnitOfWork.Modified(seat);
                UnitOfWork.SaveChanges();
            } else if (seat.State == SeatState.Reserved) // TODO: LOW Can delete speed seat?
            {
                UnitOfWork.Pantry<Seat>().Remove(seat);
                UnitOfWork.SaveChanges();
            }            

            // TODO: ERROR wrong seat
        }

        public void RemoveSeat(int theaterId, int playId, int stageId, int projectionId, int seatRow, int seatColumn)
        {
            Seat seat = UnitOfWork.Pantry<Seat>().Get(theaterId, playId, stageId, projectionId, seatRow, seatColumn);

            if (seat == null)
                return; // TODO: ERROR Throw Not Found

            
            if (seat.State == SeatState.VIP || seat.State == SeatState.Speed) // TODO: LOW Can delete speed seat?
            {
                UnitOfWork.Pantry<Seat>().Remove(seat);
                UnitOfWork.SaveChanges();
            }

            // TODO: ERROR wrong seat
        }
    }
}
