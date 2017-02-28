using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using OnlineSatisProje.Core.Entities;
using OnlineSatisProje.Data;
using OnlineSatisProje.Data.Identity;
using OnlineSatisProje.Services.Interfaces;

namespace OnlineSatisProje.Services.Services
{
    public class KullaniciRepository : IKullaniciRepository
    {
        private readonly IRepository<Kullanici> _kullaniciRepository;

        public KullaniciRepository(IRepository<Kullanici> kullaniciRepository,
            ApplicationSignInManager signInManager,
            ApplicationUserManager userManager,
            IAuthenticationManager authManager)
        {
            _kullaniciRepository = kullaniciRepository;
            SignInManager = signInManager;
            UserManager = userManager;
            AuthManager = authManager;
        }

        public IEnumerable<Kullanici> GetAll()
        {
            return _kullaniciRepository.Table.ToList();
        }

        public async Task<SignInStatus> PasswordSignInAsync(string userName, string password, bool rememberMe)
        {
            var user = await UserManager.FindAsync(userName, password);
            if (user.IsActive && !user.IsDeleted)
            {
                return await SignInManager.PasswordSignInAsync(userName, password, rememberMe, false);
            }
            return SignInStatus.Failure;
        }

        public async Task SignInAsync(Kullanici kullanici)
        {
            if (kullanici == null)
                throw new ArgumentNullException(nameof(kullanici));

            if (kullanici.IsActive && !kullanici.IsDeleted)
            {
                await SignInManager.SignInAsync(kullanici, false, false);
            }
        }

        public bool Register(Kullanici kullanici, string password)
        {
            if (kullanici == null)
                throw new ArgumentNullException(nameof(kullanici));
            kullanici.CreatedDate = DateTime.Now;
            kullanici.UpdatedDate = kullanici.CreatedDate;
            kullanici.IsActive = true;
            kullanici.IsDeleted = false;

            var createResult = UserManager.CreateAsync(kullanici, password);
            if (!createResult.Result.Succeeded)
                return false;

            UserManager.AddToRole(kullanici.Id, "Standard");
            return true;
        }

        public void SignOut()
        {
            AuthManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }

        public List<string> GetUserRoles(object userId)
        {
            var roles = UserManager.GetRoles(userId.ToString());
            return (List<string>)roles;
        }

        public ApplicationSignInManager SignInManager { get; }
        public ApplicationUserManager UserManager { get; }
        public IAuthenticationManager AuthManager { get; }

        public Kullanici GetLoggedUser()
        {
            return UserManager.FindById(AuthManager.User.Identity.GetUserId());
        }
    }
}