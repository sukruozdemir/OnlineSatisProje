#region Usings

using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using log4net;
using OnlineSatisProje.Core.Entities;
using OnlineSatisProje.Data;
using OnlineSatisProje.Services.Interfaces;
using OnlineSatisProje.Web.ActionFilters;
using OnlineSatisProje.Web.Areas.Satici.CustomActions;
using OnlineSatisProje.Web.Areas.Satici.Models;
using System.Net;

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
            _repository = repository;
            _resimRepository = resimRepository;
            _urunResimRepository = urunResimRepository;
        }

        #endregion

        #region Fields

        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly IRepository<Urun> _repository;
        private readonly IRepository<Resim> _resimRepository;
        private readonly IRepository<UrunResimMapping> _urunResimRepository;

        #endregion

        #region Actions

        /// <summary>
        ///     GET: Satici/Urun
        ///     Urun anasayfa görünümü
        /// </summary>
        /// <returns>Index view</returns>
        public ActionResult Index() => View(_repository.Table.Where(u => u.SaticiId == CurrentSatici.Id).ToList());

        /// <summary>
        ///     GET: Satici/Urun/Ekle
        ///     View görünümü
        /// </summary>
        /// <returns>Ekle view</returns>
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
                ModelState.AddModelError("", @"Model isn't valid!");
                return View(model);
            }

            // URUNU VERITABANINA EKLE. HATA OLUSMASI SIRASINDA SAYFAYA GERI DON VE MESAJI YAZDIR.
            try
            {
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
                    UpdatedDate = date,
                    SaticiId = CurrentSatici.Id
                };

                _repository.Insert(urun);
                // URUN KAYDEDILIRSA Satici/Urun SAYFASINA GERI DON.
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                ModelState.AddModelError("", @"Ürün eklenemedi!");
                return View(model);
            }
        }

        public ActionResult Sil(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "id is null!");

            var urun = _repository.GetById(id);
            if (urun == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Urun not found");

            urun.Silindi = true;
            urun.UpdatedDate = DateTime.Now;
            _repository.Update(urun);
            return RedirectToAction("Index");
        }

        public ActionResult Indirim(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var urun = _repository.GetById(id);
            if (urun == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var model = new IndirimModel
            {
                UrunId = (int)id
            };
            return View(model);
        }

        #endregion

        #region Resim Actions

        /// <summary>
        ///     GET: Satici/Urun/Resimler?urunId=2
        ///     View görünümü
        /// </summary>
        /// <param name="urunId">Resimin ekleneceği ürünün id'si.</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Resimler(int? urunId)
        {
            if (urunId == null)
                return RedirectToAction("Index");

            var urun = _repository.GetById(urunId);
            if (urun == null)
            {
                return HttpNotFound();
            }
            var model = new UrunResimMapping
            {
                UrunId = urun.Id
            };
            ViewData["ResimListe"] = _urunResimRepository.Table.ToList().Where(x => x.UrunId == urunId);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Resimler(UrunResimMapping model, HttpPostedFileBase upload)
        {
            if (upload == null)
                throw new ArgumentNullException(nameof(upload));
            try
            {
                if (upload.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(upload.FileName);
                    if (fileName != null)
                    {
                        const string diretoryPath = "~/Content/Images/UrunUploads";
                        if (!Directory.Exists(Server.MapPath(diretoryPath)))
                            Directory.CreateDirectory(Server.MapPath(diretoryPath));

                        // DOSYAYI Content/Images/UrunUploads KLASORUNE KAYDET.
                        var mapFileName = DateTime.Now.ToString("MM.dd.yyyy");
                        var fName = mapFileName + "-" + Guid.NewGuid().ToString().Substring(0, 5) + "-" + fileName;
                        var fullPath = diretoryPath + "/" + fName;
                        var filePath = Path.Combine(Server.MapPath(fullPath));
                        upload.SaveAs(filePath);

                        //byte[] fileBytes;
                        //using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                        //using (var reader = new BinaryReader(fileStream))
                        //    fileBytes = reader.ReadBytes((int)fileStream.Length);
                        var str = fullPath.Substring(1);
                        var resim = new Resim
                        {
                            ResimBinary = null,
                            ResimPath = str,
                            AltAttr = "",
                            Baslik = Path.GetFileName(upload.FileName),
                            TitleAttr = "",
                            CreatedDate = DateTime.Now
                        };
                        _resimRepository.Insert(resim);

                        if (model.UrunId > 0)
                        {
                            model.ResimId = resim.Id;
                            _urunResimRepository.Insert(model);
                            return RedirectToAction("Resimler", new { urunId = model.UrunId });
                        }

                        ModelState.AddModelError("", "Ürün id boş!");
                        return View(model);
                    }

                    ModelState.AddModelError("", "Resim yüklenemedi!");
                    return View(model);
                }

                ModelState.AddModelError("", "Resim upload edilemedi!");
                return View(model);
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
                Logger.Error(e);
                ModelState.AddModelError("", "Resim eklenirken bir hata oluştu");
                return View(model);
            }
        }

        public ActionResult ResimSil(int? urunId, int? resimId)
        {
            if (null == resimId)
                throw new ArgumentNullException(nameof(resimId));
            if (null == urunId)
                throw new ArgumentNullException(nameof(urunId));

            var urunResim =
                _urunResimRepository.Table.SingleOrDefault(u => u.UrunId == urunId.Value && u.ResimId == resimId.Value);
            if (null == urunResim)
                return RedirectToAction("Resimler", "Urun", new { urunId });

            _urunResimRepository.Delete(urunResim);
            _resimRepository.Delete(_resimRepository.GetById(resimId));

            return RedirectToAction("Resimler", "Urun", new { urunId });
        }

        #endregion
    }
}