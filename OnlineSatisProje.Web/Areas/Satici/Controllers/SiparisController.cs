using System.Linq;
using System.Web.Mvc;
using OnlineSatisProje.Core.Entities;
using OnlineSatisProje.Data;

namespace OnlineSatisProje.Web.Areas.Satici.Controllers
{
    public class SiparisController : BaseController
    {
        private readonly IRepository<Siparis> _siparisRepository;
        private readonly IRepository<SiparisItem> _siparisItemRepository;

        public SiparisController(IRepository<Siparis> siparisRepository,
            IRepository<SiparisItem> siparisItemRepository)
        {
            _siparisRepository = siparisRepository;
            _siparisItemRepository = siparisItemRepository;
        }

        // GET: Satici/Siparis
        public ActionResult Index()
        {
            var liste = _siparisItemRepository.Table.Where(x => x.SaticiId == CurrentSatici.Id && x.Siparis.Aktif && !x.Siparis.Silindi);
            return View(liste.ToList());
        }

    }
}