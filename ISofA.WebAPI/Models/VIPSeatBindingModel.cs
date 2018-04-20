using ISofA.DAL.Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ISofA.WebAPI.Models
{
    public class VIPSeatBindingModel
    {
        [Required]
        public int SeatRow { get; set; }
        [Required]
        public int SeatColumn { get; set; }

        public static implicit operator Seat(VIPSeatBindingModel model)
        {
            return new Seat()
            {
                SeatRow = model.SeatRow,
                SeatColumn = model.SeatColumn,
                State = SeatState.VIP
            };
        }
    }
}