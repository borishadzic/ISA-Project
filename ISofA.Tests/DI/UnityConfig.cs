using ISofA.DAL.Core;
using ISofA.DAL.Core.Pantries;
using ISofA.DAL.Persistence;
using ISofA.DAL.Persistence.Pantries;
using ISofA.SL.Implementations;
using ISofA.SL.Services;
using ISofA.Tests.Persistence;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace ISofA.Tests.DI
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<DbContext, ISofATestDbContext>();

            container.RegisterType<ITestUnitOfWork, TestUnitOfWork>();
            container.RegisterType<IUnitOfWork, TestUnitOfWork>();
            RegisterPantries(container);
            RegisterServices(container);
        }

        private static void RegisterPantries(IUnityContainer container)
        {
            container.RegisterType<IUserPantry, UserPantry>();
            container.RegisterType<ITheaterPantry, TheaterPantry>();
            container.RegisterType<IPlayPantry, PlayPantry>();
            container.RegisterType<IProjectionPantry, ProjectionPantry>();
            container.RegisterType<ISeatPantry, SeatPantry>();
            container.RegisterType<IStagePantry, StagePantry>();
            container.RegisterType<IItemPantry, ItemPantry>();
            container.RegisterType<IFriendRequestPantry, FriendRequestPantry>();
        }

        private static void RegisterServices(IUnityContainer container)
        {
            container.RegisterType<IAuthService, AuthService>();
            container.RegisterType<IPlayService, PlayService>();
            container.RegisterType<IStageService, StageService>();
            container.RegisterType<ISeatService, SeatService>();
            container.RegisterType<IProjectionService, ProjectionService>();
            container.RegisterType<IVisitService, VisitService>();
            container.RegisterType<ITheaterService, TheaterService>();
            container.RegisterType<IItemService, ItemService>();
            container.RegisterType<IFanZoneAdminService, FanZoneAdminService>();
            container.RegisterType<ITheaterAdminService, TheaterAdminService>();
            container.RegisterType<IFriendRequestService, FriendRequestService>();
            container.RegisterType<IUserItemService, UserItemService>();
            container.RegisterType<IBidService, BidService>();
            container.RegisterType<IUploadService, UploadService>();
        }
    }
}
