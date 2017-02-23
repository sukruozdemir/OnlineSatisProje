using System.Web.Mvc;
using OnlineSatisProje.Core.Entities;
using OnlineSatisProje.Data;

namespace OnlineSatisProje.Web.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IRepository<Kullanici> _repository;

        public HomeController(IRepository<Kullanici> repository)
        {
            _repository = repository;
        }

        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}