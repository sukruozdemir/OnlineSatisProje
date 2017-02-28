using System;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using OnlineSatisProje.Core.Entities;
using OnlineSatisProje.Data;
using OnlineSatisProje.Services.Interfaces;
using OnlineSatisProje.Web.Areas.Admin.Models;

namespace OnlineSatisProje.Web.Areas.Admin.Controllers
{
    public class KullaniciController : BaseController
    {
        private readonly IRepository<Kullanici> _repository;
        private readonly IKullaniciRepository _kullaniciRepository;

        public KullaniciController(IRepository<Kullanici> repository, IKullaniciRepository kullaniciRepository)
        {
            _repository = repository;
            _kullaniciRepository = kullaniciRepository;
        }

        // GET: Admin/Kullanici
        [HttpGet]
        public ActionResult Index()
        {
            return View(_repository.Table.ToList());
        }

        // POST: Admin/Kullanici/Detay
        [HttpPost]
        public ActionResult Detay(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException(nameof(id));
            var kullanici = _kullaniciRepository.UserManager.FindById(id);
            var viewModel = new KullaniciViewModel
            {
                Kullanici = kullanici,
                Roles = _kullaniciRepository.GetUserRoles(kullanici.Id)
            };
            return PartialView("_PartialDetay", viewModel);
        }

        // POST: Admin/Kullanici/SilOnay
        [HttpPost]
        public ActionResult SilOnay(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException(nameof(id));
            var kullanici = _kullaniciRepository.UserManager.FindById(id);
            return PartialView("_PartialSil", kullanici);
        }

        // POST: Admin/Kullanici/Sil
        [HttpPost]
        public ActionResult Sil(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException(nameof(id));
            var kullanici = _kullaniciRepository.UserManager.FindById(id);
            kullanici.IsDeleted = true;
            kullanici.IsActive = false;
            _repository.Update(kullanici);
            return RedirectToAction("Index");
        }
    }
}