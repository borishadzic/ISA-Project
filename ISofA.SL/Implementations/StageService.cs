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
    public class StageService : Service, IStageService
    {
        public StageService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public StageDTO Add(int theaterId, Stage stage)
        {
            if (stage == null)
                throw new BadRequestException("Bad Request");

            var theater = UnitOfWork.Theaters.Get(theaterId);

            if (theater == null)
                throw new TheaterNotFoundException(theaterId);

            stage.TheaterId = theaterId;

            UnitOfWork.Stages.Add(stage);
            UnitOfWork.SaveChanges();
            return new StageDTO(stage);
        }

        public IEnumerable<StageDTO> GetAll(int theaterId)
        {            
            if (UnitOfWork.Theaters.Get(theaterId) == null)
                throw new TheaterNotFoundException(theaterId);

            return UnitOfWork.Stages.Find(x => x.TheaterId == theaterId)
                .Select(x => new StageDTO(x));
        }

        public StageDTO GetStage(int theaterId, int stageId)
        {
            if (UnitOfWork.Theaters.Get(theaterId) == null)
                throw new TheaterNotFoundException(theaterId);

            return UnitOfWork.Stages.Find(x => x.TheaterId == theaterId && x.StageId == stageId)
                .Select(x => new StageDTO(x)).FirstOrDefault();
        }

        public void Remove(int stageId)
        {
            var stage = UnitOfWork.Stages.Get(stageId);

            if (stage == null)
                throw new StageNotFoundException(stageId);

            UnitOfWork.Stages.Remove(stage);

            UnitOfWork.SaveChanges();
        }

        public StageDTO Update(int stageId, Stage stage)
        {
            if (stage == null)
                throw new BadRequestException("Bad Request");

            var modified = UnitOfWork.Stages.Get(stageId);

            if (modified == null)
                throw new StageNotFoundException(stageId);

            var projections = modified.Projections.Where(x=>x.StartTime > DateTime.Now).Count();

            if (projections > 0 && (modified.SeatRows != stage.SeatRows || modified.SeatColumns != stage.SeatColumns))
                throw new BadRequestException("Can't update stage size while projections are active");

            modified.Name = stage.Name;
            modified.SeatRows = stage.SeatRows;
            modified.SeatColumns = stage.SeatColumns;

            UnitOfWork.Modified(modified);
            UnitOfWork.SaveChanges();
            return new StageDTO(modified);
        }
    }
}