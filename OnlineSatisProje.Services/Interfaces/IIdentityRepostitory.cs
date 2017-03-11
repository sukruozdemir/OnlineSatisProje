using Microsoft.Owin.Security;
using OnlineSatisProje.Data.Identity;

namespace OnlineSatisProje.Services.Interfaces
{
    public interface IIdentityRepostitory
    {
        ApplicationSignInManager SignInManager { get; }
        ApplicationUserManager UserManager { get; }
        IAuthenticationManager AuthManager { get; }
    }
}