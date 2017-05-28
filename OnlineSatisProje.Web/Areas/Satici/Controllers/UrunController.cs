#region Usings

using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using log4net;
using OnlineSatisProje.Core.Entities;
using OnlineSatisProje.Data;
using OnlineSatisProje.Services.Interfaces;
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
            IRepository<UrunResimMapping> urunResimRepository,
            IRepository<SaticiIndirimMapping> saticiIndirimRepository,
            IRepository<UrunIndirimMapping> urunIndirimRepository,
            IRepository<Kategori> kategoriRepository,
            IRepository<UrunKategoriMapping> urunKategoriRepository,
            IRepository<Etiket> etiketRepository,
            IRepository<UrunEtiketMapping> urunEtiketRepository)
        {
            _repository = repository;
            _resimRepository = resimRepository;
            _urunResimRepository = urunResimRepository;
            _saticiIndirimRepository = saticiIndirimRepository;
            _urunIndirimRepository = urunIndirimRepository;
            _kategoriRepository = kategoriRepository;
            _urunKategoriRepository = urunKategoriRepository;
            _urunEtiketRepository = urunEtiketRepository;
            _etiketRepository = etiketRepository;
        }

        #endregion

        #region Fields

        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly IRepository<Urun> _repository;
        private readonly IRepository<Resim> _resimRepository;
        private readonly IRepository<UrunResimMapping> _urunResimRepository;
        private readonly IRepository<SaticiIndirimMapping> _saticiIndirimRepository;
        private readonly IRepository<UrunIndirimMapping> _urunIndirimRepository;
        private readonly IRepository<UrunKategoriMapping> _urunKategoriRepository;
        private readonly IRepository<UrunEtiketMapping> _urunEtiketRepository;
        private readonly IRepository<Kategori> _kategoriRepository;
        private readonly IRepository<Etiket> _etiketRepository;

        #endregion

        #region Actions

        /// <summary>
        ///     GET: Satici/Urun
        ///     Urun anasayfa görünümü
        /// </summary>
        /// <returns>Index view</returns>
        public ActionResult Index()
        {
            return View(_repository.Table.Where(u => u.SaticiId == CurrentSatici.Id).ToList());
        }

        #region Ekle/Sil Actions

        /// <summary>
        ///     GET: Satici/Urun/Ekle
        ///     View görünümü
        /// </summary>
        /// <returns>Ekle view</returns>
        [HttpGet]
        public ActionResult Ekle()
        {
            return View();
        }

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
                return HttpNotFound();
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
                            return RedirectToAction("Resimler", new {urunId = model.UrunId});
                        }

                        ModelState.AddModelError("", @"Ürün id boş!");
                        return View(model);
                    }

                    ModelState.AddModelError("", @"Resim yüklenemedi!");
                    return View(model);
                }

                ModelState.AddModelError("", @"Resim upload edilemedi!");
                return View(model);
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
                Logger.Error(e);
                ModelState.AddModelError("", @"Resim eklenirken bir hata oluştu");
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
                return RedirectToAction("Resimler", "Urun", new {urunId});

            _urunResimRepository.Delete(urunResim);
            _resimRepository.Delete(_resimRepository.GetById(resimId));

            return RedirectToAction("Resimler", "Urun", new {urunId});
        }

        #endregion

        #region Indirim Actions

        public ActionResult Indirimler(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var urun = _repository.GetById(id);
            if (urun == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var liste = _urunIndirimRepository.Table.Where(u => u.UrunId == urun.Id).ToList();

            var urunindirimModel = new UrunIndirimModel
            {
                UrunId = urun.Id,
                Urun = urun,
                Indirimler = _saticiIndirimRepository.Table.Where(i => i.SaticiId == CurrentSatici.Id)
                    .Select(i => i.Indirim).ToList()
            };

            return View(new UrunIndirimViewModel
            {
                UrunIndirimMapping = liste,
                UrunIndirimModel = urunindirimModel
            });
        }

        [HttpPost]
        public ActionResult IndirimEkle(UrunIndirimModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", @"Eklenme sırasında bir hata oluştu!");
                return RedirectToAction("Indirimler", new {id = model.UrunId});
            }

            try
            {
                _urunIndirimRepository.Insert(new UrunIndirimMapping
                {
                    IndirimId = model.IndirimId,
                    UrunId = model.UrunId
                });
                return RedirectToAction("Indirimler", new {id = model.UrunId});
            }
            catch (Exception)
            {
                ModelState.AddModelError("", @"Eklenme sırasında bir hata oluştu!");
                return RedirectToAction("Indirimler", new {id = model.UrunId});
            }
        }

        #endregion

        #region Kategori Actions

        public ActionResult Kategoriler(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var urun = _repository.GetById(id);
            if (urun == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var model = new UrunKategoriModel
            {
                Kategoriler = _kategoriRepository.Table.ToList(),
                Urun = urun,
                UrunId = urun.Id
            };

            return View(new UrunKategoriViewModel
            {
                UrunKategoriMapping = urun.UrunKategoriMapping,
                UrunKategoriModel = model
            });
        }

        [HttpPost]
        public ActionResult KategoriEkle(UrunKategoriModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", @"Eklenme sırasında bir hata oluştu!");
                return RedirectToAction("Kategoriler", new {id = model.UrunId});
            }

            try
            {
                _urunKategoriRepository.Insert(new UrunKategoriMapping
                {
                    KategoriId = model.KategoriId,
                    UrunId = model.UrunId,
                    CreatedDate = DateTime.Now
                });
                return RedirectToAction("Kategoriler", new {id = model.UrunId});
            }
            catch (Exception)
            {
                ModelState.AddModelError("", @"Eklenme sırasında bir hata oluştu!");
                return RedirectToAction("Kategoriler", new {id = model.UrunId});
            }
        }

        #endregion

        #region Etiket Actions

        public ActionResult Etiketler(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var urun = _repository.GetById(id);
            if (urun == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var model = new UrunEtiketModel
            {
                Etiketler = _etiketRepository.Table.ToList(),
                Urun = urun,
                UrunId = urun.Id
            };

            return View(new UrunEtiketViewModel
            {
                UrunEtiketMapping = urun.UrunEtiketMapping,
                UrunEtiketModel = model
            });
        }

        [HttpPost]
        public ActionResult EtiketEkle(UrunEtiketModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", @"Eklenme sırasında bir hata oluştu!");
                return RedirectToAction("Etiketler", new {id = model.UrunId});
            }

            try
            {
                _urunEtiketRepository.Insert(new UrunEtiketMapping
                {
                    EtiketId = model.EtiketId,
                    UrunId = model.UrunId
                });
                return RedirectToAction("Etiketler", new {id = model.UrunId});
            }
            catch (Exception)
            {
                ModelState.AddModelError("", @"Eklenme sırasında bir hata oluştu!");
                return RedirectToAction("Etiketler", new {id = model.UrunId});
            }
        }

        #endregion

        #endregion
    }
}