using Goggles.Models;
using System.Linq;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Goggles
{
    public class MvcApplication : System.Web.HttpApplication
    {
        GogglesEntities storeDB = new GogglesEntities();
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //Application Variable that stores Categories of the Goggles throught the application
            var categories = storeDB.Categories.ToList();
            Application["categories"] = categories;
        }
    }
}
