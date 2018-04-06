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
        
        [Route("api/Stages")]
        public IEnumerable<StageDTO> GetAll(int theaterId) // TODO: LOW Da li je potrebno get sve?
        {
            return _stageService.GetAll(theaterId);
        }

        [Route("api/Stages/{stageId}")]
        public StageDTO Get(int stageId) // TODO: LOW Da li je potrebno get?
        {
            return _stageService.Get(stageId);
        }

        [Route("api/Stages")]
        public StageDTO Post([FromBody]Stage stage)
        {
            return _stageService.Add(stage);
        }

        [Route("api/Stages/{stageId}")]
        public StageDTO Put(int stageId, [FromBody]Stage stage)
        {
            return _stageService.Update(stageId, stage);
        }

        [Route("api/Stages/{stageId}")]
        public void Delete(int stageId)
        {
            _stageService.Remove(stageId);
        }
    }
}
