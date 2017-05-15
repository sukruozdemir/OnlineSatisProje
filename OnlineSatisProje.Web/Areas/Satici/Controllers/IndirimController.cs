using System.Linq;
using System.Web.Mvc;
using OnlineSatisProje.Core.Entities;
using OnlineSatisProje.Data;

namespace OnlineSatisProje.Web.Areas.Satici.Controllers
{
    public class IndirimController : BaseController
    {

        private readonly IRepository<Indirim> _inidirimRepository;
        private readonly IRepository<SaticiIndirimMapping> _saticiIndirimRepository;

        public IndirimController(IRepository<Indirim> inidirimRepository,
            IRepository<SaticiIndirimMapping> saticiIndirimRepository)
        {
            _inidirimRepository = inidirimRepository;
            _saticiIndirimRepository = saticiIndirimRepository;
        }

        // GET: Satici/Indirim
        public ActionResult Index()
        {
            var indirimler = _saticiIndirimRepository.Table.Where(i => i.SaticiId == CurrentSatici.Id).ToList();
            return View(indirimler);
        }
    }
}