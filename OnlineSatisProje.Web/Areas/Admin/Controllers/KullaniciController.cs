using System.Linq;
using System.Web.Mvc;
using OnlineSatisProje.Core.Entities;
using OnlineSatisProje.Data;

namespace OnlineSatisProje.Web.Areas.Admin.Controllers
{
    public class KullaniciController : BaseController
    {
        private readonly IRepository<Kullanici> _repository;

        public KullaniciController(IRepository<Kullanici> repository)
        {
            _repository = repository;
        }

        // GET: Admin/Kullanici
        public ActionResult Index()
        {
            return View(_repository.Table.ToList());
        }
    }
}