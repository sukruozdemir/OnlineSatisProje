using System.Web.Mvc;

namespace OnlineSatisProje.Web.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName => "Admin";

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new {action = "Index", controller = "Home", id = UrlParameter.Optional},
                new[] {"OnlineSatisProje.Web.Areas.Admin.Controllers"}
            );
        }
    }
}