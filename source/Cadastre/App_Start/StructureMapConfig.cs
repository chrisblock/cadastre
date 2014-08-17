using System.Web.Http;
using System.Web.Mvc;

using StructureMap;

namespace Cadastre
{
	public static class StructureMapConfig
	{
		public static void RegisterDependencyResolver(HttpConfiguration configuration)
		{
			ObjectFactory.Initialize(init => init.AddRegistry<CadastreRegistry>());

			var dependencyResolver = new StructureMapDependencyResolver();

			DependencyResolver.SetResolver(dependencyResolver);

			configuration.DependencyResolver = dependencyResolver;
		}
	}
}
