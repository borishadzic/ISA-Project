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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProjectionId { get; set; }

        public DateTime StartTime { get; set; }
        public int Price { get; set; }

        public int TheaterId { get; set; }
        public int PlayId { get; set; }
        public int StageId { get; set; }

        [ForeignKey(nameof(PlayId))]
        public virtual Play Play { get; set; }
        
        [ForeignKey(nameof(StageId))]
        public virtual Stage Stage { get; set; }        

        [ForeignKey(nameof(TheaterId))]
        public virtual Theater Theater { get; set; }

        public virtual ICollection<Seat> Reservations { get; set; }
    }
}
