using System.Web.Mvc;
using System.Linq;
using OnlineSatisProje.Core.Entities;
using OnlineSatisProje.Data;
using OnlineSatisProje.Services.Interfaces;

namespace OnlineSatisProje.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IRepository<Urun> _repositoryUrun;
        private readonly IUrunRepository _urunRepository;
        private readonly ISaticiRepository _saticiRepository;

        public HomeController(IRepository<Urun> repositoryUrun,
            IUrunRepository urunRepository, 
            ISaticiRepository saticiRepository)
        {
            _repositoryUrun = repositoryUrun;
            _urunRepository = urunRepository;
            _saticiRepository = saticiRepository;
        }

        [OutputCache(Duration = 30, NoStore = true)]
        public ActionResult Index()
        {
            ViewData["UrunListe"] = _urunRepository.GetHomePageProducts();
            ViewData["SaticiListe"] = _saticiRepository.GetAvailableSaitiSaticis();
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
    }
}