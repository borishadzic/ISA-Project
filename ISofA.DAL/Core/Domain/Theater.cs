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
        public int ThaterId { get; set; }

        public string Name { get; set; }
        public virtual ICollection<ISofAUser> FanZoneAdministrators { get; set; }
    }
}
