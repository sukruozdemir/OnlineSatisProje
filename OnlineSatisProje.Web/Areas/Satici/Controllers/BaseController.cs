using System.Linq;
using System.Web.Mvc;
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
        public Kullanici CurrentKullanici { get; set; }
        public Core.Entities.Satici CurrentSatici { get; set; }

        public BaseController()
        {

        }

        public BaseController(IIdentityRepostitory identityRepostitory, IRepository<Core.Entities.Satici> saticiRepository)
        {
            CurrentKullanici = identityRepostitory.UserManager.FindById(User.Identity.GetUserId());
            CurrentSatici = saticiRepository.Table.FirstOrDefault(x => x.KullaniciId == CurrentKullanici.Id);
        }
    }
}