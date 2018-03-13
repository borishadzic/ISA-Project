﻿using ISofA.DAL.Core.Domain;
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
    [RoutePrefix("api/UserItems/{userItemId:guid}/bids")]
    public class BidsController : ApiController
    {
        private readonly IBidService _bidService;

        public BidsController(IBidService bidService)
        {
            _bidService = bidService;
        }

        [Route("")]
        public IEnumerable<Bid> Get(Guid userItemId)
        {
            return _bidService.GetAll(userItemId);
        }

        [Route("{bidderId}")]
        public Bid Get(Guid userItemId, string bidderId)
        {
            var bid = _bidService.Get(userItemId, bidderId);

            if (bid == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return bid;
        }

        [Route("")]
        public Bid Post(Guid userItemId, Bid bid)
        {
            var adddedBid = _bidService.AddBid(userItemId, User.Identity.GetUserId(), bid);

            if (adddedBid == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            return adddedBid;
        }
    }
}
