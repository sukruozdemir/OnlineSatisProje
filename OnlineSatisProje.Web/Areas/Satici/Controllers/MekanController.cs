using System;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web.Mvc;
using log4net;
using OnlineSatisProje.Core.Entities;
using OnlineSatisProje.Data;
using OnlineSatisProje.Web.Areas.Satici.Models;

namespace OnlineSatisProje.Web.Areas.Satici.Controllers
{
    public class MekanController : BaseController
    {

        private readonly IRepository<SaticiMekanMapping> _saticiMekanRepository;
        private readonly IRepository<Sehir> _sehirRepository;
        private readonly IRepository<Ilce> _ilceRepository;
        private readonly IRepository<Adres> _adresRepository;
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);


        public MekanController(IRepository<SaticiMekanMapping> saticiMekanRepository,
            IRepository<Sehir> sehirRepository,
            IRepository<Ilce> ilceRepository,
            IRepository<Adres> adresRepository)
        {
            _saticiMekanRepository = saticiMekanRepository;
            _sehirRepository = sehirRepository;
            _ilceRepository = ilceRepository;
            _adresRepository = adresRepository;
        }

        // GET: Satici/Mekan
        public ActionResult Index()
        {
            var liste = _saticiMekanRepository.Table.ToList();
            ViewBag.Sehirler = _sehirRepository.Table.OrderBy(x => x.Ad).ToList();
            return View(liste);
        }

        [HttpPost]
        public ActionResult Ekle(SaticiMekanModel model)
        {
            if (!ModelState.IsValid)
            {
                Logger.Error("Model is not valid!");
                ModelState.AddModelError("", @"Hata");
                return RedirectToAction("Index");
            }
            try
            {
                var date = DateTime.Now;
                var adres = new Adres
                {
                    Baslik = model.Baslik,
                    Adres1 = model.Adres,
                    IlceId = model.IlceId,
                    Telefon = model.Telefon,
                    CreatedDate = date,
                    Aktif = true
                };
                _adresRepository.Insert(adres);

                var saticimekan = new SaticiMekanMapping
                {
                    SaticiId = CurrentSatici.Id,
                    AdresId = adres.Id,
                    Aciklama = model.Aciklama,
                    CreatedDate = date,
                    UpdatedDate = date,
                    Silindi = false,
                    Aktif = true
                };
                _saticiMekanRepository.Insert(saticimekan);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", @"Mekan eklenemedi");
                Logger.Error(e.Message);
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Sil(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            var item = _saticiMekanRepository.GetById(id);
            if (item == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            try
            {
                item.Aktif = false;
                item.Silindi = true;
                item.Adres.Aktif = false;
                _saticiMekanRepository.Update(item);
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Aktif(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            var item = _saticiMekanRepository.GetById(id);
            if (item == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            try
            {
                item.Aktif = true;
                item.Silindi = false;
                item.Adres.Aktif = true;
                _saticiMekanRepository.Update(item);
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
            }

            return RedirectToAction("Index");
        }

        public JsonResult Ilceler(int id)
        {
            var liste = _ilceRepository.Table.Where(x => x.SehirId == id).Select(x => new
            {
                id = x.Id,
                ad = x.Ad
            }).ToList();
            return Json(liste, JsonRequestBehavior.AllowGet);
        }
    }
}