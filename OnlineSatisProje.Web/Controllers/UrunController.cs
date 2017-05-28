using System.Net;
using System.Linq;
using System.Web.Mvc;
using OnlineSatisProje.Services.Interfaces;
using PagedList;

namespace OnlineSatisProje.Web.Controllers
{
    public class UrunController : BaseController
    {
        private readonly IUrunRepository _urunRepository;

        public UrunController(
            IUrunRepository urunRepository
            )
        {
            _urunRepository = urunRepository;
        }

        // GET: Urun
        public ActionResult Index()
        {
            return RedirectToAction("Urunler");
        }

        public ActionResult Detay(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var urun = _urunRepository.GetDiscountPrice(id.Value);
            if (urun == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            if (!_urunRepository.IsAvailable(urun))
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            return View(urun);
        }

        public ActionResult Urunler(int sayfa = 1, int kategoriId = 0, int saticiId = 0)
        {
            var liste = _urunRepository.GetAvailableProductsWithDiscount();

            if (kategoriId > 0)
            {
                liste = _urunRepository.GetUrunsByCategoryId(kategoriId);
            }

            if (saticiId > 0)
            {
                liste = liste.Where(u => u.SaticiId == saticiId).ToList();
            }

            return View(liste.OrderByDescending(l => l.CreatedDate).ToPagedList(sayfa, 9));
        }
    }
}