using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISofA.DAL.Core.Domain
{
    public class Theater
    {
        [Key]
        public int TheaterId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ISofAUser> FanZoneAdmins { get; set; }
        public virtual ICollection<ISofAUser> TheaterAdmins { get; set; }
    }
}
