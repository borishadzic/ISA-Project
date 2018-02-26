using ISofA.SL.DTO;
using ISofA.SL.Services;
using ISofA.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ISofA.WebAPI.Controllers
{
    [RoutePrefix("api/Theaters/{theaterId}")]
    public class TheaterAdminsController : ApiController
    {
        // TODO: Dodaj ogranicenje da samo administratori sistema mogu da dodaju nove admine theatra
        private readonly ITheaterAdminService _theaterAdminService;

        public TheaterAdminsController(ITheaterAdminService theaterAdminService)
        {
            _theaterAdminService = theaterAdminService;
        }

        [Route("Admins")]
        public IEnumerable<ISofAUserDTO> Get(int theaterId)
        {
            return _theaterAdminService.GetTheaterAdmins(theaterId);
        }

        [Route("Admins")]
        public IHttpActionResult Post(int theaterId, AdminModel theaterAdmin)
        {
            var admin = _theaterAdminService.AddTheaterAdmin(theaterId, theaterAdmin.Id);

            if (admin != null)
            {
                return Ok(admin);
            }

            return BadRequest();
        }

        [Route("Admins/{theaterAdminId}")]
        public void Delete(int theaterId, string theaterAdminId)
        {
            _theaterAdminService.RemoveTheaterAdmin(theaterId, theaterAdminId);
        }
    }
}
