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

  //      [HttpPost]
		//[Route("api/FriendRequest/send")]
  //      public IHttpActionResult SendFriendRequest([FromBody]string Id)
  //      {
  //          bool salji = _friendRequestService.SendFriendRequest(Id, User.Identity.GetUserId());
  //          if (salji)
  //              return Ok();
  //          else
  //              return BadRequest();

  //      }
		[HttpGet]
		[Route("api/FriendRequests/GetFriendRequests")]
        public IEnumerable<ISofAUserDTO> Get()
        {
            return _friendRequestService.GetFriendRequests(User.Identity.GetUserId());
        }

        [HttpPost]
        [Route("api/FriendRequests/accept")]
        public IHttpActionResult AcceptFriendRequest([FromBody]ISofAUser senderId)
        {
            var potvrda = _friendRequestService.AcceptFriendRequest( User.Identity.GetUserId(), senderId.Id);
            if (potvrda)
                return Ok();
            else
                return BadRequest();
        }

		[HttpPost]
		[Route("api/FriendRequests/sendFriendRequest")]
		public IHttpActionResult SendRequest([FromBody] ISofAUser isauser)
		{
			string Id = isauser.Id;
			bool salji = _friendRequestService.SendFriendRequest(Id, User.Identity.GetUserId());
			if (salji)
				return Ok();
			else
				return BadRequest();
		}

        [HttpPost]
        [Route("api/FriendRequests/decline")]
        public IHttpActionResult DeclineFriendRequest([FromBody]ISofAUser isauser)
        {
			string senderId = isauser.Id;
            var potvrda = _friendRequestService.DeclineFriendRequest(User.Identity.GetUserId(), senderId);
            if (potvrda)
                return Ok();
            else
                return BadRequest();
        }

		[HttpGet]
		[Route("api/FriendRequests/GetMyFriends")]
		public IEnumerable<ISofAUserDTO> GetFriends()
		{
			return _friendRequestService.GetFriends(User.Identity.GetUserId());
		}

		[HttpPost]
		[Route("api/FriendRequests/removeFriend")]
		public bool RemoveFriend([FromBody]ISofAUser UserId)
		{
			return _friendRequestService.RemoveFriend(User.Identity.GetUserId(), UserId.Id);

		}

		

	}
}
