using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISofA.DAL.Core.Domain
{
    public class Play
    {
        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PlayId { get; set; }

        public bool Active { get; set; }
        public string Name { get; set; }
        public string Actors { get; set; }
        public string Genre { get; set; }
        public string Director { get; set; }
        public int DurationMins { get; set; }
        public string PosterUrl { get; set; }
        public string TrailerUrl { get; set; }
        public string Description { get; set; }

        public int TheaterId { get; set; }

        [ForeignKey("TheaterId")]
        public virtual Theater Theater { get; set; }

        public virtual ICollection<Projection> Projections { get; set; }
    }
}
