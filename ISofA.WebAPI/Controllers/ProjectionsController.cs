using ISofA.DAL.Core.Domain;
using ISofA.SL.DTO;
using ISofA.SL.Services;
using ISofA.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public IEnumerable<ProjectionDTO> GetProjections(int theaterId, DateTime dateStart, int days)
        {
            return _projectionService.GetProjectionsForTheater(theaterId, dateStart, days);
        }

        [Route("api/Projections/{projectionId}")]
        public ProjectionDTO Get(int projectionId)
        {
            return _projectionService.GetProjectionDetail(projectionId);
        }

        [Route("api/Theaters/{theaterId}/Projections")]
        public IEnumerable<ProjectionDTO> Post(int theaterId, [FromBody]IEnumerable<ProjectionBindingModel> projections)
        {
            return _projectionService.Add(theaterId, projections.Select<ProjectionBindingModel, Projection>(x=>x));
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
