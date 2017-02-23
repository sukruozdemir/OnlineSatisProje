using System.Web.Mvc;

namespace OnlineSatisProje.Web.Areas.Satici.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Satici/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}