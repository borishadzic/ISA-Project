using ISofA.DAL.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ISofA.WebAPI.Models
{
    public class InvatationBindingModel
    {
        public IEnumerable<ISofAUser> users { get; set; }
        public IEnumerable<int> projectionIds { get; set; }
        public IEnumerable<int> rows { get; set; }
        public IEnumerable<int> columns { get; set; }
    }
}