using System.Web;
using System.Web.Mvc;

namespace ERPPortfolio.Filters
{
    public class SessionAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return httpContext.Session != null && httpContext.Session["CurrentUserId"] != null;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
                new System.Web.Routing.RouteValueDictionary(new { controller = "Account", action = "Login" })
            );
        }
    }
}
