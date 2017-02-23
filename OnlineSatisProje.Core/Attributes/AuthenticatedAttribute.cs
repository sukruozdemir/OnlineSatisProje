using System;
using System.Web.Mvc;

namespace OnlineSatisProje.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AuthenticatedAttribute : AuthorizeAttribute
    {
        //public override void OnAuthorization(AuthorizationContext filterContext)
        //{
        //    if (filterContext.HttpContext.User.Identity.IsAuthenticated)
        //    {
        //        var request = filterContext.HttpContext.Request;
        //        var url = request.IsSecureConnection ? "https://" : "http://";
        //        url += request["HTTP_HOST"] + "/";
        //        filterContext.Result = new RedirectResult(url);
        //    }
        //    base.OnAuthorization(filterContext);
        //}
    }
}