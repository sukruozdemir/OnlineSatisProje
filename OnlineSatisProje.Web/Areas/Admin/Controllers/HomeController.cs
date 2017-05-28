using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using OnlineSatisProje.Core.Entities;
using OnlineSatisProje.Data;
using OnlineSatisProje.Services.Interfaces;

namespace OnlineSatisProje.Web.Areas.Admin.Controllers
{
    /// <summary>
    ///     Represents the home page
    /// </summary>
    public class HomeController : BaseController
    {
        private readonly IRepository<Adres> _adresRepository;
        private readonly IRepository<Etiket> _etiketRepository;
        private readonly IRepository<Kategori> _kategoriRepository;
        private readonly IRepository<Kullanici> _kullaniciRepository;
        private readonly IRepository<Resim> _resimRepository;
        private readonly IRepository<IdentityRole> _roleRepository;
        private readonly IRepository<Core.Entities.Satici> _saticiRepository;
        private readonly IRepository<Siparis> _siparisRepository;
        private readonly IRepository<Urun> _urunRepository;

        /// <summary>
        ///     Ctor
        /// </summary>
        /// <param name="kullaniciRepository"></param>
        /// <param name="urunRepository"></param>
        /// <param name="kategoriRepository"></param>
        /// <param name="etiketRepository"></param>
        /// <param name="identityRepostitory"></param>
        /// <param name="roleRepository"></param>
        /// <param name="resimRepository"></param>
        /// <param name="siparisRepository"></param>
        /// <param name="adresRepository"></param>
        /// <param name="saticiRepository"></param>
        public HomeController(IRepository<Kullanici> kullaniciRepository,
            IRepository<Urun> urunRepository,
            IRepository<Kategori> kategoriRepository,
            IRepository<Etiket> etiketRepository,
            IIdentityRepostitory identityRepostitory,
            IRepository<IdentityRole> roleRepository,
            IRepository<Resim> resimRepository,
            IRepository<Siparis> siparisRepository,
            IRepository<Adres> adresRepository,
            IRepository<Core.Entities.Satici> saticiRepository)
        {
            _kullaniciRepository = kullaniciRepository;
            _urunRepository = urunRepository;
            _kategoriRepository = kategoriRepository;
            _etiketRepository = etiketRepository;
            _roleRepository = roleRepository;
            _resimRepository = resimRepository;
            _siparisRepository = siparisRepository;
            _adresRepository = adresRepository;
            _saticiRepository = saticiRepository;
        }

        /// <summary>
        ///     // GET: Admin/Home
        /// </summary>
        /// <returns>View</returns>
        public ActionResult Index()
        {
            var saticiRol = _roleRepository.Table.FirstOrDefault(x => x.Name == "Satıcı");
            if (saticiRol != null)
            {
                var list = _kullaniciRepository.Table.ToList().Where(k => k.Roles.Any(a => a.RoleId == saticiRol.Id));
            }

            ViewData["TotalSatici"] = _saticiRepository.Table.ToList().Count;
            ViewData["TotalUser"] = _kullaniciRepository.Table.ToList().Count;
            ViewData["TotalProduct"] = _urunRepository.Table.ToList().Count;
            ViewData["TotalCategory"] = _kategoriRepository.Table.ToList().Count;
            ViewData["TotalTag"] = _etiketRepository.Table.ToList().Count;
            ViewData["TotalPicture"] = _resimRepository.Table.ToList().Count;
            ViewData["TotalOrder"] = _siparisRepository.Table.ToList().Count;
            ViewData["TotalAddress"] = _adresRepository.Table.ToList().Count;

            return View();
        }
    }
}