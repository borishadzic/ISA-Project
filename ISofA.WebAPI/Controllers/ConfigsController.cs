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
    [RoutePrefix("api/configs")]
    public class ConfigsController : ApiController
    {
        private readonly IConfigService _configService;

        public ConfigsController(IConfigService configService)
        {
            _configService = configService;
        }

        [Route("userlevel")]
        public UserLevelDTO Get()
        {
            return _configService.GetUserLevel();
        }

        [Route("userlevel")]
        public UserLevelDTO Post(UserLevelDTO userLevel)
        {
            var levels = _configService.AddUserLevel(userLevel);

            if (levels == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            return levels;
        }
    }
}
