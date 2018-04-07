using ISofA.SL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace ISofA.Tests.Unit.Student_3.StageService
{
    public class StageServiceTest : ServiceTest
    {
        protected IStageService _stageService;

        protected new void Init()
        {
            base.Init();
            _stageService = _diResolver.Resolve<IStageService>();
        }
    }
}
