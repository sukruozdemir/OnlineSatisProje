using System.Web.Mvc;

namespace OnlineSatisProje.Web.Areas.Satici.Controllers
{
    public class HomeController : BaseController
    {
        #region Ctor

        #endregion

        #region Actions

        // GET: Satici/Home
        public ActionResult Index()
        {
            return View();
        }

        #endregion
    }
}