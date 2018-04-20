using ISofA.DAL.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ISofA.WebAPI.Models
{
    public class TheaterBindingModel
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public DateTime WorkStart { get; set; }
        public int WorkDuration { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public TheaterType Type { get; set; }

        public static implicit operator Theater(TheaterBindingModel model)
        {
            return new Theater()
            {
                Name = model.Name,
                Address = model.Address,
                Description = model.Description,
                WorkStart = model.WorkStart.Hour * 60 + model.WorkStart.Minute,
                WorkDuration = model.WorkDuration,
                Latitude = model.Latitude,
                Longitude = model.Longitude,
                Type = model.Type
            };
        }
    }
}