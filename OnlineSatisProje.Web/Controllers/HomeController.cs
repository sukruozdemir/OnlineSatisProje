using System.Web.Mvc;
using System.Linq;
using OnlineSatisProje.Core.Entities;
using OnlineSatisProje.Data;
using OnlineSatisProje.Services.Interfaces;

namespace OnlineSatisProje.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository<Urun> _repositoryUrun;
        private readonly IUrunRepository _urunRepository;

        public HomeController(IRepository<Urun> repositoryUrun,
            IUrunRepository urunRepository)
        {
            _repositoryUrun = repositoryUrun;
            _urunRepository = urunRepository;
        }

        [OutputCache(Duration = 30)]
        public ActionResult Index()
        {
            ViewData["UrunListe"] = _urunRepository.GetHomePageProducts();
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