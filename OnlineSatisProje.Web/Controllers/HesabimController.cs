using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using OnlineSatisProje.Core.Entities;
using OnlineSatisProje.Data;
using OnlineSatisProje.Services.Interfaces;
using OnlineSatisProje.Web.Models;

namespace OnlineSatisProje.Web.Controllers
{
    [Authorize]
    public class HesabimController : Controller
    {
        private readonly IIdentityRepostitory _identityRepostitory;
        private readonly IRepository<KullaniciAdresMapping> _kullaniciAdresRepository;

        public HesabimController(IIdentityRepostitory identityRepostitory, 
            IRepository<KullaniciAdresMapping> kullaniciAdresRepository)
        {
            _identityRepostitory = identityRepostitory;
            _kullaniciAdresRepository = kullaniciAdresRepository;
        }

        public ActionResult Index()
        {
            var user = GetCurrentUser();
            var model = new HesapModel
            {
                Email = user.Email,
                KullaniciAdi = user.UserName,
                SaticiMi = IsSatici(),
                CreatedDate = user.CreatedDate
            };
            return View(model);
        }

        public ActionResult Adreslerim()
        {
            var user = GetCurrentUser();
            var kullaniciAdresler = _kullaniciAdresRepository.Table.Where(k => k.KullaniciId == user.Id).ToList();
            return View(kullaniciAdresler);
        }


        private Kullanici GetCurrentUser()
        {
            var id = User.Identity.GetUserId();
            var user = _identityRepostitory.UserManager.FindById(id);
            return user;
        }

        private bool IsSatici()
        {
            return _identityRepostitory.UserManager.IsInRole(GetCurrentUser().Id, "Satıcı");
        }
    }
}