using ISofA.DAL.Core.Domain;
using ISofA.SL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISofA.SL.Services
{
    public interface IProjectionService
    {
        IEnumerable<ProjectionDTO> GetProjectionsForPlay(int theaterId, int playId, DateTime dateStart);
        ProjectionDTO GetProjectionDetail(int theaterId, int playId, int stageId, int projectionId);
        ProjectionDTO Add(int theaterId, int playId, int stageId, Projection projection);
        ProjectionDTO Update(int theaterId, int playId, int stageId, int projectionId, Projection projection);
        void Remove(int theaterId, int playId, int stageId, int projectionId);        
    }
}