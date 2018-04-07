using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISofA.DAL.Core.Domain
{
    public enum TheaterType
    {
        Cinema,
        Play
    }

    public class Theater
    {
        [Key]
        public int TheaterId { get; set; }
        public string Name { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public TheaterType Type { get; set; }

        public virtual ICollection<ISofAUser> FanZoneAdmins { get; set; }
        public virtual ICollection<ISofAUser> TheaterAdmins { get; set; }

        public virtual ICollection<Play> Repertoire { get; set; }
        public virtual ICollection<Stage> Stages { get; set; }
        public virtual ICollection<Item> Items { get; set; }
        public virtual ICollection<UserItem> UserItems { get; set; }

        public virtual ICollection<ISofAUser> AdminsOfTheater { get; set; }
    }
}
