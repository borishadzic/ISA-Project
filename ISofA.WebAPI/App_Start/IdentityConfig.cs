using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using ISofA.WebAPI.Models;
using ISofA.DAL.Persistence;
using ISofA.DAL.Core.Domain;
using ISofA.WebAPI.Services;
using Microsoft.Owin.Security.DataProtection;

namespace ISofA.WebAPI
{
    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.

    public class ApplicationUserManager : UserManager<ISofAUser>
    {
        public ApplicationUserManager(IUserStore<ISofAUser> store)
            : base(store)
        {
            //var provider = new DpapiDataProtectionProvider("Sample");
            //UserTokenProvider = new DataProtectorTokenProvider<ISofAUser>(provider.Create("EmailConfirmation"));

        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var manager = new ApplicationUserManager(new UserStore<ISofAUser>(context.Get<ISofADbContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ISofAUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true                
            };
            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };
            manager.EmailService = new EmailService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<ISofAUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }
}
