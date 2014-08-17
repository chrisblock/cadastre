using System.Web.Http;

using Newtonsoft.Json.Serialization;

namespace Cadastre
{
	public static class CamelCaseJsonSerializerConfig
	{
		public static void ConfigureCamelCaseJsonSerailization(HttpConfiguration configuration)
		{
			configuration.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
		}
	}
}
