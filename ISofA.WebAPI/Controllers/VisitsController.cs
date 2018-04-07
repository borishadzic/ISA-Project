using ISofA.SL.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace ISofA.WebAPI.Controllers
{
    [Authorize]
    public class VisitsController : ApiController
    {
        private readonly IVisitService _visitService;

        public VisitsController(IVisitService visitService)
        {
            _visitService = visitService;
        }

        [HttpGet]
        [Route("api/Theaters/{theaterId}/Visits")]
        public IEnumerable<Object> GetUserVisits(int theaterId)
        {
            return _visitService.GetUserVisits(theaterId, User.Identity.GetUserId());
        }        


    }
}
