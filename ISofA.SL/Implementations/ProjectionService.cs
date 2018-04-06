using ISofA.DAL.Core;
using ISofA.DAL.Core.Domain;
using ISofA.DAL.Core.Pantries;
using ISofA.SL.DTO;
using ISofA.SL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISofA.SL.Implementations
{
    public class ProjectionService : Service, IProjectionService
    {
        public ProjectionService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public ProjectionDTO Add(Projection projection)
        {
            // Todo: play Id does not belong to theater with stageId
            projection = UnitOfWork.Projections.Add(projection);
            UnitOfWork.SaveChanges();

            return new ProjectionDTO(projection);
        }

        public IEnumerable<ProjectionDTO> GetProjectionsForPlay(int playId, DateTime dateStart)
        {
            dateStart = dateStart.Date;
            DateTime dateEnd = dateStart.AddDays(1);
            return UnitOfWork.Projections
                .Find(x => x.PlayId == playId && x.StartTime >= dateStart && x.StartTime <= dateStart.AddDays(1))
                .Select(x => new ProjectionDTO(x));
        }

        public ProjectionDTO GetProjectionDetail(int projectionId)
        {
            Projection projection = UnitOfWork.Projections.Get(projectionId);
            return new ProjectionDTO(projection);
        }

        public void Remove(int projectionId)
        {
            IProjectionPantry pantry = UnitOfWork.Projections;
            pantry.Remove(pantry.Get(projectionId));
            UnitOfWork.SaveChanges();
        }

        public ProjectionDTO Update(int projectionId, Projection projection)
        {
            Projection modified = UnitOfWork.Projections.Get(projectionId);
            UnitOfWork.Modified(modified);
            modified.StartTime = projection.StartTime;
            modified.Price = projection.Price;
            UnitOfWork.SaveChanges();
            return new ProjectionDTO(modified);
        }
    }
}