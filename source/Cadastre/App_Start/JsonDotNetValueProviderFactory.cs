using System;
using System.IO;
using System.Web.Mvc;

namespace Cadastre
{
	public class JsonDotNetValueProviderFactory : ValueProviderFactory
	{
		private readonly Stream _inputStream;

		public JsonDotNetValueProviderFactory()
		{
		}

		public JsonDotNetValueProviderFactory(Stream inputStream)
		{
			_inputStream = inputStream;
		}

		public override IValueProvider GetValueProvider(ControllerContext controllerContext)
		{
			if (controllerContext == null)
			{
				throw new ArgumentNullException("controllerContext");
			}

			IValueProvider result = null;

			var request = controllerContext.HttpContext.Request;

			if (request.ContentType.StartsWith("application/json", StringComparison.OrdinalIgnoreCase))
			{
				result = new JsonDotNetValueProvider(_inputStream ?? request.InputStream);
			}

			return result;
		}
	}
}
