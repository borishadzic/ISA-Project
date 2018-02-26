using ISofA.DAL.Core.Domain;
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
    public class FanZoneAdminsController : ApiController
    {
        // TODO: Dodaj ogranicenje da samo administratori sistema mogu da dodaju nove admine fan zone
        private readonly IFanZoneAdminService _fanZoneAdminService;

        public FanZoneAdminsController(IFanZoneAdminService fanZoneAdminService)
        {
            _fanZoneAdminService = fanZoneAdminService;
        }

        [Route("FanZoneAdmins")]
        public IEnumerable<ISofAUserDTO> Get(int theaterId)
        {
            return _fanZoneAdminService.GetFanZoneAdmins(theaterId);
        }

        [Route("FanZoneAdmins")]
        public IHttpActionResult Post(int theaterId, AdminModel fanZoneAdmin)
        {
            var admin = _fanZoneAdminService.AddFanZoneAdmin(theaterId, fanZoneAdmin.Id);

            if (admin != null)
            {
                return Ok(admin);
            }

            return BadRequest();
        }

        [Route("FanZoneAdmins/{fanZoneAdminId}")]
        public void Delete(int theaterId, string fanZoneAdminId)
        {
            _fanZoneAdminService.RemoveFanZoneAdmin(theaterId, fanZoneAdminId);
        }
    }
}
