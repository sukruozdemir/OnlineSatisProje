﻿using OnlineSatisProje.Core.Entities;
using OnlineSatisProje.Data;
using OnlineSatisProje.Web.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineSatisProje.Web.Areas.Admin.Controllers
{
    public class KategoriController : BaseController
    {
        private readonly IRepository<Kategori> _kategoriRepository;

        public KategoriController(IRepository<Kategori> kategoriRepo)
        {
            _kategoriRepository = kategoriRepo;
        }

        /// <summary>
        /// GET: Satici/Kategori
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View(_kategoriRepository.Table.ToList());
        }

        [HttpPost]
        public ActionResult Ekle(KategoriModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            var date = DateTime.Now;
            _kategoriRepository.Insert(new Kategori
            {
                Ad = model.Ad,
                Aciklama = model.Aciklama,
                Aktif = model.Aktif,
                ResimId = 0,
                AnaKategoriId = 0,
                CreatedDate = date,
                UpdatedDate = date
            });
            return RedirectToAction("Index");
        }
    }
}