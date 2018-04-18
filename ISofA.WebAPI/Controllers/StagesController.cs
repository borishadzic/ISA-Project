using ISofA.DAL.Core.Domain;
using ISofA.SL.DTO;
using ISofA.SL.Services;
using ISofA.WebAPI.Models;
using System.Collections.Generic;
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
        public IEnumerable<StageDTO> GetAll(int theaterId) // TODO: LOW Da li je potrebno get sve?
        {
            return _stageService.GetAll(theaterId);
        }

        [Route("api/Theaters/{theaterId}/Stages")]
        public StageDTO Post(int theaterId, [FromBody]StageBindingModel stage)
        {
            return _stageService.Add(theaterId, stage);
        }

        [Route("api/Theaters/{theaterId}/Stages/{stageId}")]
        public StageDTO Put(int theaterId, int stageId, [FromBody]StageBindingModel stage)
        {
            return _stageService.Update(stageId, stage);
        }

        [Route("api/Theaters/{theaterId}/Stages/{stageId}")]
        public void Delete(int theaterId, int stageId)
        {
            _stageService.Remove(stageId);
        }
    }
}
