using System;
using System.Linq;
using System.Web.Mvc;
using OnlineSatisProje.Core.Entities;
using OnlineSatisProje.Data;

namespace OnlineSatisProje.Web.Areas.Admin.Controllers
{
    public class IlceController : Controller
    {
        private readonly IRepository<Ilce> _ilceRepository;
        private readonly IRepository<Sehir> _sehirRepository;

        public IlceController(IRepository<Sehir> sehirRepository,
            IRepository<Ilce> ilceRepository)
        {
            _sehirRepository = sehirRepository;
            _ilceRepository = ilceRepository;
        }

        // GET: Admin/Ilce
        public ActionResult Index()
        {
            var liste = _ilceRepository.Table.OrderBy(i => i.Ad).ToList();
            ViewData["Sehirler"] = new SelectList(_sehirRepository.Table.OrderBy(s => s.Ad).ToList(), "Id", "Ad");
            return View(liste);
        }

        [HttpPost]
        public ActionResult Ekle(Ilce ilce)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", @"İlçe eklenemedi");
                return RedirectToAction("Index");
            }
            ilce.CreatedDate = DateTime.Now;
            _ilceRepository.Insert(ilce);
            return RedirectToAction("Index");
        }
    }
}