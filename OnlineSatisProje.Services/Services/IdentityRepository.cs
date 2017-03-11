using Microsoft.Owin.Security;
using OnlineSatisProje.Data.Identity;
using OnlineSatisProje.Services.Interfaces;

namespace OnlineSatisProje.Services.Services
{
    public class IdentityRepository : IIdentityRepostitory
    {
        public IdentityRepository(ApplicationSignInManager signInManager, ApplicationUserManager userManager,
            IAuthenticationManager authManager)
        {
            SignInManager = signInManager;
            UserManager = userManager;
            AuthManager = authManager;
        }

        public ApplicationSignInManager SignInManager { get; }
        public ApplicationUserManager UserManager { get; }
        public IAuthenticationManager AuthManager { get; }
    }
}