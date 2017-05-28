using System;
using System.Linq;
using System.Web.Mvc;
using OnlineSatisProje.Core.Entities;
using OnlineSatisProje.Data;

namespace OnlineSatisProje.Web.Areas.Admin.Controllers
{
    public class SehirController : Controller
    {
        private readonly IRepository<Sehir> _sehirRepository;

        public SehirController(IRepository<Sehir> sehirRepository)
        {
            _sehirRepository = sehirRepository;
        }

        // GET: Admin/Sehir
        public ActionResult Index()
        {
            return View(_sehirRepository.Table.OrderBy(s => s.Ad).ToList());
        }

        [HttpPost]
        public ActionResult Ekle(Sehir sehir)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", @"Şehir eklenemedi");
                return RedirectToAction("Index");
            }

            sehir.CreatedDate = DateTime.Now;
            _sehirRepository.Insert(sehir);
            return RedirectToAction("Index");
        }

    }
}