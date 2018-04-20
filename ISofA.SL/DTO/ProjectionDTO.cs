using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISofA.DAL.Core.Domain;

namespace ISofA.SL.DTO
{
    public class ProjectionDTO
    {

        public ProjectionDTO(Projection projection)
        {
            TheaterId = projection.TheaterId;
            PlayId = projection.PlayId;
            StageId = projection.StageId;
            ProjectionId = projection.ProjectionId;
            StartTime = projection.StartTime;
            Price = projection.Price;
        }

        public int TheaterId { get; set; }
        public int PlayId { get; set; }
        public int StageId { get; set; }
        public int ProjectionId { get; set; }
        public DateTime StartTime { get; set; }
        public int Price { get; set; }
    }
}
