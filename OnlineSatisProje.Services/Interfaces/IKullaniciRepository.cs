using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using OnlineSatisProje.Core.Entities;
using OnlineSatisProje.Data.Identity;

namespace OnlineSatisProje.Services.Interfaces
{
    public interface IKullaniciRepository
    {
        ApplicationSignInManager SignInManager { get; }
        ApplicationUserManager UserManager { get; }
        IAuthenticationManager AuthManager { get; }
        IEnumerable<Kullanici> GetAll();
        Task<SignInStatus> PasswordSignInAsync(string userName, string password, bool rememberMe);
        Task SignInAsync(Kullanici kullanici);
        bool Register(Kullanici kullanici, string password);
        void SignOut();
        List<string> GetUserRoles(object userId);
        Kullanici GetLoggedUser();
    }
}