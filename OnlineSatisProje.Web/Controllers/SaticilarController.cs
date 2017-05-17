using OnlineSatisProje.Core.Entities;
using OnlineSatisProje.Data;
using OnlineSatisProje.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace OnlineSatisProje.Web.Controllers
{
    public class SaticilarController : Controller
    {
        private readonly IRepository<Satici> _saticiRepository;
        private readonly IRepository<Resim> _resimRepository;

        public SaticilarController(IRepository<Satici> saticiRepository, IRepository<Resim> resimRepository)
        {
            _saticiRepository = saticiRepository;
            _resimRepository = resimRepository;
        }

        // GET: Satici
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Profil(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var satici = _saticiRepository.GetById(id);
            if (satici == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var model = new SaticiViewModel
            {
                Satici = satici
            };
            if (satici.LogoId > 0)
                model.SaticiResim = _resimRepository.GetById(satici.LogoId);
            else
                model.SaticiResim = null;
            return View(model);
        }
    }
}