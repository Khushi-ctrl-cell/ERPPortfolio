using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Routing;
using ERPPortfolio.App_Start;
using ERPPortfolio.Data;

namespace ERPPortfolio
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            Database.SetInitializer(new ErpDbInitializer());
        }
    }
}
