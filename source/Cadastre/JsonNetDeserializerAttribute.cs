using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web.Mvc;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Cadastre
{
	public class JsonNetDeserializerAttribute : ActionFilterAttribute
	{
		private static readonly Lazy<JsonSerializerSettings> LazySerializerSettings = new Lazy<JsonSerializerSettings>(CreateSerializerSettings, LazyThreadSafetyMode.ExecutionAndPublication);

		private static JsonSerializerSettings SerializerSettings { get { return LazySerializerSettings.Value; } }

		private static JsonSerializerSettings CreateSerializerSettings()
		{
			var settings = new JsonSerializerSettings
			{
				ContractResolver = new CamelCasePropertyNamesContractResolver()
			};

			return settings;
		}

		private static readonly Lazy<JsonSerializer> LazyJsonSerializer = new Lazy<JsonSerializer>(CreateJsonSerializer, LazyThreadSafetyMode.ExecutionAndPublication);

		private static JsonSerializer CreateJsonSerializer()
		{
			var serializer = JsonSerializer.Create(SerializerSettings);

			return serializer;
		}

		private static JsonSerializer JsonSerializer { get { return LazyJsonSerializer.Value; } }

		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			var request = filterContext.HttpContext.Request;

			if (String.Equals(request.HttpMethod, "POST", StringComparison.OrdinalIgnoreCase) && request.ContentType.StartsWith("application/json", StringComparison.OrdinalIgnoreCase))
			{
				string name = null;
				Type type = null;

				var parameters = filterContext.ActionDescriptor.GetParameters();

				if (parameters.Length == 1)
				{
					var parameter = parameters.Single();

					name = parameter.ParameterName;
					type = parameter.ParameterType;
				}

				if (String.IsNullOrWhiteSpace(name) == false)
				{
					var inputStream = request.InputStream;

					inputStream.Position = 0;

					object value;

					using (var streamReader = new StreamReader(inputStream))
					{
						using (var jsonReader = new JsonTextReader(streamReader))
						{
							value = JsonSerializer.Deserialize(jsonReader, type);
						}
					}

					filterContext.ActionParameters[name] = value;
				}
			}
		}
	}
}
