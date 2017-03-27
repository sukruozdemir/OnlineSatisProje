using System.Linq;
using System.Web.Mvc;
using OnlineSatisProje.Core.Entities;
using OnlineSatisProje.Data;

namespace OnlineSatisProje.Web.Areas.Admin.Controllers
{
    public class EtiketController : BaseController
    {
        private readonly IRepository<Etiket> _etiketRepository;

        public EtiketController(IRepository<Etiket> etiketRepository)
        {
            _etiketRepository = etiketRepository;
        }

        // GET: Admin/Etiket
        public ActionResult Index()
        {
            var etiketList = _etiketRepository.Table.ToList();
            return View(etiketList);
        }

        [HttpPost]
        public ActionResult Ekle() => RedirectToAction("Index");
    }
}