using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using log4net;
using OnlineSatisProje.Core.Entities;
using OnlineSatisProje.Data;
using OnlineSatisProje.Web.Models;

namespace OnlineSatisProje.Web.Controllers
{
    [Authorize]
    public class HesabimController : BaseController
    {
        private readonly IRepository<Adres> _adresRepository;
        private readonly IRepository<Ilce> _ilceRepository;
        private readonly IRepository<KullaniciAdresMapping> _kullaniciAdresRepository;
        private readonly IRepository<Resim> _resimRepository;
        private readonly IRepository<Satici> _saticiRepository;
        private readonly IRepository<Sehir> _sehirRepository;
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);


        public HesabimController(IRepository<KullaniciAdresMapping> kullaniciAdresRepository,
            IRepository<Adres> adresRepository,
            IRepository<Ilce> ilceRepository,
            IRepository<Sehir> sehirRepository,
            IRepository<Satici> saticiRepository,
            IRepository<Resim> resimRepository)
        {
            _kullaniciAdresRepository = kullaniciAdresRepository;
            _adresRepository = adresRepository;
            _ilceRepository = ilceRepository;
            _sehirRepository = sehirRepository;
            _saticiRepository = saticiRepository;
            _resimRepository = resimRepository;
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
            return View(CurrentUser.Siparis.Where(x => !x.Silindi).OrderByDescending(x => x.Tarih).ToList());
        }

        public ActionResult SaticiProfil()
        {
            if (!Satici)
                return RedirectToAction("Index");
            var satici = _saticiRepository.Table.FirstOrDefault(s => s.KullaniciId == CurrentUser.Id);
            if (satici?.LogoId == null) return View(satici);
            var saticiResim = _resimRepository.GetById(satici.LogoId);
            ViewData["SaticiResim"] = saticiResim;
            return View(satici);
        }

        [HttpPost]
        public ActionResult SaticiResimUpload(HttpPostedFileBase saticiresim)
        {
            if (saticiresim == null) throw new ArgumentNullException(nameof(saticiresim));
            if (!Satici) return RedirectToAction("Index");

            try
            {
                if (saticiresim.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(saticiresim.FileName);
                    if (fileName != null)
                    {
                        const string directoryPath = "~/Content/Images/SaticiProfilUploads";
                        if (!Directory.Exists(Server.MapPath(directoryPath)))
                            Directory.CreateDirectory(Server.MapPath(directoryPath));

                        var mapFileName = DateTime.Now.ToString("MM/dd/yyyy");
                        var fName = mapFileName + "-" + Guid.NewGuid().ToString().Substring(0, 5) + "-" + fileName;
                        var fullPath = directoryPath + "/" + fName;
                        var filePath = Path.Combine(Server.MapPath(fullPath));
                        saticiresim.SaveAs(filePath);
                        var str = fullPath.Substring(1);
                        
                        var resim = new Resim
                        {
                            ResimBinary = null,
                            ResimPath = str,
                            AltAttr = "",
                            TitleAttr = "",
                            Baslik = Path.GetFileName(saticiresim.FileName),
                            CreatedDate = DateTime.Now
                        };
                        _resimRepository.Insert(resim);
                        var satici = _saticiRepository.Table.FirstOrDefault(x => x.KullaniciId == CurrentUser.Id);
                        if (satici != null)
                        {
                            satici.UpdatedDate = DateTime.Now;
                            satici.LogoId = resim.Id;
                            _saticiRepository.Update(satici);
                            return RedirectToAction("SaticiProfil");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
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
            var liste = _ilceRepository.Table.Where(x => x.SehirId == id)
                .Select(x => new
                {
                    id = x.Id,
                    ad = x.Ad
                })
                .ToList();
            return Json(liste, JsonRequestBehavior.AllowGet);
        }
    }
}