using ISofA.DAL.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISofA.SL.DTO
{
    public class TheaterDetailDTO
    {
        public int TheaterId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public int WorkStart { get; set; }
        public int WorkDuration { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public TheaterType Type { get; set; }

        public static implicit operator TheaterDetailDTO(Theater theater)
        {
            return new TheaterDetailDTO()
            {
                TheaterId = theater.TheaterId,
                Name = theater.Name,
                Address = theater.Address,
                Description = theater.Description,
                WorkStart = theater.WorkStart,
                WorkDuration = theater.WorkDuration,
                Latitude = theater.Latitude,
                Longitude = theater.Longitude,                
                Type = theater.Type,
            };
        }

    }
}
