using System.Web.Http;
using System.Web.Mvc;

using Newtonsoft.Json.Serialization;

using StructureMap;

namespace Cadastre
{
	public static class StructureMapConfig
	{
		public static void RegisterDependencyResolver()
		{
			ObjectFactory.Initialize(init => init.AddRegistry<CadastreRegistry>());

			var dependencyResolver = new StructureMapDependencyResolver();

			DependencyResolver.SetResolver(dependencyResolver);

			GlobalConfiguration.Configuration.DependencyResolver = dependencyResolver;

			// TODO: move this into some other configurator thingy
			GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
		}
	}
}
