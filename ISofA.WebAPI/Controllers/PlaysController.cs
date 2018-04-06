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
        
        [Route("api/Plays")]
        public IEnumerable<PlayDTO> GetRepertoire(int theaterId)
        {
            return _playService.GetRepertoire(theaterId);
        }

        [Route("api/Plays/{playId}")]
        public PlayDTO Get(int playId)
        {
            return _playService.Get(playId);
        }

        [Route("api/Plays")]
        public PlayDTO Post([FromBody]Play play)
        {
            return _playService.Add(play);
        }

        // PUT api/values/5
        [Route("api/Plays/{playId}")]
        public PlayDTO Put(int playId, [FromBody]Play play)
        {
            return _playService.Update(playId, play);
        }

        [Route("api/Plays/{playId}")]
        public void Delete(int playId)
        {
            _playService.Remove(playId);
        }
    }
}
