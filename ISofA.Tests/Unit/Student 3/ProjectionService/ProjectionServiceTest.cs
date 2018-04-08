using ISofA.SL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace ISofA.Tests.Unit.Student_3.ProjectionService
{
    public class ProjectionServiceTest : ServiceTest
    {
        protected IProjectionService _projectionService;

        protected new void Init()
        {
            base.Init();
            _projectionService = _diResolver.Resolve<IProjectionService>();
        }
    }
}
