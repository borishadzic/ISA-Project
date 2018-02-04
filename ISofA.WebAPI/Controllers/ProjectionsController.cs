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
    public class ProjectionsController : ApiController
    {
        private readonly IProjectionService _projectionService;

        public ProjectionsController(IProjectionService projectionService)
        {
            _projectionService = projectionService;
        }

        [Route("api/Theaters/{theaterId}/Projections")]
        public IEnumerable<ProjectionDTO> GetProjectionsForPlay(int theaterId, int playId, DateTime dateStart)
        {
            return _projectionService.GetProjectionsForPlay(theaterId, playId, dateStart.ToUniversalTime());
        }

        [Route("api/Theaters/{theaterId}/Projections/{projectionId}")]
        public ProjectionDTO Get(int theaterId, int playId, int stageId, int projectionId)
        {
            return _projectionService.GetProjectionDetail(theaterId, playId, stageId, projectionId);
        }

        [Route("api/Theaters/{theaterId}/Projections")]
        public ProjectionDTO Post(int theaterId, int playId, int stageId, [FromBody]Projection projection)
        {
            return _projectionService.Add(theaterId, playId, stageId, projection);
        }

        [Route("api/Theaters/{theaterId}/Projections/{projectionId}")]
        public ProjectionDTO Put(int theaterId, int playId, int stageId, int projectionId, [FromBody]Projection projection)
        {
            return _projectionService.Update(theaterId, playId, stageId, projectionId, projection);
        }

        [Route("api/Theaters/{theaterId}/Projections/{projectionId}")]
        public void Delete(int theaterId, int playId, int stageId, int projectionId)
        {
            _projectionService.Remove(theaterId, playId, stageId, projectionId);
        }
    }
}
