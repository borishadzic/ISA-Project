using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISofA.DAL.Core.Domain;

namespace ISofA.SL.DTO
{
    public class SpeedSeatListElementDTO
    {

        public SpeedSeatListElementDTO(Seat x)
        {
            TheaterId = x.TheaterId;
            PlayName = x.Play.Name;
            PlayPosterUrl = x.Play.PosterUrl;
            StageName = x.Stage.Name;
            StartTime = x.Projection.StartTime;
            SeatRow = x.SeatRow;
            SeatColumn = x.SeatColumn;
            Discount = x.Discount;
        }

        public int TheaterId { get; }
        public String PlayName { get; }
        public String PlayPosterUrl { get; }
        public String StageName { get; }
        public DateTime StartTime { get; }
        public int Discount { get;  }
        public int SeatRow { get; }
        public int SeatColumn { get; }
    }
}
