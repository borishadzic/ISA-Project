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
            SeatRow = x.SeatRow;
            SeatColumn = x.SeatColumn;
            Discount = x.Discount;
        }

        public int Discount { get;  }
        public int SeatRow { get; }
        public int SeatColumn { get; }
    }
}
