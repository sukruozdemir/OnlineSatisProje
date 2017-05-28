using System.Linq;
using System.Web.Mvc;
using OnlineSatisProje.Core.Entities;
using OnlineSatisProje.Data;
using OnlineSatisProje.Services.Interfaces;

namespace OnlineSatisProje.Web.Controllers
{
    public class HomeController : BaseController
    {
        #region Ctor

        public HomeController(IRepository<Urun> repositoryUrun,
            IUrunRepository urunRepository,
            ISaticiRepository saticiRepository,
            IRepository<Resim> resimRepository)
        {
            _repositoryUrun = repositoryUrun;
            _urunRepository = urunRepository;
            _saticiRepository = saticiRepository;
            _resimRepository = resimRepository;
        }

        #endregion

        #region Alanlar

        private readonly IRepository<Urun> _repositoryUrun;
        private readonly IRepository<Resim> _resimRepository;
        private readonly ISaticiRepository _saticiRepository;
        private readonly IUrunRepository _urunRepository;

        #endregion

        #region Actionlar

        public ActionResult Index()
        {
            ViewData["UrunListe"] = _urunRepository.GetHomePageProducts();
            ViewData["SaticiListe"] = _saticiRepository.GetAvailableSaitiSaticis();
            ViewData["Resimler"] = _resimRepository.Table.ToList();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        #endregion
    }
}