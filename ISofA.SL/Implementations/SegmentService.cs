using ISofA.DAL.Core;
using ISofA.DAL.Core.Domain;
using ISofA.SL.DTO;
using ISofA.SL.Exceptions;
using ISofA.SL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ISofA.SL.Implementations
{
    public class SegmentService : Service, ISegmentService
    {
        private readonly ClaimsIdentity _identity;

        public SegmentService(IUnitOfWork unitOfWork, IIdentity identity) : base(unitOfWork)
        {
            _identity = (ClaimsIdentity)identity;
        }

        public void Create(int projectionId, Seat seat)
        {
            var projection = UnitOfWork.Projections.Get(projectionId);

            if (projection == null)
                throw new ProjectionNotFoundException(projectionId);

            var discountTicket = UnitOfWork.Seats.Get(projectionId, seat.SeatRow, seat.SeatColumn);
            if (discountTicket != null)
                throw new BadRequestException("Bad Request");

            seat.TheaterId = projection.TheaterId;
            seat.PlayId = projection.PlayId;
            seat.StageId = projection.StageId;
            seat.ProjectionId = projectionId;
            seat = UnitOfWork.Seats.Add(seat);
            UnitOfWork.SaveChanges();
        }

        public IEnumerable<SpeedSeatListElementDTO> GetDiscountTickets(int theaterId)
        {
            return UnitOfWork.Seats
                .GetSpeedSeats(theaterId)
                .Select(x => new SpeedSeatListElementDTO(x));
        }

        public void ReserveDiscountTicket(int projectionId, Seat seat)
        {
            var projection = UnitOfWork.Projections.Get(projectionId);

            if (projection == null)
                throw new ProjectionNotFoundException(projectionId);

            var discountTicket = UnitOfWork.Seats.Get(projectionId, seat.SeatRow, seat.SeatColumn);
            if (discountTicket == null)
                throw new BadRequestException("Bad Request");

            if (discountTicket.State != SeatState.Speed && discountTicket.UserId != null)
                throw new BadRequestException("BadRequest");

            discountTicket.UserId = null; // todo identity apply

            UnitOfWork.Modified(discountTicket);

        }
    }
}
