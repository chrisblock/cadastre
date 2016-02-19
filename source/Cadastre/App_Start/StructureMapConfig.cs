using System.Web.Http;
using System.Web.Mvc;

using StructureMap;

namespace Cadastre
{
	public static class StructureMapConfig
	{
		public static void RegisterDependencyResolver(HttpConfiguration configuration)
		{
			var container = new Container(new CadastreRegistry());

			var dependencyResolver = new StructureMapDependencyResolver(container);

			DependencyResolver.SetResolver(dependencyResolver);

			configuration.DependencyResolver = dependencyResolver;
		}
	}
}
