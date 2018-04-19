using ISofA.DAL.Core.Domain;
using ISofA.SL.DTO;
using System.Collections.Generic;

namespace ISofA.SL.Services
{
    public interface IStageService
    {
        IEnumerable<StageDTO> GetAll(int theaterId);
        StageDTO Add(int theaterId, Stage stage);
        StageDTO Update(int stageId, Stage stage);
        void Remove(int stageId);
        StageDTO GetStage(int theaterId, int stageId);


    }
}
