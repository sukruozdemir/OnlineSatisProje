using System.Web.Mvc;
using OnlineSatisProje.Core.Attributes;

namespace OnlineSatisProje.Web.Areas.Satici.Controllers
{
    [CustomAuthorize(Roles = "Satıcı")]
    public class BaseController : Controller
    {
    }
}