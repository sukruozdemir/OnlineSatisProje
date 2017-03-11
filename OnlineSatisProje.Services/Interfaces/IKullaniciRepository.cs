using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using OnlineSatisProje.Core.Entities;

namespace OnlineSatisProje.Services.Interfaces
{
    public interface IKullaniciRepository
    {
        IEnumerable<Kullanici> GetAll();
        Task<SignInStatus> PasswordSignInAsync(string userName, string password, bool rememberMe);
        Task SignInAsync(Kullanici kullanici);
        bool Register(Kullanici kullanici, string password);
        void SignOut();
        List<string> GetUserRoles(object userId);
        Kullanici GetLoggedUser();
    }
}