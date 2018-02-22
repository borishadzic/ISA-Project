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
    public class StageService : Service, IStageService
    {
        public StageService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public StageDTO Add(int theaterId, Stage stage)
        {
            stage.TheaterId = theaterId;
            UnitOfWork.Stages.Add(stage);
            UnitOfWork.SaveChanges();
            return new StageDTO(stage);
        }

        public IEnumerable<StageDTO> Get(int theaterId)
        {
            return UnitOfWork.Stages.Find(x => x.TheaterId == theaterId)
                .Select(x => new StageDTO(x));
        }

        public StageDTO Get(int theaterId, int stageId)
        {
            Stage stage = UnitOfWork.Stages.Get(theaterId, stageId);
            return new StageDTO(stage);
        }

        public void Remove(int theaterId, int stageId)
        {
            IStagePantry pantry = (IStagePantry)UnitOfWork.Stages;
            pantry.Remove(pantry.Get(theaterId, stageId));
            UnitOfWork.SaveChanges();

        }

        public StageDTO Update(int theaterId, int stageId, Stage stage)
        {
            // TODO: Throw exception when projections exist for given stage
            Stage modified = UnitOfWork.Stages.Get(theaterId, stageId);
            modified.SeatRows = stage.SeatRows;
            modified.SeatColumns = stage.SeatColumns;
            return new StageDTO(modified);
        }
    }
}