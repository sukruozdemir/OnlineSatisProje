using System;
using System.Linq;
using System.Web.Mvc;
using OnlineSatisProje.Core.Entities;
using OnlineSatisProje.Data;
using OnlineSatisProje.Web.Areas.Satici.Models;

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

        // GET: Satici/Indirim/Ekle
        public ActionResult Ekle()
        {
            var model = new IndirimModel
            {
                BaslangicTarihi = DateTime.Now.ToString("d"),
                BitisTarihi = DateTime.Now.ToString("d")
            };
            
            return View(model);
        }

        // POST: Satici/Indirim/Ekle
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Ekle(IndirimModel model)
        {
            if(!ModelState.IsValid)
            {
                ModelState.AddModelError("", @"Eklenirken bir hata oluştu");
                return RedirectToAction("Index");
            }
            try
            {
                var i = new Indirim
                {
                    Aktif = true,
                    BaslangicTarihi = Convert.ToDateTime(model.BaslangicTarihi),
                    BitisTarihi = Convert.ToDateTime(model.BitisTarihi),
                    IndirimMiktari = model.IndirimMiktari,
                    IndirimYuzdesi = model.IndirimYuzdesi,
                    Baslik = model.Baslik
                };
                _inidirimRepository.Insert(i);
                _saticiIndirimRepository.Insert(new SaticiIndirimMapping
                {
                    IndirimId = i.Id,
                    SaticiId = CurrentSatici.Id
                });
                return RedirectToAction("Index");

            }
            catch (Exception)
            {
                ModelState.AddModelError("", @"Indirim eklenemedi!");
                return View(model);
            }       
        }
    }
}