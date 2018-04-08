﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISofA.DAL.Core.Domain
{
    public enum TheaterType
    {
        Cinema = 0, Play = 1
    }

    public class Theater
    {
        [Key]
        public int TheaterId { get; set; }
        public string Name { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public TheaterType Type { get; set; }

        public virtual ICollection<ISofAUser> Admins { get; set; }
        public virtual ICollection<Play> Repertoire { get; set; }
        public virtual ICollection<Stage> Stages { get; set; }
        public virtual ICollection<Item> Items { get; set; }
        public virtual ICollection<UserItem> UserItems { get; set; }
    }
}
