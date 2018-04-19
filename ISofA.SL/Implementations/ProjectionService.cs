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

        private Projection AddStep(int theaterId, Projection projection)
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

            projection.TheaterId = stage.TheaterId;

            return UnitOfWork.Projections.Add(projection);
        }

        private IEnumerable<Projection> AddSteps(int theaterId, IEnumerable<Projection> projections)
        {
            foreach (var projection in projections)
                yield return AddStep(theaterId, projection);
        }

        public ProjectionDTO Add(int theaterId, Projection projection)
        {
            projection = AddStep(theaterId, projection);
            UnitOfWork.SaveChanges();

            return new ProjectionDTO(projection);
        }

        public IEnumerable<ProjectionDTO> Add(int theaterId, IEnumerable<Projection> projections)
        {
            var x = AddSteps(theaterId, projections).ToList();
            UnitOfWork.SaveChanges();

            return x.Select<Projection, ProjectionDTO>(y => new ProjectionDTO(y));
        }

        public IEnumerable<ProjectionDTO> GetProjectionsForTheater(int theaterId, DateTime dateStart, int days)
        {            
            var theater = UnitOfWork.Theaters.Get(theaterId);

            if (theater == null)
                throw new TheaterNotFoundException(theaterId);

            if (days < 0)
                throw new BadRequestException("BadRequest");

            days = days == 0 ? 0 : days - 1;

            var startTime = dateStart.Date.AddMinutes(theater.WorkStart);
            var endTime = dateStart.Date.AddDays(days).AddMinutes(theater.WorkStart + theater.WorkDuration);

            return UnitOfWork.Projections
                .Find(x => x.TheaterId == theaterId && x.StartTime >= startTime && x.StartTime < endTime )
                .Select(x => new ProjectionDTO(x));
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