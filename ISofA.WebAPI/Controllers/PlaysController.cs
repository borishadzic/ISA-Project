using ISofA.DAL.Core.Domain;
using ISofA.SL.DTO;
using ISofA.SL.Services;
using ISofA.WebAPI.Filters;
using ISofA.WebAPI.Models;
using System.Collections.Generic;
using System.Linq;
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
            return _playService.GetAll(theaterId);
        }

        [Route("api/Plays/{playId}")]
        public PlayDTO Get(int playId)
        {
            return _playService.Get(playId);
        }

        [Route("api/Theaters/{theaterId}/Plays")]
        public PlayDTO Post(int theaterId, [FromBody]PlayBindingModel play)
        {
            return _playService.Add(theaterId, play);
        }

        // PUT api/values/5
        [Route("api/Theaters/{theaterId}/Plays/{playId}")]
        public PlayDTO Put(int theaterId, int playId, [FromBody]PlayBindingModel play)
        {
            return _playService.Update(playId, play);
        }

        [Route("api/Theaters/{theaterId}/Plays/{playId}")]
        public void Delete(int theaterId, int playId)
        {
            _playService.Remove(playId);
        }
    }
}
