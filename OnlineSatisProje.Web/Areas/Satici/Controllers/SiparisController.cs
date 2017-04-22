using System;
using System.Linq;
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
            // TODO: HATA
            try
            {
                _siparisItemRepository.Update(model.SiparisItem);
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