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
        public string Name { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public TheaterType Type { get; set; }
        public IEnumerable<RepertoireDTO> Repertoire { get; set; }

        public static implicit operator TheaterDetailDTO(Theater theater)
        {
            return new TheaterDetailDTO()
            {
                Name = theater.Name,
                Latitude = theater.Latitude,
                Longitude = theater.Longitude,
                Type = theater.Type,
                Repertoire = theater.Repertoire.Select<Play, RepertoireDTO>(x => x)
            };
        }

        public class RepertoireDTO
        {
            public string Name { get; set; }
            public string Actors { get; set; }
            public string Genre { get; set; }
            public string Director { get; set; }
            public int DurationMins { get; set; }
            public string PosterUrl { get; set; }
            public string TrailerUrl { get; set; }
            public string Description { get; set; }

            public IEnumerable<ProjectionListDTO> Projections { get; set; }

            public static implicit operator RepertoireDTO(Play play)
            {
                var today = DateTime.Today;
                var tommorow = DateTime.Today.AddDays(1);
                return new RepertoireDTO()
                {
                    Name = play.Name,
                    Actors = play.Actors,
                    Genre = play.Genre,
                    Director = play.Director,
                    DurationMins = play.DurationMins,
                    PosterUrl = play.PosterUrl,
                    TrailerUrl = play.TrailerUrl,
                    Description = play.Description,
                    Projections = play.Projections.Where(x=>x.StartTime > today && x.StartTime <= tommorow).Select<Projection, ProjectionListDTO>(x => x)
                };
            }

            public class ProjectionListDTO
            {

                public DateTime StartTime { get; set; }
                public int Price { get; set; }
                public string Stage { get; set; }

                public static implicit operator ProjectionListDTO(Projection projection)
                {
                    return new ProjectionListDTO()
                    {
                        StartTime = projection.StartTime,
                        Price = projection.Price,
                        Stage = projection.Stage.Name
                    };
                }
            }
        }

    }
}
