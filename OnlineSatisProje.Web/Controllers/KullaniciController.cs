using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using OnlineSatisProje.Core.Entities;
using OnlineSatisProje.Web.Models;

namespace OnlineSatisProje.Web.Controllers
{
    [Authorize]
    public class KullaniciController : Controller
    {
        public KullaniciController()
        {
        }

        public KullaniciController(ApplicationUserManager userManager, ApplicationSignInManager signInManager,
            IAuthenticationManager authManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            AuthManager = authManager;
        }

        public ApplicationSignInManager SignInManager { get; }

        public ApplicationUserManager UserManager { get; }


        public IAuthenticationManager AuthManager { get; }

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
            if (!ModelState.IsValid)
                return View(model);

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
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

        //
        // POST: /Kullanici/KayitOl
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> KayitOl(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var user = new Kullanici {UserName = model.Email, Email = model.Email, CreatedDate = DateTime.Now};
            var result = await UserManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                UserManager.AddToRole(user.Id, "Standard");
                await SignInManager.SignInAsync(user, false, false);

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                return RedirectToAction("Index", "Home");
            }
            AddErrors(result);

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // POST: /Kullanici/Cikis
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cikis()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        #region Helpers

        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
                ModelState.AddModelError("", error);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; }
            public string RedirectUri { get; }
            public string UserId { get; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties {RedirectUri = RedirectUri};
                if (UserId != null)
                    properties.Dictionary[XsrfKey] = UserId;
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }

        #endregion
    }
}