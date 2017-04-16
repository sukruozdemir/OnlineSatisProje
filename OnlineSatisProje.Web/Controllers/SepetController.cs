using System.Web.Mvc;

namespace OnlineSatisProje.Web.Controllers
{
    [Authorize]
    public class SepetController : BaseController
    {
        // GET: Sepet
        public ActionResult Index()
        {
            return View();
        }
    }
}