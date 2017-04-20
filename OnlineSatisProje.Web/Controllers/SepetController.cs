using System.Linq;
using System.Web.Mvc;
using OnlineSatisProje.Core.Entities;
using OnlineSatisProje.Data;

namespace OnlineSatisProje.Web.Controllers
{
    [Authorize]
    public class SepetController : BaseController
    {
        private readonly IRepository<SepetItem> _sepetRepository;

        public SepetController(IRepository<SepetItem> sepetRepository)
        {
            _sepetRepository = sepetRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>View</returns>
        public ActionResult Index()
        {
            var liste = _sepetRepository.Table.Where(s => s.KullaniciId == CurrentUser.Id).ToList();
            return View(liste);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>View</returns>
        public ActionResult Odeme()
        {
            var list = _sepetRepository.Table.Where(s => s.KullaniciId == CurrentUser.Id).ToList();
            var toplamucret = list.Sum(x => x.Urun.Fiyat * x.Miktar);
            if (toplamucret <= 0)
            {
                return RedirectToAction("Index");
            }
            ViewBag.ToplamUcret = toplamucret;
            return View();
        }
    }
}