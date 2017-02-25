using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using OnlineSatisProje.Core.Entities;
using OnlineSatisProje.Services.Interfaces;
using OnlineSatisProje.Web.Models;

namespace OnlineSatisProje.Web.Controllers
{
    [Authorize]
    public class KullaniciController : Controller
    {
        private readonly IKullaniciRepository _kullaniciRepository;

        public KullaniciController(IKullaniciRepository kullaniciRepository)
        {
            _kullaniciRepository = kullaniciRepository;
        }

        //
        // GET: /Kullanici/Giris
        [AllowAnonymous]
        public ActionResult Giris(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            ViewBag.ReturnUrl = returnUrl;

            return View();
        }

        //
        // POST: /Kullanici/Giris
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Giris(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid) return View(model);
            var result = await _kullaniciRepository.PasswordSignInAsync(model.Email, model.Password, model.RememberMe);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    break;
                case SignInStatus.RequiresVerification:
                    break;
                case SignInStatus.Failure:
                    break;
                default:
                    ModelState.AddModelError("", "Giriş yapılamadı");
                    return View(model);
            }

            return View(model);
        }

        //
        // GET: /Kullanici/KayitOl
        [AllowAnonymous]
        public ActionResult KayitOl()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            return View();
        }


        // POST: /Kullanici/KayitOl
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> KayitOl(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var kullanici = new Kullanici {UserName = model.Email, Email = model.Email};
            if (!_kullaniciRepository.Register(kullanici, model.Password))
                return View(model);

            await _kullaniciRepository.SignInAsync(kullanici);
            return RedirectToAction("Index", "Home", new {area = ""});
        }

        //
        // POST: /Kullanici/Cikis
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cikis()
        {
            _kullaniciRepository.SignOut();
            return RedirectToAction("Index", "Home");
        }

        #region Helpers

        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            return RedirectToAction("Index", "Home");
        }

        #endregion
    }
}