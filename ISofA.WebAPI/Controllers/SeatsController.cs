using ISofA.DAL.Core.Domain;
using ISofA.SL.DTO;
using ISofA.SL.Services;
using ISofA.WebAPI.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
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

		[HttpPost]
		[Route("api/Theaters/{theaterId}/Projections/{projectionId}/ReserveSeats")]
		public IEnumerable<SeatDTO> AddMultipleReservations(int theaterId, int projectionId, [FromBody]IEnumerable<Seat> seat)
		{
			return _seatService.AddMultipleReservations(theaterId, projectionId, User.Identity.GetUserId(), seat);
		}

		[HttpGet]
		[Route("api/Profile/myReservations")]
		public IEnumerable<SeatDTO> GetUserReservations()
		{
			return _seatService.GetUserReservations(User.Identity.GetUserId());
		}

		[HttpGet]
        [Route("api/Theaters/{theaterId}/Visits")]
        public IEnumerable<SpeedSeatListElementDTO> GetUserVisits(int theaterId)
        {
            return null;
        }

        [HttpGet]
        [Route("api/Theaters/Projections/{projectionId}/Seats")]
        public IEnumerable<SeatDTO> GetProjectionSeats(int projectionId) // TODO: LOW Da li je potreban getter za sva sedista?
        {
            return _seatService.GetProjectionSeats(projectionId);
        }

        [HttpPost]
        [Route("api/Theaters/{theaterId}/Projections/{projectionId}/Seats")]
        public SeatDTO AddReservation(int theaterId, int playId, int stageId, int projectionId, [FromBody]Seat seat)
        {
            return _seatService.AddReservation(theaterId, playId, stageId, projectionId, User.Identity.GetUserId(), seat);
        }

        [HttpPost]
        [Route("api/Theaters/{theaterId}/Projections/{projectionId}/Seats/Speed")]
        public SeatDTO AddSpeedSeat(int theaterId, int projectionId, [FromBody]Seat seat)
        {
            return _seatService.AddSpeedSeat(projectionId, seat);
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

        [AllowAnonymous]
        [HttpGet]
        [Route("api/FriendReservations/ConfirmInvite/{userId}/{ProjectionId}/{SeatRow}/{SeatColumn}")]
        public void AcceptFriendInvatation(string userId, int ProjectionId, int SeatRow, int SeatColumn)
        {
            _seatService.ConfirmFriendInvatation(userId, ProjectionId, SeatRow, SeatColumn);
        }


        [AllowAnonymous]
        [HttpGet]
        [Route("api/FriendReservations/DeclineInvite/{ProjectionId}/{SeatRow}/{SeatColumn}")]
        public void DeclineFriendInvatation(int ProjectionId, int SeatRow, int SeatColumn)
        {
            _seatService.DeclineFriendInvatation(ProjectionId, SeatRow, SeatColumn);
        }

        [HttpPost]
        [Route("api/Seats/SendInvatations")]
        public async Task SendInvatations(InvatationBindingModel invite)
        {
            await _seatService.InviteFriendsToMovies(invite.users, invite.projectionIds, invite.rows, invite.columns);
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
