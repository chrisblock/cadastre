using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using StructureMap;

namespace Cadastre
{
	public class MvcApplication : HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();

			StructureMapConfig.RegisterDependencyResolver();
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
		}

		protected void Application_EndRequest()
		{
			ObjectFactory.ReleaseAndDisposeAllHttpScopedObjects();
		}
	}
}
