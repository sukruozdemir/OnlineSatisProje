using System;
using System.Linq;
using System.Web.Mvc;
using OnlineSatisProje.Core.Entities;
using OnlineSatisProje.Core.Enums;
using OnlineSatisProje.Data;
using OnlineSatisProje.Web.Models;

namespace OnlineSatisProje.Web.Controllers
{
    [Authorize]
    public class SepetController : BaseController
    {
        private readonly IRepository<SepetItem> _sepetRepository;
        private readonly IRepository<SiparisItem> _siparisItemRepository;
        private readonly IRepository<Siparis> _siparisRepository;

        public SepetController(IRepository<Siparis> siparisRepository,
            IRepository<SiparisItem> siparisItemRepository, IRepository<SepetItem> sepetRepository)
        {
            _siparisRepository = siparisRepository;
            _siparisItemRepository = siparisItemRepository;
            _sepetRepository = sepetRepository;
        }

        /// <summary>
        /// </summary>
        /// <returns>View</returns>
        public ActionResult Index()
        {
            var liste = CurrentUser.SepetItem.Where(s => s.Aktif).ToList();
            return View(liste);
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
                Tarih = DateTime.Now
            };

            try
            {
                _siparisRepository.Insert(siparis);
                var sepetListe = CurrentUser.SepetItem;
                foreach (var sepetItem in sepetListe)
                {
                    var siparisItem = new SiparisItem
                    {
                        SiparisItemGuid = Guid.NewGuid(),
                        SiparisId = siparis.Id,
                        UrunId = sepetItem.UrunId,
                        Miktar = sepetItem.Miktar,
                        Fiyat = sepetItem.Urun.Fiyat,
                        IndirimMiktari = 0
                    };
                    _siparisItemRepository.Insert(siparisItem);
                    sepetItem.Aktif = false;
                    _sepetRepository.Update(sepetItem);
                }

            }
            catch (Exception)
            {
                ModelState.AddModelError("", @"Sipariş sırasında bir hata oluştu");
            }

            return View(model);
        }
    }
}