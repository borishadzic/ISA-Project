using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISofA.DAL.Core.Domain;

namespace ISofA.SL.DTO
{
    public class PlayDTO
    {

        public bool Active { get; set; }
        public int TheaterId { get; set; }
        public int PlayId { get; set; }
        public string Name { get; set; }
        public string Actors { get; set; }
        public string Genre { get; set; }
        public string Director { get; set; }
        public int DurationMins { get; set; }
        public string PosterUrl { get; set; }
        public string TrailerUrl { get; set; }
        public string Description { get; set; }

        public static implicit operator PlayDTO(Play play)
        {
            return new PlayDTO()
            {
                Active = play.Active,
                TheaterId = play.TheaterId,
                PlayId = play.PlayId,
                Name = play.Name,
                Actors = play.Actors,
                Genre = play.Genre,
                Director = play.Director,
                DurationMins = play.DurationMins,
                PosterUrl = play.PosterUrl,
                TrailerUrl = play.TrailerUrl,
                Description = play.Description,
            };
        }
    }
}
