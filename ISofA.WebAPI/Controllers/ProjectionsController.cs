using ISofA.SL.DTO;
using ISofA.SL.Services;
using ISofA.WebAPI.Models;
using System;
using System.Collections.Generic;
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

        [Route("api/Projections")]
        public IEnumerable<ProjectionDTO> GetProjectionsForPlay(int playId, DateTime dateStart)
        {
            return _projectionService.GetProjectionsForPlay(playId, dateStart.ToUniversalTime());
        }

        [Route("api/Projections/{projectionId}")]
        public ProjectionDTO Get(int projectionId)
        {
            return _projectionService.GetProjectionDetail(projectionId);
        }

        [Route("api/Theaters/{theaterId}/Projections")]
        public ProjectionDTO Post(int theaterId, [FromBody]ProjectionBindingModel projection)
        {
            return _projectionService.Add(theaterId, projection);
        }

        [Route("api/Theaters/{theaterId}/Projections/{projectionId}")]
        public ProjectionDTO Put(int theaterId, int projectionId, [FromBody]ProjectionBindingModel projection)
        {
            return _projectionService.Update(projectionId, projection);
        }

        [Route("api/Theaters/{theaterId}/Projections/{projectionId}")]
        public void Delete(int theaterId, int projectionId)
        {
            _projectionService.Remove(projectionId);
        }
    }
}
