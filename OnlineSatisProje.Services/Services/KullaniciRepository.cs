using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using OnlineSatisProje.Core.Entities;
using OnlineSatisProje.Data;
using OnlineSatisProje.Services.Interfaces;

namespace OnlineSatisProje.Services.Services
{
    public class KullaniciRepository : IKullaniciRepository
    {
        private readonly IIdentityRepostitory _identityRepostitory;
        private readonly IRepository<Kullanici> _kullaniciRepository;

        public KullaniciRepository(IRepository<Kullanici> kullaniciRepository, IIdentityRepostitory identityRepostitory)
        {
            _kullaniciRepository = kullaniciRepository;
            _identityRepostitory = identityRepostitory;
        }

        public IEnumerable<Kullanici> GetAll()
        {
            return _kullaniciRepository.Table.ToList();
        }

        public async Task<SignInStatus> PasswordSignInAsync(string userName, string password, bool rememberMe)
        {
            var user = await _identityRepostitory.UserManager.FindAsync(userName, password);
            if (user.IsActive && !user.IsDeleted)
                return await _identityRepostitory.SignInManager.PasswordSignInAsync(userName, password, rememberMe,
                    false);
            return SignInStatus.Failure;
        }

        public async Task SignInAsync(Kullanici kullanici)
        {
            if (kullanici == null)
                throw new ArgumentNullException(nameof(kullanici));

            if (kullanici.IsActive && !kullanici.IsDeleted)
                await _identityRepostitory.SignInManager.SignInAsync(kullanici, false, false);
        }

        public bool Register(Kullanici kullanici, string password)
        {
            if (kullanici == null)
                throw new ArgumentNullException(nameof(kullanici));
            kullanici.CreatedDate = DateTime.Now;
            kullanici.UpdatedDate = kullanici.CreatedDate;
            kullanici.IsActive = true;
            kullanici.IsDeleted = false;

            var createResult = _identityRepostitory.UserManager.CreateAsync(kullanici, password);
            if (!createResult.Result.Succeeded)
                return false;

            _identityRepostitory.UserManager.AddToRole(kullanici.Id, "Standard");
            return true;
        }

        public void SignOut()
        {
            _identityRepostitory.AuthManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }

        public List<string> GetUserRoles(object userId)
        {
            var roles = _identityRepostitory.UserManager.GetRoles(userId.ToString());
            return (List<string>) roles;
        }

        public Kullanici GetLoggedUser()
        {
            return _identityRepostitory.UserManager.FindById(_identityRepostitory.AuthManager.User.Identity.GetUserId());
        }
    }
}