using System.Web.Mvc;

namespace OnlineSatisProje.Web.Controllers
{
    [Authorize]
    public class SepetController : Controller
    {
        // GET: Sepet
        public ActionResult Index()
        {
            return View();
        }
    }
}