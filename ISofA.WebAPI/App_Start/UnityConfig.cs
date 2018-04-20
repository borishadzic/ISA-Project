using ISofA.DAL.Core;
using ISofA.DAL.Core.Domain;
using ISofA.DAL.Core.Pantries;
using ISofA.DAL.Persistence;
using ISofA.DAL.Persistence.Pantries;
using ISofA.SL.Implementations;
using ISofA.SL.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler;
using Microsoft.Owin.Security.DataHandler.Serializer;
using Microsoft.Owin.Security.DataProtection;
using System;
using System.Data.Entity;
using System.Security.Principal;
using System.Web;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace ISofA.WebAPI
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
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your type's mappings here.
            // container.RegisterType<IProductRepository, ProductRepository>();
            //container.RegisterType<IUserStore<ISofAUser>, UserStore<ISofAUser>>();
            //container.RegisterType<UserManager<ISofAUser>>();
            container.RegisterType<DbContext, ISofADbContext>();
            //container.RegisterType(typeof(ISecureDataFormat<>), typeof(SecureDataFormat<>));
            //container.RegisterType<ISecureDataFormat<AuthenticationTicket>, SecureDataFormat<AuthenticationTicket>>();
            //container.RegisterType<ISecureDataFormat<AuthenticationTicket>, TicketDataFormat>();
            //container.RegisterType<IDataSerializer<AuthenticationTicket>, TicketSerializer>();
            //container.RegisterType<IDataProtector>(
            //new InjectionFactory(c => new DpapiDataProtectionProvider().Create("ASP.NET Identity")));
            //container.RegisterType<ApplicationUserManager>();
            //container.RegisterType<AccountController>(new InjectionConstructor());

            container.RegisterType<IIdentity>(new InjectionFactory(u => HttpContext.Current.User.Identity));

            container.RegisterType<IUnitOfWork, UnitOfWork>();            
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
			container.RegisterType<IPlayService, PlayService>();
            container.RegisterType<IStageService, StageService>();
            container.RegisterType<ISeatService, SeatService>();
            container.RegisterType<IProjectionService, ProjectionService>();
            container.RegisterType<IVisitService, VisitService>();
            container.RegisterType<ITheaterService, TheaterService>();
            container.RegisterType<IItemService, ItemService>();
            container.RegisterType<IAdminService, AdminService>();
			container.RegisterType<IFriendRequestService, FriendRequestService>();
            container.RegisterType<IUserItemService, UserItemService>();
            container.RegisterType<IBidService, BidService>();
            container.RegisterType<IUploadService, UploadService>();
			container.RegisterType<IUserService, UserService>();
            container.RegisterType<IEmailService, EmailService>();
            container.RegisterType<IConfigService, ConfigService>();
            container.RegisterType<ISegmentService, SegmentService>();
        }
    }
}