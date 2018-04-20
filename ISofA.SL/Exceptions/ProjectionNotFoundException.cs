using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISofA.SL.Exceptions
{
    [Serializable]
    public class ProjectionNotFoundException : Exception
    {
        public ProjectionNotFoundException(int projectionId) : base($"Projection with id {projectionId} not found")
        {
        }
    }
}
