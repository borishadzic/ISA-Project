using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISofA.DAL.Core.Domain;

namespace ISofA.SL.DTO
{
    public class SeatDTO
    {
        public SeatDTO(Seat seat)
        {
            TheaterId = seat.TheaterId;
            PlayId = seat.PlayId;
            StageId = seat.StageId;
            ProjectionId = seat.ProjectionId;
            SeatRow = seat.SeatRow;
            SeatColumn = seat.SeatColumn;
            State = seat.State;
            Discount = seat.Discount;
        }

        public int TheaterId { get; }
        public int PlayId { get; }
        public int StageId { get; }
        public int ProjectionId { get; }
        public int SeatRow { get; }
        public int SeatColumn { get; }
        public SeatState State { get; }
        public int Discount { get; }
    }
}
