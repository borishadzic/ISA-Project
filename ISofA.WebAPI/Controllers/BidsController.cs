using ISofA.DAL.Core.Domain;
using ISofA.SL.DTO;
using ISofA.SL.Services;
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
    [RoutePrefix("api/UserItems/{userItemId:guid}/bids")]
    public class BidsController : ApiController
    {
        private readonly IBidService _bidService;

        public BidsController(IBidService bidService)
        {
            _bidService = bidService;
        }

        [Route("")]
        public IEnumerable<BidDTO> Get(Guid userItemId)
        {
            return _bidService.GetAll(userItemId);
        }

        [Route("{bidderId}")]
        public BidDTO Get(Guid userItemId, string bidderId)
        {
            var bid = _bidService.Get(userItemId, bidderId);

            if (bid == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return bid;
        }

        [Route("")]
        public UserItemDetailDTO Post(Guid userItemId, Bid bid)
        {
            var userItem = _bidService.AddBid(userItemId, User.Identity.GetUserId(), bid);

            if (userItem == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            return userItem;
        }

        [Route("{bidderId}")]
        public UserItemDTO Post(Guid userItemId, string bidderId)
        {
            var userItem = _bidService.SellItem(User.Identity.GetUserId(), userItemId, bidderId);

            if (userItem == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            return userItem;
        }
    }
}
