using System;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using log4net;
using OnlineSatisProje.Core.Entities;
using OnlineSatisProje.Core.Enums;
using OnlineSatisProje.Data;
using OnlineSatisProje.Web.Areas.Satici.Models;

namespace OnlineSatisProje.Web.Areas.Satici.Controllers
{
    public class SiparisController : BaseController
    {
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
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

            ViewBag.SiparisDurumList = EnumHelper.GetSelectList(typeof(SiparisDurumu));
            ViewBag.OdemeDurumList = EnumHelper.GetSelectList(typeof(OdemeDurumu));
            ViewBag.KargoDurumList = EnumHelper.GetSelectList(typeof(KargoDurumu));

            return View(liste.ToList());
        }

        [HttpPost]
        public ActionResult SiparisDuzenle(SiparisModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", @"Sipariş düzenlenirken bir hata oluştu");
                return RedirectToAction("Index");
            }
            try
            {
                var siparis = _siparisRepository.GetById(model.SiparisId);
                siparis.KargoDurumId = model.KargoDurumId;
                siparis.SiparisDurumId = model.SiparisDurumId;
                siparis.OdemeDurumId = model.OdemeDurumId;
                _siparisRepository.Update(siparis);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult SiparisTamamlandi(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            var siparis = _siparisRepository.GetById(id);
            if (siparis == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            try
            {
                siparis.Aktif = false;
                siparis.KargoDurumu = KargoDurumu.Iletildi;
                siparis.OdemeDurumu = OdemeDurumu.Odendi;
                siparis.SiparisDurumu = SiparisDurumu.Tamamlandi;
                _siparisRepository.Update(siparis);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
                return RedirectToAction("Index");
            }
        }
    }
}