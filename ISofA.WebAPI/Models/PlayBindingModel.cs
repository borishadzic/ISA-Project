using ISofA.DAL.Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ISofA.WebAPI.Models
{
    public class PlayBindingModel
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 2)]
        public string Actors { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Genre { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Director { get; set; }

        [Required]
        public int DurationMins { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string PosterUrl { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string TrailerUrl { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Description { get; set; }

        public static implicit operator Play(PlayBindingModel model)
        {
            return new Play()
            {
                Name = model.Name,
                Actors = model.Actors,
                Genre = model.Genre,
                Director = model.Director,
                DurationMins = model.DurationMins,
                PosterUrl = model.PosterUrl,
                TrailerUrl = model.TrailerUrl,
                Description = model.Description,
            };
        }
    }
}