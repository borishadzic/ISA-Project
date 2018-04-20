using ISofA.DAL.Core.Domain;
using ISofA.SL.DTO;
using ISofA.SL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ISofA.WebAPI.Controllers
{   
    public class DiscountTicketsController : ApiController
    {
        private readonly ISegmentService _segmentService;

        public DiscountTicketsController(ISegmentService segmentService)
        {
            _segmentService = segmentService;
        }

        [HttpGet]
        [Route("api/Theaters/{theaterId}/DiscountTickets")]
        public IEnumerable<SpeedSeatListElementDTO> GetDiscountTickets(int theaterId)
        {
            return _segmentService.GetDiscountTickets(theaterId);
        }

        [HttpPost]
        [Route("api/Theaters/{theaterId}/Projections/{projectionId}/DiscountTickets")]
        public void CreateDiscountTicket(int theaterId, int projectionId, [FromBody]Seat seat)
        {
            _segmentService.Create(projectionId, seat);
        }

        [HttpPut]
        [Route("api/Theaters/{theaterId}/Projections/{projectionId}/DiscountTickets")]
        public void ReserveDiscountTicket(int theaterId, int projectionId, [FromBody]Seat seat)
        {
            _segmentService.ReserveDiscountTicket(projectionId, seat);
        }
    }
}
