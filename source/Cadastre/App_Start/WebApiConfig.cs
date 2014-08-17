using System.Web.Http;

namespace Cadastre
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration configuration)
		{
			StructureMapConfig.RegisterDependencyResolver(configuration);

			CamelCaseJsonSerializerConfig.ConfigureCamelCaseJsonSerailization(configuration);

			configuration.MapHttpAttributeRoutes();
		}
	}
}
