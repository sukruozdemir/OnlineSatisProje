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
            return View();
        }
    }
}