using ISofA.DAL.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISofA.WebAPI.Models
{
    public class ProjectionBindingModel
    {

        public static implicit operator Projection(ProjectionBindingModel model)
        {
            return new Projection()
            {
                
            };
        }
    }
}
