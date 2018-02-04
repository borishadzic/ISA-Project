using ISofA.DAL.Core.Domain;
using ISofA.SL.DTO;
using ISofA.SL.Services;
using ISofA.WebAPI.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ISofA.WebAPI.Controllers
{
    public class SeatsController : ApiController
    {
        private readonly ISeatService _seatService;

        public SeatsController(ISeatService seatService)
        {
            _seatService = seatService;
        }

        [Route("api/Theaters/{theaterId}/Projections/{projectionId}/Seats")]
        public IEnumerable<SeatDTO> Get(int theaterId, int playId, int stageId, int projectionId) // TODO: LOW Da li je potreban getter za sva sedista?
        {
            return _seatService.GetProjectionSeats(theaterId, playId, stageId, projectionId);
        }        

        [Route("api/Theaters/{theaterId}/Projections/{projectionId}/Seats")]
        public SeatDTO ReserveSeat(int theaterId, int playId, int stageId, int projectionId, [FromBody]Seat seat)
        {
            return _seatService.SetReservedSeat(theaterId, playId, stageId, projectionId, seat.SeatColumn, seat.SeatColumn);
        }

        [Route("api/Theaters/{theaterId}/Projections/{projectionId}/Seats/Speed")]
        public SeatDTO AddSpeedSeat(int theaterId, int playId, int stageId, int projectionId, [FromBody]Seat seat)
        {
            return _seatService.SetSpeedSeat(theaterId, playId, stageId, projectionId, seat);
        }

        [Route("api/Theaters/{theaterId}/Projections/{projectionId}/Seats/VIP")]
        public SeatDTO AddVIPSeat(int theaterId, int playId, int stageId, int projectionId, [FromBody]Seat seat)
        {
            return _seatService.SetVIPSeat(theaterId, playId, stageId, projectionId, seat.SeatColumn, seat.SeatColumn);
        }

        [HttpDelete]
        [Route("api/Theaters/{theaterId}/Projections/{projectionId}/Seats")]
        public void UnreserveSeat(int theaterId, int playId, int stageId, int projectionId, int seatRow, int seatColumn)
        {
            _seatService.CancelReservation(theaterId, playId, stageId, projectionId, seatRow, seatColumn);
        }

        [HttpDelete]
        [Route("api/Theaters/{theaterId}/Projections/{projectionId}/Seats/Speed")]
        public void RemoveSeat(int theaterId, int playId, int stageId, int projectionId, int seatRow, int seatColumn)
        {
            _seatService.CancelReservation(theaterId, playId, stageId, projectionId, seatRow, seatColumn);
        }

    }
}
