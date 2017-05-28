using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using OnlineSatisProje.Core.Entities;
using OnlineSatisProje.Services.Interfaces;

namespace OnlineSatisProje.Web.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!User.Identity.IsAuthenticated) return;
            _identityRepostitory = DependencyResolver.Current.GetService<IIdentityRepostitory>();
            CurrentUser = GetCurrentUser();
            Satici = IsSatici();
            ViewBag.CurrentUser = CurrentUser;
            base.OnActionExecuting(filterContext);
        }

        #region Alanlar

        protected Kullanici CurrentUser { get; set; }
        protected bool Satici { get; set; }

        private IIdentityRepostitory _identityRepostitory;

        #endregion

        #region Araçlar

        private Kullanici GetCurrentUser()
        {
            var id = User.Identity.GetUserId();
            var user = _identityRepostitory.UserManager.FindById(id);
            return user;
        }

        private bool IsSatici()
        {
            return CurrentUser != null && _identityRepostitory.UserManager.IsInRole(GetCurrentUser().Id, "Satıcı");
        }

        #endregion
    }
}