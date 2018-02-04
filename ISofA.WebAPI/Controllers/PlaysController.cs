using ISofA.DAL.Core.Domain;
using ISofA.SL.DTO;
using ISofA.SL.Services;
using ISofA.WebAPI.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ISofA.WebAPI.Controllers
{
    public class PlaysController : ApiController
    {
        private readonly IPlayService _playService;

        public PlaysController(IPlayService playService)
        {
            _playService = playService;
        }
        
        [Route("api/Theaters/{theaterId}/Plays")]
        public IEnumerable<PlayDTO> Get(int theaterId)
        {
            return _playService.Get(theaterId);
        }

        [Route("api/Theaters/{theaterId}/Plays/{playId}")]
        public PlayDTO Get(int theaterId, int playId)
        {
            return _playService.Get(theaterId, playId);
        }

        [Route("api/Theaters/{theaterId}/Plays")]
        public PlayDTO Post(int theaterId, [FromBody]Play play)
        {
            return _playService.Add(theaterId, play);
        }

        // PUT api/values/5
        [Route("api/Theaters/{theaterId}/Plays/{playId}")]
        public PlayDTO Put(int theaterId, int playId, [FromBody]Play play)
        {
            return _playService.Update(theaterId, playId, play);
        }

        [Route("api/Theaters/{theaterId}/Plays/{playId}")]
        public void Delete(int theaterId, int playId)
        {
            _playService.Remove(theaterId, playId);
        }
    }
}
