#region Usings

using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using OnlineSatisProje.Core.Entities;
using OnlineSatisProje.Data;
using OnlineSatisProje.Services.Interfaces;
using OnlineSatisProje.Web.ActionFilters;
using OnlineSatisProje.Web.Areas.Satici.CustomActions;
using OnlineSatisProje.Web.Areas.Satici.Models;

#endregion

namespace OnlineSatisProje.Web.Areas.Satici.Controllers
{
    public class UrunController : BaseController
    {
        #region Ctor

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

        #endregion

        #region Fields

        private readonly IRepository<Urun> _repository;
        private readonly IRepository<Resim> _resimRepository;
        private readonly IUrunRepository _urunRepository;
        private readonly IRepository<UrunResimMapping> _urunResimRepository;

        #endregion

        #region Actions

        /// <summary>
        ///     GET: Satici/Urun
        ///     Urun anasayfa görünümü
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var list = _urunRepository.GetAll();
            return View(list);
        }

        /// <summary>
        ///     GET: Satici/Urun/Ekle
        ///     View görünümü
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Ekle() => View();

        /// <summary>
        ///     POST: Satiici/Urun/Ekle
        ///     Veritabanına ürünü ekler.
        /// </summary>
        /// <param name="model">Ürün model</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Ekle(UrunModel model)
        {
            // MODEL VAR MI?
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            // MODEL DOGRULANDI MI?
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Ürün eklenemedi!");
                return View(model);
            }

            // URUNUN EKLENDIGI/GUNCELLENDIGI TARIH
            var date = DateTime.Now;

            // YENI BIR URUN NESNESI OLUSTUR VE MODELDEN GELEN VERILERI NESNENIN ALANLARINA SET ET.
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

            // URUNU VERITABANINA EKLE. HATA OLUSMASI SIRASINDA SAYFAYA GERI DON VE MESAJI YAZDIR.
            try
            {
                _repository.Insert(urun);
                // URUN KAYDEDILIRSA Satici/Urun SAYFASINA GERI DON.
                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("", "Ürün eklenemedi!");
                return View(model);
            }
        }

        #endregion

        #region Resim Actions

        /// <summary>
        ///     GET: Satici/Urun/ResimEkle?urunId=2
        ///     View görünümü
        /// </summary>
        /// <param name="urunId">Resimin ekleneceği ürünün id'si.</param>
        /// <returns></returns>
        [HttpGet]
        [NoCache]
        public ActionResult ResimEkle(int? urunId)
        {
            if (urunId == null)
                return HttpNotFound();
            ViewBag.ResimEkleUrunId = urunId;
            ViewData["UrunResimList"] = _resimRepository.Table.ToList();
            return View();
        }

        /// <summary>
        ///     POST: Satici/Urun/ResimEkle
        ///     Resimi veri tabanına ekler. Eklenen resim, ürün ile ilişkilendirilir. (UrunResimMapping tablosuna eklenir.)
        /// </summary>
        /// <param name="upload">Eklenecek Resim</param>
        /// <param name="resimekleurunid">Ürün id</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [NoCache]
        public ActionResult ResimEkle(HttpPostedFileBase upload, string resimekleurunid)
        {
            try
            {
                // DOSYA UPLOAD EDİLMİŞ Mİ?
                if (upload != null && upload.ContentLength > 0)
                {
                    // UPLOAD EDİLEN DOSYANIN İSMİNİ AL
                    var fileName = Path.GetFileName(upload.FileName);
                    if (fileName != null)
                    {
                        // DOSYAYI Content/Images/UrunUploads KLASORUNE KAYDET.
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

                        // RESİM OLUŞTUR VE RESİMİ VERİ TABANINA KAYDET
                        var resim = new Resim
                        {
                            Baslik = fileName,
                            CreatedDate = DateTime.Now,
                            ResimBinary = fileBytes,
                            AltAttr = "",
                            TitleAttr = ""
                        };
                        _resimRepository.Insert(resim);

                        // URUN ID BOŞ DEĞİLSE RESİM İLE URUNU ILISKILENDIR. UrunResimMapping TABLOSUNA KAYDET.
                        if (resimekleurunid != null)
                            // VERİ TABANINA EKLE
                        {
                            _urunResimRepository.Insert(new UrunResimMapping
                            {
                                UrunId = int.Parse(resimekleurunid),
                                ResimId = resim.Id
                            });
                        }
                        else
                        {
                            // HATA MESAJI
                            ModelState.AddModelError("", "Ürün id boş veya geçersiz!");
                            return View();
                        }

                        // BÜTÜN İŞLEMLER DOĞRU ŞEKİLDE GERÇEKLEŞİRSE Satici/Urun sayfasına geri dön
                        return RedirectToAction("ResimEkle", "Urun", new {urunId = resimekleurunid});
                    }
                }
            }
            catch (Exception)
            {
                // Herhangi bir hata oluşması sırasında; sayfada görünecek hata
                ModelState.AddModelError("", "Resim eklenemedi! ");
            }

            // Hata oluştuğu sırada sayfaya geri döner
            return View();
        }

        [NoCache]
        public ActionResult Thumbnail(int? resimId)
        {
            if (null == resimId) throw new ArgumentNullException(nameof(resimId));
            var resim = _resimRepository.GetById(resimId);
            var image = new WebImage(resim.ResimBinary).Resize(196, 110, false, true).Crop(1, 1);
            return new ImageResult(new MemoryStream(image.GetBytes()), "binary/octet-stream");
        }

        [NoCache]
        public ActionResult ResimSil(int? urunId, int? resimId)
        {
            if (null == resimId)
                throw new ArgumentNullException(nameof(resimId));
            if (null == urunId)
                throw new ArgumentNullException(nameof(urunId));

            var urunResim =
                _urunResimRepository.Table.SingleOrDefault(u => u.UrunId == urunId.Value && u.ResimId == resimId.Value);
            if (null == urunResim)
                return RedirectToAction("ResimEkle", "Urun", new {urunId});

            _urunResimRepository.Delete(urunResim);
            _resimRepository.Delete(_resimRepository.GetById(resimId));

            return RedirectToAction("ResimEkle", "Urun", new {urunId});
        }

        #endregion
    }
}