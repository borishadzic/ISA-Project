using ISofA.SL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace ISofA.Tests.Unit.Student_3.PlayService
{
    public class PlayServiceTest : IdentityServiceTest
    {
        protected IPlayService _playService;

        protected new void Init()
        {
            base.Init();
            _playService = _diResolver.Resolve<IPlayService>();
        }
    }
}
