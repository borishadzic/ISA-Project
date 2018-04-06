using ISofA.Tests.DI;
using ISofA.Tests.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace ISofA.Tests.Unit
{
    public class ServiceTest
    {
        protected IUnityContainer _diResolver;
        protected ITestUnitOfWork _unitOfWork;

        protected void Init()
        {
            _diResolver = UnityConfig.Container;
            _unitOfWork = _diResolver.Resolve<ITestUnitOfWork>();
            _unitOfWork.NukeDatabase();
        }
    }
}
