using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISofA.DAL.Core.Domain
{
    public class Projection
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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProjectionId { get; set; }

        public DateTime StartTime { get; set; }
        public int Price { get; set; }

        [ForeignKey("TheaterId")]
        public virtual Theater Theater { get; set; }

        [ForeignKey("TheaterId, PlayId")]
        public virtual Play Play{ get; set; }

        [ForeignKey("TheaterId, StageId")]
        public virtual Stage Stage { get; set; }

        public virtual ICollection<Seat> Seats { get; set; }
    }
}
