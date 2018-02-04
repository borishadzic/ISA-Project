using ISofA.DAL.Core.Domain;
using ISofA.SL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISofA.SL.Services
{
    public interface IStageService
    {
        IEnumerable<StageDTO> Get(int theaterId);
        StageDTO Get(int theaterId, int stageId);
        StageDTO Add(int theaterId, Stage stage);
        StageDTO Update(int theaterId, int stageId, Stage stage);
        void Remove(int theaterId, int stageId);


    }
}
