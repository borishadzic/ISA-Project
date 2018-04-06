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
        IEnumerable<StageDTO> GetAll(int theaterId);
        StageDTO Get(int stageId);
        StageDTO Add(Stage stage);
        StageDTO Update(int stageId, Stage stage);
        void Remove(int stageId);


    }
}
