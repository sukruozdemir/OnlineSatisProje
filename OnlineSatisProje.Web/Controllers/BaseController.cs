using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using OnlineSatisProje.Core.Entities;
using OnlineSatisProje.Services.Interfaces;

namespace OnlineSatisProje.Web.Controllers
{
    public class BaseController : Controller
    {
        private readonly IIdentityRepostitory _identityRepostitory;

        public BaseController()
        {
            _identityRepostitory = DependencyResolver.Current.GetService<IIdentityRepostitory>();
        }

        public Kullanici GetCurrentUser()
        {
            var id = User.Identity.GetUserId();
            var user = _identityRepostitory.UserManager.FindById(id);
            return user;
        }

        public bool IsSatici()
        {
            return _identityRepostitory.UserManager.IsInRole(GetCurrentUser().Id, "Satıcı");
        }
    }
}