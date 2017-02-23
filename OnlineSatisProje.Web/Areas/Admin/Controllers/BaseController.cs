using System.Web.Mvc;
using OnlineSatisProje.Core.Attributes;

namespace OnlineSatisProje.Web.Areas.Admin.Controllers
{
    [CustomAuthorize(Roles = "Admin")]
    public abstract class BaseController : Controller
    {
    }
}