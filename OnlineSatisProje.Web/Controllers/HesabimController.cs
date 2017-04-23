﻿using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineSatisProje.Core.Entities;
using OnlineSatisProje.Data;
using OnlineSatisProje.Web.Models;

namespace OnlineSatisProje.Web.Controllers
{
    [Authorize]
    public class HesabimController : BaseController
    {
        private readonly IRepository<KullaniciAdresMapping> _kullaniciAdresRepository;
        private readonly IRepository<Adres> _adresRepository;
        private readonly IRepository<Ilce> _ilceRepository;
        private readonly IRepository<Sehir> _sehirRepository;
        private readonly IRepository<Satici> _saticiRepository;

        public HesabimController(IRepository<KullaniciAdresMapping> kullaniciAdresRepository,
            IRepository<Adres> adresRepository,
            IRepository<Ilce> ilceRepository,
            IRepository<Sehir> sehirRepository, 
            IRepository<Satici> saticiRepository)
        {
            _kullaniciAdresRepository = kullaniciAdresRepository;
            _adresRepository = adresRepository;
            _ilceRepository = ilceRepository;
            _sehirRepository = sehirRepository;
            _saticiRepository = saticiRepository;
        }

        public ActionResult Index()
        {
            var user = CurrentUser;
            var model = new HesapModel
            {
                Email = user.Email,
                KullaniciAdi = user.UserName,
                SaticiMi = Satici,
                CreatedDate = user.CreatedDate
            };
            return View(model);
        }

        public ActionResult Siparislerim()
        {
            return View(CurrentUser.Siparis.Where(x => !x.Silindi).OrderByDescending(x=>x.Tarih).ToList());
        }

        public ActionResult SaticiProfil()
        {
            if (!Satici)
            {
                return RedirectToAction("Index");
            }
            var satici = _saticiRepository.Table.FirstOrDefault(s => s.KullaniciId == CurrentUser.Id);
            return View(satici);
        }

        [HttpPost]
        public ActionResult SaticiResimUpload(HttpPostedFileBase saticiresim)
        {
            if(saticiresim == null) throw new ArgumentNullException(nameof(saticiresim));

            try
            {
                if (saticiresim.ContentLength > 0)
                {
                    
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("", @"Resim eklenirken bir hata oluştu");
            }

            return RedirectToAction("Index");
        }

        public ActionResult Adreslerim()
        {
            ViewBag.Sehirler = _sehirRepository.Table.OrderBy(x => x.Ad).ToList();
            var user = CurrentUser;
            var kullaniciAdresler = _kullaniciAdresRepository.Table.Where(k => k.KullaniciId == user.Id).ToList();
            return View(kullaniciAdresler);
        }

        [HttpPost]
        public ActionResult AdresEkle(Adres adres)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", @"Adres eklenemedi");
                return View("Adreslerim");
            }

            var user = CurrentUser;
            var kullaniciAdresler = _kullaniciAdresRepository.Table.Where(k => k.KullaniciId == user.Id).ToList();

            try
            {
                adres.CreatedDate = DateTime.Now;
                adres.Aktif = true;
                _adresRepository.Insert(adres);
                _kullaniciAdresRepository.Insert(new KullaniciAdresMapping
                {
                    AdresId = adres.Id,
                    KullaniciId = CurrentUser.Id
                });


                return RedirectToAction("Adreslerim", kullaniciAdresler);
            }
            catch
            {
                ModelState.AddModelError("", @"Adres eklenirken bir hata oluştu");
                return View("Adreslerim", kullaniciAdresler);
            }
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