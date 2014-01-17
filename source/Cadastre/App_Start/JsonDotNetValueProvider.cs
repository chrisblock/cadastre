using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Web.Mvc;

using Newtonsoft.Json;

namespace Cadastre
{
	public class JsonDotNetValueProvider : IValueProvider
	{
		private readonly IValueProvider _valueProvider;

		private static void RecursiveAdd(string prefix, IEnumerable<KeyValuePair<string, object>> o, Action<string, object> add)
		{
			foreach (var x in o)
			{
				var enumerable = x.Value as IEnumerable<ExpandoObject>;

				if (enumerable != null)
				{
					var i = 0;
					foreach (var item in enumerable)
					{
						var p = String.Format("{0}[{1}]", prefix, i);

						RecursiveAdd(p, item, add);

						i++;
					}
				}
				else
				{
					// TODO: traverse this object's properties, if possible
					var key = String.Format("{0}{1}", prefix, x.Key);

					add(key, x.Value);
				}
			}
		}

		public JsonDotNetValueProvider(Stream inputStream)
		{
			using (var streamReader = new StreamReader(inputStream))
			{
				using (var jsonTextStream = new JsonTextReader(streamReader))
				{
					var serializer = new JsonSerializer();

					var result = serializer.Deserialize<ExpandoObject>(jsonTextStream);

					var dictionary = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);

					RecursiveAdd(String.Empty, result, dictionary.Add);

					_valueProvider = new DictionaryValueProvider<object>(dictionary, CultureInfo.CurrentCulture);
				}
			}
		}

		public bool ContainsPrefix(string prefix)
		{
			var result = false;

			if (_valueProvider != null)
			{
				result = _valueProvider.ContainsPrefix(prefix);
			}

			return result;
		}

		public ValueProviderResult GetValue(string key)
		{
			ValueProviderResult result = null;

			if (_valueProvider != null)
			{
				result = _valueProvider.GetValue(key);
			}

			return result;
		}
	}
}
