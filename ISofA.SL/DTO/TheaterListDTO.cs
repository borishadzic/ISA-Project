using ISofA.DAL.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISofA.SL.DTO
{
    public class TheaterListDTO
    {
        public TheaterListDTO(Theater theater)
        {
            TheaterId = theater.TheaterId;
            Name = theater.Name;
            Latitude = theater.Latitude;
            Longitude = theater.Longitude;
        }

        public int TheaterId { get; set; }
        public string Name { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
    }
}
