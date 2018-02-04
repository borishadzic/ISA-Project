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
    public class StagesController : ApiController
    {
        private readonly IStageService _stageService;

        public StagesController(IStageService stageService)
        {
            _stageService = stageService;
        }
        
        [Route("api/Theaters/{theaterId}/Stages")]
        public IEnumerable<StageDTO> Get(int theaterId) // TODO: LOW Da li je potrebno get sve?
        {
            return _stageService.Get(theaterId);
        }

        [Route("api/Theaters/{theaterId}/Stages/{stageId}")]
        public StageDTO Get(int theaterId, int stageId) // TODO: LOW Da li je potrebno get?
        {
            return _stageService.Get(theaterId, stageId);
        }

        [Route("api/Theaters/{theaterId}/Stages")]
        public StageDTO Post(int theaterId, [FromBody]Stage stage)
        {
            return _stageService.Add(theaterId, stage);
        }

        [Route("api/Theaters/{theaterId}/Stages/{stageId}")]
        public StageDTO Put(int theaterId, int stageId, [FromBody]Stage stage)
        {
            return _stageService.Update(theaterId, stageId, stage);
        }

        [Route("api/Theaters/{theaterId}/Stages/{stageId}")]
        public void Delete(int theaterId, int stageId)
        {
            _stageService.Remove(theaterId, stageId);
        }
    }
}
