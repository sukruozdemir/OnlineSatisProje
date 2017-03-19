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


        public UrunController(IUrunRepository urunRepository, IRepository<Urun> repository,
            IRepository<Resim> resimRepository)
        {
            _urunRepository = urunRepository;
            _repository = repository;
            _resimRepository = resimRepository;
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
        public ActionResult ResimEkle(int urunId)
        {
            var model = new UrunResimModel
            {
                UrunId = urunId
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResimEkle(UrunResimModel model, HttpPostedFileBase upload)
        {
            try
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(upload.FileName);
                    if (fileName != null)
                    {
                        // DOSYAYI KAYDET
                        var filePath = Path.Combine(Server.MapPath("~/Content/Images/UrunUploads/"), fileName);
                        upload.SaveAs(filePath);

                        // DOSYAYININ BYTE DIZISINI AL
                        byte[] fileBytes;
                        using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                        using (var reader = new BinaryReader(fs))
                            fileBytes = reader.ReadBytes((int)fs.Length);

                        // SAVE
                        var resim = new Resim
                        {
                            Baslik = fileName,
                            CreatedDate = DateTime.Now,
                            ResimBinary = fileBytes
                        };
                        _resimRepository.Insert(resim);
                        return RedirectToAction("Index", "Urun");
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Resim eklenemedi! " + ex.Message);
            }

            return View(model);
        }

        //    var urun = _repository.GetById(urunId);
        //    }
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    {
        //    if (urunId == null)
        //{
        //public ActionResult ResimEkle(int? urunId)
        //[HttpGet]

        // GET: Satici/Urun/ResimEkle/2

        //    if (urun == null)
        //        return HttpNotFound("Ürün bulunamadı");

        //    var model = new UrunResimMapping
        //    {
        //        UrunId = urun.Id
        //    };

        //    return View(model);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult ResimEkle([Bind(Include = "UrunId")] UrunResimMapping model, HttpPostedFileBase upload)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            ModelState.AddModelError("", "Model hatası!");
        //            return View(model);
        //        }

        //        if (upload != null && upload.ContentLength > 0)
        //        {
        //            var resim = new Resim
        //            {
        //                Baslik = Path.GetFileName(upload.FileName),
        //                CreatedDate = DateTime.Now
        //            };
        //            using (var reader = new BinaryReader(upload.InputStream))
        //            {
        //                resim.ResimBinary = reader.ReadBytes(upload.ContentLength);
        //            }
        //            _resimRepository.Insert(resim);
        //            //_urunRepository.ResimEkle(model.UrunId, resim);
        //            return RedirectToAction("ResimEkle", "Urun", new { urunId = model.UrunId });
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        ModelState.AddModelError("", "Resim eklenemedi!");
        //    }

        //    return View(model);
        //}
    }
}