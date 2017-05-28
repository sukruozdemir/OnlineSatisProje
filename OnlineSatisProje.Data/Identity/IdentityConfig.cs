using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataProtection;
using OnlineSatisProje.Core.Entities;

namespace OnlineSatisProje.Data.Identity
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class ApplicationUserStore : UserStore<Kullanici>
    {
        // ReSharper disable once SuggestBaseTypeForParameter
        public ApplicationUserStore(IDbContext context)
            : base((DbContext) context)
        {
        }
    }

    public class ApplicationRoleStore : RoleStore<IdentityRole>
    {
        public ApplicationRoleStore(IDbContext context) : base((DbContext) context)
        {
        }
    }

    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
    public class ApplicationUserManager : UserManager<Kullanici>
    {
        public ApplicationUserManager(IUserStore<Kullanici> store, IDataProtectionProvider dataProtectionProvider)
            : base(store)
        {
            UserValidator = new UserValidator<Kullanici>(this)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true
            };


            UserLockoutEnabledByDefault = false;

            UserTokenProvider =
                new DataProtectorTokenProvider<Kullanici>(dataProtectionProvider.Create("ASP.NET Identity"));
        }
    }

    // Configure the application sign-in manager which is used in this application.
    public class ApplicationSignInManager : SignInManager<Kullanici, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager,
            IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(Kullanici user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager) UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options,
            IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(),
                context.Authentication);
        }
    }
}