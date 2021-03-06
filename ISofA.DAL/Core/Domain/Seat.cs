﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISofA.DAL.Core.Domain
{
    public enum SeatState
    {
        Reserved, VIP, Speed, Bought
    }

    public class Seat
    {
        public int TheaterId { get; set; }

        public int PlayId { get; set; }

        public int StageId { get; set; } // Properties no longer reflect reality

        [Key]
        [Column(Order = 1)]
        public int ProjectionId { get; set; }
        [Key]
        [Column(Order = 2)]
        public int SeatRow { get; set; }
        [Key]
        [Column(Order = 3)]
        public int SeatColumn { get; set; }

        public string UserId { get; set; }
        public SeatState State { get; set; }
        public int Discount { get; set; }

        [ForeignKey(nameof(TheaterId))]
        public virtual Theater Theater { get; set; }        

        [ForeignKey(nameof(ProjectionId))]
        public virtual Projection Projection { get; set; }

        [ForeignKey(nameof(PlayId))]
        public virtual Play Play { get; set; }

        [ForeignKey(nameof(StageId))]
        public virtual Stage Stage { get; set; }

        [ForeignKey("UserId")]
        public virtual ISofAUser User { get; set; }
    }
}
