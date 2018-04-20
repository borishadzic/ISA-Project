using ISofA.DAL.Core.Domain;
using ISofA.SL.DTO;
using ISofA.SL.Services;
using ISofA.WebAPI.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ISofA.WebAPI.Controllers
{   
    public class VIPSeatsController : ApiController
    {
        private readonly ISegmentService _segmentService;

        public VIPSeatsController(ISegmentService segmentService)
        {
            _segmentService = segmentService;
        }

        [HttpPost]
        [Route("api/Theaters/{theaterId}/Projections/{projectionId}/VIPSeats")]
        public void CreateVIPSeat(int theaterId, int projectionId, [FromBody]VIPSeatBindingModel seat)
        {
            _segmentService.Create(projectionId, seat);
        }
    }
}
