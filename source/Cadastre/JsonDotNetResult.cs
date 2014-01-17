using System;
using System.IO;
using System.Threading;
using System.Web.Mvc;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Cadastre
{
	public class JsonDotNetResult : JsonResult
	{
		private static readonly Lazy<JsonSerializerSettings> LazySerializerSettings = new Lazy<JsonSerializerSettings>(CreateSerializerSettings, LazyThreadSafetyMode.ExecutionAndPublication);

		private static JsonSerializerSettings CreateSerializerSettings()
		{
			return new JsonSerializerSettings
			{
				ContractResolver = new CamelCasePropertyNamesContractResolver()
			};
		}

		public static JsonSerializerSettings SerializerSettings { get { return LazySerializerSettings.Value; } }

		private static readonly Lazy<JsonSerializer> LazyJsonSerializer = new Lazy<JsonSerializer>(CreateJsonSerializer, LazyThreadSafetyMode.ExecutionAndPublication);

		private static JsonSerializer CreateJsonSerializer()
		{
			var serializer = JsonSerializer.Create(SerializerSettings);

			return serializer;
		}

		private static JsonSerializer JsonSerializer {get { return LazyJsonSerializer.Value; }}

		private static JsonTextWriter CreateJsonTextWriter(TextWriter textWriter, Formatting formatting)
		{
			return new JsonTextWriter(textWriter)
			{
				Formatting = formatting
			};
		}

		public Formatting Formatting { get; set; }

		public override void ExecuteResult(ControllerContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}

			var request = context.HttpContext.Request;
			var response = context.HttpContext.Response;

			if ((JsonRequestBehavior == JsonRequestBehavior.DenyGet) && String.Equals(request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
			{
				throw new InvalidOperationException("GET request not allowed");
			}

			response.ContentType = (String.IsNullOrWhiteSpace(ContentType) == false)
				? ContentType
				: "application/json";

			if (ContentEncoding != null)
			{
				response.ContentEncoding = ContentEncoding;
			}

			if (Data != null)
			{
				using (var writer = CreateJsonTextWriter(response.Output, Formatting))
				{
					JsonSerializer.Serialize(writer, Data);

					writer.Flush();
				}
			}
		}
	}
}
