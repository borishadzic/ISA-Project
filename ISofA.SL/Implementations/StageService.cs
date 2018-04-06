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

        public StageDTO Add(Stage stage)
        {
            // todo theaterId exists? authorize
            UnitOfWork.Stages.Add(stage);
            UnitOfWork.SaveChanges();
            return new StageDTO(stage);
        }

        public IEnumerable<StageDTO> GetAll(int theaterId)
        {
            return UnitOfWork.Stages.Find(x => x.TheaterId == theaterId)
                .Select(x => new StageDTO(x));
        }

        public StageDTO Get(int stageId)
        {
            Stage stage = UnitOfWork.Stages.Get(stageId);
            return new StageDTO(stage);
        }

        public void Remove(int stageId)
        {
            IStagePantry pantry = (IStagePantry)UnitOfWork.Stages;
            pantry.Remove(pantry.Get(stageId));
            UnitOfWork.SaveChanges();

        }

        public StageDTO Update(int stageId, Stage stage)
        {
            // TODO: Throw exception when projections exist for given stage
            // maybe uneditable?
            Stage modified = UnitOfWork.Stages.Get(stageId);
            modified.SeatRows = stage.SeatRows;
            modified.SeatColumns = stage.SeatColumns;
            return new StageDTO(modified);
        }
    }
}