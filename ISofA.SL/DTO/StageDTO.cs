using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISofA.DAL.Core.Domain;

namespace ISofA.SL.DTO
{
    public class StageDTO
    {
        public StageDTO(Stage stage)
        {
            TheaterId = stage.TheaterId;
            StageId = stage.StageId;
            SeatRows = stage.SeatRows;
            SeatColumns = stage.SeatColumns;
            Name = stage.Name;
        }

        public int TheaterId { get; set; }
        public int StageId { get; set; }
        public int SeatRows { get; set; }
        public int SeatColumns { get; set; }
        public string Name { get; set; }
    }
}
