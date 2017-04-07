using System.Web.Mvc;
using System.Linq;
using OnlineSatisProje.Core.Entities;
using OnlineSatisProje.Data;

namespace OnlineSatisProje.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository<Urun> _urunRepository;

        public HomeController(IRepository<Urun> urunRepository)
        {
            _urunRepository = urunRepository;
        }

        public ActionResult Index()
        {

            ViewData["UrunListe"] = _urunRepository.Table.ToList();
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