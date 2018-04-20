using ISofA.DAL.Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ISofA.WebAPI.Models
{
    public class DiscountTicketBindingModel
    {
        [Required]
        public int Discount { get; set; }
        [Required]
        public int SeatRow { get; set; }
        [Required]
        public int SeatColumn { get; set; }

        public static implicit operator Seat(DiscountTicketBindingModel model)
        {
            return new Seat()
            {
                Discount = model.Discount,
                SeatRow = model.SeatRow,
                SeatColumn = model.SeatColumn,
                State = SeatState.Speed
            };
        }
    }
}