using ISofA.DAL.Core;
using ISofA.DAL.Core.Domain;
using ISofA.DAL.Core.Pantries;
using ISofA.SL.DTO;
using ISofA.SL.Exceptions;
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

        public ProjectionDTO Add(int theaterId, Projection projection)
        {
            if (projection == null)
                throw new BadRequestException("Bad Request");

            var theater = UnitOfWork.Theaters.Get(theaterId);

            if (theater == null)
                throw new TheaterNotFoundException(theaterId);

            var play = UnitOfWork.Plays.Get(projection.PlayId);

            if (play == null)
                throw new PlayNotFoundException(projection.PlayId);

            var stage = UnitOfWork.Stages.Get(projection.StageId);

            if (stage == null)
                throw new StageNotFoundException(projection.StageId);

            if (play.TheaterId != theaterId)
                throw new BadRequestException("Bad Request");

            if (stage.TheaterId != theaterId)
                throw new BadRequestException("Bad Request");
                        
            projection = UnitOfWork.Projections.Add(projection);
            UnitOfWork.SaveChanges();

            return new ProjectionDTO(projection);
        }

        public IEnumerable<ProjectionDTO> GetProjectionsForPlay(int playId, DateTime dateStart)
        {
            dateStart = dateStart.Date;
            DateTime dateEnd = dateStart.AddDays(1);
            return UnitOfWork.Projections
                .Find(x => x.PlayId == playId && x.StartTime >= dateStart && x.StartTime <= dateEnd)
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