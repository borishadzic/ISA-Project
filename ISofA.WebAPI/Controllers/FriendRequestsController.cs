using ISofA.SL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using ISofA.DAL.Core.Domain;
using ISofA.SL.DTO;

namespace ISofA.WebAPI.Controllers
{
    [Authorize]
    public class FriendRequestsController : ApiController
    {
        private IFriendRequestService _friendRequestService;
        public FriendRequestsController(IFriendRequestService _friendRequestService)
        {
            this._friendRequestService = _friendRequestService;
        }

        [HttpPost]
        public IHttpActionResult SendFriendRequest(string Id)
        {
            bool salji = _friendRequestService.SendFriendRequest(Id, User.Identity.GetUserId());
            if (salji)
                return Ok();
            else
                return BadRequest();

        }

        public IEnumerable<ISofAUserDTO> Get()
        {
            return _friendRequestService.GetFriendRequests(User.Identity.GetUserId());
        }

        [HttpPost]
        [Route("api/FriendRequests/{senderId}/accept")]
        public IHttpActionResult AcceptFriendRequest(string senderId)
        {
            var potvrda = _friendRequestService.AcceptFriendRequest( User.Identity.GetUserId(), senderId);
            if (potvrda)
                return Ok();
            else
                return BadRequest();
        }

        [HttpPost]
        [Route("api/FriendRequests/{senderId}/decline")]
        public IHttpActionResult DeclineFriendRequest(string senderId)
        {
            var potvrda = _friendRequestService.DeclineFriendRequest(User.Identity.GetUserId(), senderId);
            if (potvrda)
                return Ok();
            else
                return BadRequest();
        }

		[HttpGet]
		[Route("api/Friends")]
		public IEnumerable<ISofAUserDTO> GetFriends()
		{
			return _friendRequestService.GetFriends(User.Identity.GetUserId());
		}

		[HttpPost]
		[Route("api/Friends/{UserId}/remove")]
		public bool RemoveFriend(string UserId)
		{
			return _friendRequestService.RemoveFriend(User.Identity.GetUserId(), UserId);

		}

	}
}
