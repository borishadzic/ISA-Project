using ISofA.DAL.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISofA.WebAPI.Models
{
    public class StageBindingModel
    {

        public static implicit operator Stage(StageBindingModel model)
        {
            return new Stage()
            {

            };
        }
    }
}
