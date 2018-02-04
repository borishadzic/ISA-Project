using ISofA.DAL.Core.Domain;
using ISofA.SL.DTO;
using ISofA.SL.Services;
using ISofA.WebAPI.Authorization;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ISofA.WebAPI.Controllers
{
    [Authorize]
    public class SeatsController : ApiController
    {
        private readonly ISeatService _seatService;

        public SeatsController(ISeatService seatService)
        {
            _seatService = seatService;
        }

        [HttpGet]
        [Route("api/Theaters/{theaterId}/Speed")]
        public IEnumerable<SpeedSeatListElementDTO> GetSpeedSeats(int theaterId)
        {
            return _seatService.GetSpeedSeats(theaterId);
        }



        [HttpGet]
        [Route("api/Theaters/{theaterId}/Visits")]
        public IEnumerable<SpeedSeatListElementDTO> GetUserVisits(int theaterId)
        {

        }

        [HttpGet]
        [Route("api/Theaters/{theaterId}/Projections/{projectionId}/Seats")]
        public IEnumerable<SeatDTO> GetProjectionSeats(int theaterId, int playId, int stageId, int projectionId) // TODO: LOW Da li je potreban getter za sva sedista?
        {
            return _seatService.GetProjectionSeats(theaterId, playId, stageId, projectionId);
        }

        [HttpPost]
        [Route("api/Theaters/{theaterId}/Projections/{projectionId}/Seats")]
        public SeatDTO AddReservation(int theaterId, int playId, int stageId, int projectionId, [FromBody]Seat seat)
        {
            return _seatService.AddReservation(theaterId, playId, stageId, projectionId, User.Identity.GetUserId(), seat);
        }

        [HttpPost]
        [Route("api/Theaters/{theaterId}/Projections/{projectionId}/Seats/Speed")]
        public SeatDTO AddSpeedSeat(int theaterId, int playId, int stageId, int projectionId, [FromBody]Seat seat)
        {
            return _seatService.AddSpeedSeat(theaterId, playId, stageId, projectionId, seat);
        }

        [HttpPost]
        [Route("api/Theaters/{theaterId}/Projections/{projectionId}/Seats/VIP")]
        public SeatDTO AddVIPSeat(int theaterId, int playId, int stageId, int projectionId, [FromBody]Seat seat)
        {
            return _seatService.AddVIPSeat(theaterId, playId, stageId, projectionId, seat);
        }

        [HttpDelete]
        [Route("api/Theaters/{theaterId}/Projections/{projectionId}/Seats")]
        public void CancelReservation(int theaterId, int playId, int stageId, int projectionId, int seatRow, int seatColumn)
        {
            _seatService.CancelReservation(theaterId, playId, stageId, projectionId, User.Identity.GetUserId(), seatRow, seatColumn);
        }

        [HttpDelete]
        [Route("api/Theaters/{theaterId}/Projections/{projectionId}/Seats/Delete")] // TODO: LOW Seperate route for speed and vip seats?
        // TODO: AUTH THEATER ADMIN
        public void RemoveSeat(int theaterId, int playId, int stageId, int projectionId, int seatRow, int seatColumn)
        {
            _seatService.RemoveSeat(theaterId, playId, stageId, projectionId, seatRow, seatColumn);
        }

    }
}
