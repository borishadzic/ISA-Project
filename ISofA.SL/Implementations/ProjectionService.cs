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

        public ProjectionDTO Add(int theaterId, int playId, int stageId, Projection projection)
        {
            projection.TheaterId = theaterId;
            projection.PlayId = playId;
            projection.StageId = stageId;
            projection = UnitOfWork.Pantry<Projection>().Add(projection);
            UnitOfWork.SaveChanges();

            return new ProjectionDTO(projection);
        }

        public IEnumerable<ProjectionDTO> GetProjectionsForPlay(int theaterId, int playId, DateTime dateStart)
        {
            dateStart = dateStart.Date;
            DateTime dateEnd = dateStart.AddDays(1);            
            return UnitOfWork.Pantry<Projection>()
                .Find(x => x.TheaterId == theaterId && x.PlayId == playId && x.StartTime >= dateStart && x.StartTime <= dateStart.AddDays(1))
                .Select(x => new ProjectionDTO(x));
        }

        public ProjectionDTO GetProjectionDetail(int theaterId, int playId, int stageId, int projectionId)
        {
            Projection projection = UnitOfWork.Pantry<Projection>().Get(theaterId, playId, stageId, projectionId);
            return new ProjectionDTO(projection);
        }

        public void Remove(int theaterId, int playId, int stageId, int projectionId)
        {
            IProjectionPantry pantry = (IProjectionPantry)UnitOfWork.Pantry<Projection>();
            pantry.Remove(pantry.Get(theaterId, playId, stageId, projectionId));
            UnitOfWork.SaveChanges();
        }

        public ProjectionDTO Update(int theaterId, int playId, int stageId, int projectionId, Projection projection)
        {
            Projection modified = UnitOfWork.Pantry<Projection>().Get(theaterId, playId, stageId, projectionId);
            UnitOfWork.Modified(modified);
            modified.StartTime = projection.StartTime;
            modified.Price = projection.Price;
            UnitOfWork.SaveChanges();
            return new ProjectionDTO(modified);
        }
    }
}
