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
        private readonly IIdentityRepostitory _identityRepostitory;
        private readonly IKullaniciRepository _kullaniciRepository;
        private readonly IRepository<Kullanici> _repository;
        private readonly IRolRepository _roleRepository;

        public KullaniciController(IRepository<Kullanici> repository,
            IKullaniciRepository kullaniciRepository,
            IRolRepository roleRepository, IIdentityRepostitory identityRepostitory)
        {
            _repository = repository;
            _kullaniciRepository = kullaniciRepository;
            _roleRepository = roleRepository;
            _identityRepostitory = identityRepostitory;
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
            var kullanici = _identityRepostitory.UserManager.FindById(id);
            var viewModel = new KullaniciViewModel
            {
                Kullanici = kullanici,
                Roles = _kullaniciRepository.GetUserRoles(kullanici.Id)
            };
            return PartialView("_PartialDetay", viewModel);
        }

        // POST: Admin/Kullanici/Duzenle
        [HttpPost]
        public ActionResult Duzenle(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException(nameof(id));
            var kullanici = _identityRepostitory.UserManager.FindById(id);
            return PartialView("_PartialDuzenle", kullanici);
        }

        // POST: Admin/Kullanici/SilOnay
        [HttpPost]
        public ActionResult SilOnay(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException(nameof(id));
            var kullanici = _identityRepostitory.UserManager.FindById(id);
            return PartialView("_PartialSil", kullanici);
        }

        // POST: Admin/Kullanici/Sil
        [HttpPost]
        public ActionResult Sil(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException(nameof(id));
            var kullanici = _identityRepostitory.UserManager.FindById(id);
            kullanici.IsDeleted = true;
            kullanici.IsActive = false;
            kullanici.UpdatedDate = DateTime.Now;
            _repository.Update(kullanici);
            return RedirectToAction("Index");
        }

        // GET: Admin/Kullanici/RolEkle
        [HttpGet]
        public ActionResult RolEkle()
        {
            ViewBag.Roller = new SelectList(_roleRepository.GetRoles(), "Id", "Name");
            return View();
        }

        // POST: Admin/Kullanici/RolEkle
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RolEkle(KullaniciRolModel model)
        {
            ViewBag.Roller = new SelectList(_roleRepository.GetRoles(), "Id", "Name");
            if (ModelState.IsValid)
            {
                if (_roleRepository.RolEkle(model.Email, model.RolId)) return RedirectToAction("Index");

                ModelState.AddModelError("", "Rol eklerken bir hata oluştu");
                return View(model);
            }
            ModelState.AddModelError("", "Model Error");
            return View(model);
        }
    }
}