using System;
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
        [Key]
        [Column(Order = 1)]
        public int TheaterId { get; set; }
        [Key]
        [Column(Order = 2)]
        public int PlayId { get; set; }
        [Key]
        [Column(Order = 3)]
        public int StageId { get; set; }
        [Key]
        [Column(Order = 4)]
        public int ProjectionId { get; set; }        
        [Key]
        [Column(Order = 5)]
        public int SeatRow { get; set; }
        [Key]
        [Column(Order = 6)]
        public int SeatColumn { get; set; }

        public string UserId { get; set; }
        public SeatState State { get; set; }
        public int Discount { get; set; }

        [ForeignKey("TheaterId, PlayId, StageId, ProjectionId")]
        public virtual Projection Projection { get; set; }
        [ForeignKey("UserId")]
        public virtual ISofAUser User { get; set; }
    }
}
