using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISofA.DAL.Core.Domain
{
    public class Stage
    {        
        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StageId { get; set; }

        public int SeatRows { get; set; }
        public int SeatColumns { get; set; }

        public int TheaterId { get; set; }

        [ForeignKey("TheaterId")]
        public virtual Theater Theater { get; set; }

        public virtual ICollection<Projection> Projections { get; set; }
    }
}
