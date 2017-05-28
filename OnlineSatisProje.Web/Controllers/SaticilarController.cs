using System.Linq;
using System.Net;
using System.Web.Mvc;
using OnlineSatisProje.Core.Entities;
using OnlineSatisProje.Data;
using OnlineSatisProje.Web.Models;
using PagedList;

namespace OnlineSatisProje.Web.Controllers
{
    public class SaticilarController : Controller
    {
        private readonly IRepository<Resim> _resimRepository;
        private readonly IRepository<Satici> _saticiRepository;

        public SaticilarController(IRepository<Satici> saticiRepository, IRepository<Resim> resimRepository)
        {
            _saticiRepository = saticiRepository;
            _resimRepository = resimRepository;
        }

        public ActionResult Index()
        {
            return RedirectToAction("Listele");
        }

        public ActionResult Profil(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var satici = _saticiRepository.GetById(id);
            if (!satici.Aktif || satici.Silindi)
                satici = null;

            if (satici == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var model = new SaticiViewModel
            {
                Satici = satici,
                SaticiResim = satici.LogoId > 0 ? _resimRepository.GetById(satici.LogoId) : null
            };
            return View(model);
        }

        public ActionResult Listele(int sayfa = 1)
        {
            var liste = _saticiRepository.Table.Where(s => s.Aktif || !s.Silindi).OrderBy(s => s.Ad).ToList();
            var viewListe = liste.Select(satici => new SaticiViewModel
            {
                Satici = satici,
                SaticiResim = _resimRepository.GetById(satici.LogoId) == null
                    ? null
                    : _resimRepository.GetById(satici.LogoId)
            }).ToList();

            return View(viewListe.ToPagedList(sayfa, 12));
        }
    }
}