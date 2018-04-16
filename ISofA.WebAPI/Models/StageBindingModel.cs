using ISofA.DAL.Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISofA.WebAPI.Models
{
    public class StageBindingModel
    {
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Name { get; set; }
        [Required]
        public int SeatRows { get; set; }
        [Required]
        public int SeatColumns { get; set; }

        public static implicit operator Stage(StageBindingModel model)
        {
            return new Stage()
            {
                Name = model.Name,
                SeatRows = model.SeatRows,
                SeatColumns = model.SeatColumns
            };
        }
    }
}
