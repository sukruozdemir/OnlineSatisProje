using System;
using System.Linq;
using System.Web.Mvc;
using OnlineSatisProje.Core.Entities;
using OnlineSatisProje.Data;
using OnlineSatisProje.Web.Areas.Admin.Models;

namespace OnlineSatisProje.Web.Areas.Admin.Controllers
{
    public class EtiketController : BaseController
    {
        private readonly IRepository<Etiket> _etiketRepository;

        public EtiketController(IRepository<Etiket> etiketRepository)
        {
            _etiketRepository = etiketRepository;
        }

        // GET: Admin/Etiket
        public ActionResult Index()
        {
            var etiketList = _etiketRepository.Table.ToList();
            return View(etiketList);
        }

        /// <summary>
        ///     POST: Admin/Satici/Ekle
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Ekle(EtiketModel model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Index");
            var date = DateTime.Now;
            _etiketRepository.Insert(new Etiket
            {
                Ad = model.Ad,
                CreatedDate = date
            });
            return RedirectToAction("Index");
        }

        /// <summary>
        ///     POST: Admin/Satici/Sil
        /// </summary>
        /// <param name="etiketId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Sil(int? etiketId)
        {
            if (null == etiketId)
                throw new ArgumentNullException(nameof(etiketId));

            _etiketRepository.Delete(_etiketRepository.GetById(etiketId));
            return RedirectToAction("Index");
        }
    }
}