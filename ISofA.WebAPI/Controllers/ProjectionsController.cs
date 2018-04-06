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

        [Route("api/Projections")]
        public ProjectionDTO Post([FromBody]Projection projection)
        {
            return _projectionService.Add(projection);
        }

        [Route("api/Projections/{projectionId}")]
        public ProjectionDTO Put(int projectionId, [FromBody]Projection projection)
        {
            return _projectionService.Update(projectionId, projection);
        }

        [Route("api/Projections/{projectionId}")]
        public void Delete(int projectionId)
        {
            _projectionService.Remove(projectionId);
        }
    }
}
