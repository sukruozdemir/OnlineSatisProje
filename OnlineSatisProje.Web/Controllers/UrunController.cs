using System.Net;
using System.Web.Mvc;
using OnlineSatisProje.Core.Entities;
using OnlineSatisProje.Data;
using OnlineSatisProje.Services.Interfaces;

namespace OnlineSatisProje.Web.Controllers
{
    public class UrunController : BaseController
    {
        private readonly IRepository<Urun> _repositoryUrun;
        private readonly IUrunRepository _urunRepository;
        private readonly IRepository<SaticiIndirimMapping> _saticiIndirimRepository;
        private readonly IRepository<UrunIndirimMapping> _urunIndirimRepository;

        public UrunController(IRepository<Urun> repositoryUrun,
            IUrunRepository urunRepository,
            IRepository<SaticiIndirimMapping> saticiIndirimRepository,
            IRepository<UrunIndirimMapping> urunIndirimRepository)
        {
            _repositoryUrun = repositoryUrun;
            _urunRepository = urunRepository;
            _saticiIndirimRepository = saticiIndirimRepository;
            _urunIndirimRepository = urunIndirimRepository;
        }

        // GET: Urun
        public ActionResult Index()
        {
            return RedirectToAction("Liste");
        }

        public ActionResult Liste() => View(_urunRepository.GetHomePageProducts());

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
    }
}