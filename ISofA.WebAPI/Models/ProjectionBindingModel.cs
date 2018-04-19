using ISofA.DAL.Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISofA.WebAPI.Models
{
    public class ProjectionBindingModel
    {
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public int PlayId { get; set; }
        [Required]
        public int StageId { get; set; }

        public static implicit operator Projection(ProjectionBindingModel model)
        {
            return new Projection()
            {
                StartTime = model.StartTime,
                Price = model.Price,
                PlayId = model.PlayId,
                StageId = model.StageId
            };
        }
    }
}
