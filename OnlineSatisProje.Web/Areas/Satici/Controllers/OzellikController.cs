using System.Linq;
using System.Web.Mvc;
using OnlineSatisProje.Core.Entities;
using OnlineSatisProje.Data;

namespace OnlineSatisProje.Web.Areas.Satici.Controllers
{
    /// <summary>
    /// Ürün özelllik
    /// </summary>
    public class OzellikController : BaseController
    {
        private readonly IRepository<UrunOzellik> _urunOzellikRepository;

        public OzellikController(IRepository<UrunOzellik> urunOzellikRepository)
        {
            _urunOzellikRepository = urunOzellikRepository;
        }

        // GET: Satici/Ozellik
        public ActionResult Index()
        {
            return View(_urunOzellikRepository.Table.ToList());
        }

        [HttpPost]
        public ActionResult Ekle(UrunOzellik urunOzellik)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Özellik eklenemedi!");
                return PartialView("_PartialOzellikEkle");
            }

            _urunOzellikRepository.Insert(urunOzellik);

            return RedirectToAction("Index");
        }
    }
}