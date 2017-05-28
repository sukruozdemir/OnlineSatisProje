using System.Web.Mvc;

namespace OnlineSatisProje.Web.Areas.Satici
{
    public class SaticiAreaRegistration : AreaRegistration
    {
        public override string AreaName => "Satici";

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Satici_default",
                "Satici/{controller}/{action}/{id}",
                new {action = "Index", controller = "Home", id = UrlParameter.Optional},
                new[] {"OnlineSatisProje.Web.Areas.Satici.Controllers"}
            );
        }
    }
}