using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using OnlineSatisProje.Core.Entities;
using OnlineSatisProje.Data;
using OnlineSatisProje.Services.Interfaces;
using OnlineSatisProje.Web.Areas.Satici.Models;

namespace OnlineSatisProje.Web.Areas.Satici.Controllers
{
    public class UrunController : BaseController
    {
        private readonly IRepository<Urun> _repository;
        private readonly IRepository<Resim> _resimRepository;
        private readonly IUrunRepository _urunRepository;
        private readonly IRepository<UrunResimMapping> _urunResimRepository;

        public UrunController(IUrunRepository urunRepository,
            IRepository<Urun> repository,
            IRepository<Resim> resimRepository,
            IRepository<UrunResimMapping> urunResimRepository)
        {
            _urunRepository = urunRepository;
            _repository = repository;
            _resimRepository = resimRepository;
            _urunResimRepository = urunResimRepository;
        }

        // GET: Satici/Urun
        public ActionResult Index()
        {
            var list = _urunRepository.GetAll();
            return View(list);
        }

        // GET: Satici/Urun/Ekle
        [HttpGet]
        public ActionResult Ekle()
        {
            return View();
        }

        // POST: Satiici/Urun/Ekle
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Ekle(UrunModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Ürün eklenemedi!");
                return View(model);
            }
            var date = DateTime.Now;
            var urun = new Urun
            {
                Baslik = model.Baslik,
                Aktif = true,
                KisaAciklama = model.KisaAciklama,
                TamAciklama = model.UzunAciklama,
                AnasayfadaGoster = model.AnasayfadaGoster,
                KullaniciYorumIzinVer = model.YorumIzinVer,
                KargoAktif = model.KargoAktif,
                UcretsizKargo = model.UcretsizKargo,
                Fiyat = model.Fiyat,
                Silindi = false,
                Yayinlandi = model.Yayinlandi,
                CreatedDate = date,
                UpdatedDate = date
            };

            try
            {
                _repository.Insert(urun);
                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("", "Ürün eklenemedi!");
                return View(model);
            }
        }

        // GET: Satici/Urun/ResimEkle?urunId=2
        [HttpGet]
        public ActionResult ResimEkle(int? urunId)
        {
            if (urunId == null)
                return HttpNotFound();
            ViewBag.ResimEkleUrunId = urunId;
            return View();
        }

        // POST: Satici/Urun/ResimEkle
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResimEkle(HttpPostedFileBase upload, string resimekleurunid)
        {
            try
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(upload.FileName);
                    if (fileName != null)
                    {
                        // DOSYAYI KAYDET
                        var mapFileName = DateTime.Now.ToString("MM.dd.yyyy");
                        var filePath = Path.Combine(Server.MapPath("~/Content/Images/UrunUploads/"),
                            mapFileName + "-" + Guid.NewGuid().ToString().Substring(0, 5) + "-" + fileName);
                        upload.SaveAs(filePath);

                        // DOSYAYININ BYTE DIZISINI AL
                        byte[] fileBytes;
                        using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                        using (var reader = new BinaryReader(fs))
                        {
                            fileBytes = reader.ReadBytes((int) fs.Length);
                        }
                        // SAVE
                        var resim = new Resim
                        {
                            Baslik = fileName,
                            CreatedDate = DateTime.Now,
                            ResimBinary = fileBytes,
                            AltAttr = "",
                            TitleAttr = ""
                        };
                        _resimRepository.Insert(resim);

                        if (resimekleurunid != null)
                            _urunResimRepository.Insert(new UrunResimMapping
                            {
                                UrunId = int.Parse(resimekleurunid),
                                ResimId = resim.Id
                            });

                        return RedirectToAction("Index", "Urun");
                    }
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Resim eklenemedi! ");
            }

            return View();
        }
    }
}