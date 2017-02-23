using System;
using System.Web.Mvc;

namespace OnlineSatisProje.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated || !filterContext.HttpContext.User.IsInRole(Roles))
            {
                var request = filterContext.HttpContext.Request;
                var url = request.IsSecureConnection ? "https://" : "http://";
                url += request["HTTP_HOST"] + "/";
                filterContext.Result = new RedirectResult(url + "AccessDenied.html");
            }
            //else if (!filterContext.HttpContext.User.IsInRole(Roles))
            //{
            //    var request = filterContext.HttpContext.Request;
            //    var url = request.IsSecureConnection ? "https://" : "http://";
            //    url += request["HTTP_HOST"] + "/";
            //    filterContext.Result = new RedirectResult(url + "AccessDenied.html");
            //}
            else
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
        }
    }
}