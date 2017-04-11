using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.AspNet.Identity;
using OnlineSatisProje.Core.Attributes;
using OnlineSatisProje.Core.Entities;
using OnlineSatisProje.Data;
using OnlineSatisProje.Services.Interfaces;

namespace OnlineSatisProje.Web.Areas.Satici.Controllers
{
    [CustomAuthorize(Roles = "Satıcı")]
    public class BaseController : Controller
    {
        // SATICI CONTROLLERININ HEPSINDE ERISILECEK ALANLAR
        public Kullanici CurrentKullanici { get; set; }
        public Core.Entities.Satici CurrentSatici { get; set; }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // GET LOGGED USER-ID
            var userid = User.Identity.GetUserId();
            // RESOLVE
            var identityRepostitory = DependencyResolver.Current.GetService<IIdentityRepostitory>();
            var saticiRepository = DependencyResolver.Current.GetService<IRepository<Core.Entities.Satici>>();

            // SET INFO
            CurrentKullanici = identityRepostitory.UserManager.FindById(userid);
            CurrentSatici = saticiRepository.Table.FirstOrDefault(x => x.KullaniciId == CurrentKullanici.Id);
            base.OnActionExecuting(filterContext);
        }
    }
}