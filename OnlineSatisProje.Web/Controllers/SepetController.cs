using System;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web.Mvc;
using log4net;
using OnlineSatisProje.Core.Entities;
using OnlineSatisProje.Core.Enums;
using OnlineSatisProje.Data;
using OnlineSatisProje.Web.Models;

namespace OnlineSatisProje.Web.Controllers
{
    [Authorize]
    public class SepetController : BaseController
    {
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IRepository<SepetItem> _sepetRepository;
        private readonly IRepository<SiparisItem> _siparisItemRepository;
        private readonly IRepository<Siparis> _siparisRepository;
        private readonly IRepository<Urun> _urunRepository;

        public SepetController(IRepository<Siparis> siparisRepository,
            IRepository<SiparisItem> siparisItemRepository,
            IRepository<SepetItem> sepetRepository,
            IRepository<Urun> urunRepository)
        {
            _siparisRepository = siparisRepository;
            _siparisItemRepository = siparisItemRepository;
            _sepetRepository = sepetRepository;
            _urunRepository = urunRepository;
        }

        /// <summary>
        /// </summary>
        /// <returns>View</returns>
        public ActionResult Index()
        {
            var liste = CurrentUser.SepetItem.Where(s => s.Aktif).ToList();
            return View(liste);
        }

        [HttpPost]
        public ActionResult Ekle(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var urun = _urunRepository.GetById(id);
            if (urun == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            var sepet =
                _sepetRepository.Table.FirstOrDefault(x => x.UrunId == urun.Id && x.Aktif &&
                                                           x.KullaniciId == CurrentUser.Id);
            try
            {
                if (sepet != null)
                {
                    sepet.Miktar += 1;
                    sepet.UpdatedDate = DateTime.Now;
                    _sepetRepository.Update(sepet);
                }
                else
                {
                    var date = DateTime.Now;
                    var sepetitem = new SepetItem
                    {
                        Aktif = true,
                        CreatedDate = date,
                        UpdatedDate = date,
                        KullaniciId = CurrentUser.Id,
                        UrunId = urun.Id,
                        Miktar = 1
                    };
                    _sepetRepository.Insert(sepetitem);
                }
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// </summary>
        /// <returns>View</returns>
        public ActionResult Odeme()
        {
            var list = CurrentUser.SepetItem.Where(x => x.Aktif);
            var toplamucret = list.Sum(x => x.Urun.Fiyat * x.Miktar);
            if (toplamucret <= 0)
                return RedirectToAction("Index");

            var model = new OdemeModel
            {
                ToplamUcret = toplamucret,
                CurrentUser = CurrentUser
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Odeme(OdemeModel model)
        {
            model.CurrentUser = CurrentUser;
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", @"Alan bilgilerini kontrol ediniz");
                return View(model);
            }
            if (!(CurrentUser.SepetItem.Count > 0))
                return RedirectToAction("Index");

            var sepetListe = CurrentUser.SepetItem;
            var siparis = new Siparis
            {
                SiparisGuid = Guid.NewGuid(),
                KullaniciId = CurrentUser.Id,
                KargoAdresId = model.KargoAdresi,
                SiparisDurumu = SiparisDurumu.Bekliyor,
                OdemeDurumu = OdemeDurumu.Odendi,
                KargoDurumu = KargoDurumu.Bekliyor,
                Aktif = true,
                Silindi = false,
                KartAdi = model.KartAd,
                KartNumarasi = model.KartNumara,
                KartCvv = model.KartCvv,
                KartSonKullanimAy = model.KartSonKullanimAy,
                KartSonKullanimYil = model.KartSonKullanimYil,
                Tarih = DateTime.Now,
                SiparisToplam = sepetListe.Where(x => x.Aktif).Sum(x => x.Urun.Fiyat * x.Miktar)
            };

            try
            {
                _siparisRepository.Insert(siparis);
                foreach (var sepetItem in sepetListe.Where(x => x.Aktif))
                {
                    var siparisItem = new SiparisItem
                    {
                        SiparisItemGuid = Guid.NewGuid(),
                        SiparisId = siparis.Id,
                        UrunId = sepetItem.UrunId,
                        Miktar = sepetItem.Miktar,
                        Fiyat = sepetItem.Urun.Fiyat,
                        IndirimMiktari = 0,
                        SaticiId = sepetItem.Urun.SaticiId
                    };
                    _siparisItemRepository.Insert(siparisItem);
                    sepetItem.Aktif = false;
                    sepetItem.UpdatedDate = DateTime.Now;
                    _sepetRepository.Update(sepetItem);
                }
                return RedirectToAction("Siparislerim", "Hesabim");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", @"Sipariş sırasında bir hata oluştu");
            }

            return View(model);
        }
    }
}