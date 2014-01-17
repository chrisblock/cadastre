using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace Cadastre
{
	public static class ValueProviderFactoriesConfig
	{
		public static void RegisterValueProviderFactories()
		{
			//ModelBinders.Binders.DefaultBinder = new ModelBinder();

			var formValueProviderFactory = ValueProviderFactories.Factories
				.OfType<FormValueProviderFactory>()
				.SingleOrDefault();

			ValueProviderFactories.Factories.Remove(formValueProviderFactory);

			ValueProviderFactories.Factories.Add(new FormValueProviderFactory());

			var jsonValueProviderFactory = ValueProviderFactories.Factories
				.OfType<JsonValueProviderFactory>()
				.SingleOrDefault();

			ValueProviderFactories.Factories.Remove(jsonValueProviderFactory);

			ValueProviderFactories.Factories.Add(new JsonDotNetValueProviderFactory());
		}
	}

	public class FormValueProviderFactory : ValueProviderFactory
	{
		public override IValueProvider GetValueProvider(ControllerContext controllerContext)
		{
			return new FormValueProvider(controllerContext);
		}
	}

	/*
	public class FormValueProvider : IValueProvider
	{
		private const string BracketExpressionString = @"\[([A-Za-z]+)\]";

		private static readonly Regex BracketExpression = new Regex(BracketExpressionString);

		private static IEnumerable<string> ParseKey(string key)
		{
			// for form values like data[key1][key2], as provided using jQuery $.post, we want
			//   to also ensure that the form data.key1.key2 is in the dictionary to conform to
			//   what ASP.NET MVC expects

			var result = new List<string>
			{
				key
			};

			var str = key;

			while (BracketExpression.IsMatch(str))
			{
				str = BracketExpression.Replace(str, @".$1", 1);

				result.Add(str);
			}

			return result;
		}

		private readonly IValueProvider _valueProvider;

		public FormValueProvider(ControllerContext controllerContext) : this(controllerContext.HttpContext.Request.Unvalidated.Form)
		{
		}

		public FormValueProvider(NameValueCollection formValues)
		{
			var values = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

			foreach (string key in formValues)
			{
				var value = formValues.Get(key);

				if (value != null)
				{
					var keys = ParseKey(key);

					foreach (var k in keys)
					{
						values[k] = value;
					}
				}
			}

			_valueProvider = new DictionaryValueProvider<string>(values, CultureInfo.CurrentCulture);
		}

		public bool ContainsPrefix(string prefix)
		{
			var result = _valueProvider.ContainsPrefix(prefix);

			return result;
		}

		public ValueProviderResult GetValue(string key)
		{
			var result = _valueProvider.GetValue(key);

			return result;
		}
	}
	*/

	public class FormValueProvider : IValueProvider, IUnvalidatedValueProvider
	{
		private const string BracketExpressionString = @"\[([A-Za-z]+)\]";
		private static readonly Regex BracketExpression = new Regex(BracketExpressionString);

		private static IEnumerable<string> ParseKey(string key)
		{
			// for form values like data[key1][key2], as provided using jQuery $.post, we want
			//   to also ensure that the form data.key1.key2 is in the dictionary to conform to
			//   what ASP.NET MVC expects

			var result = new List<string>
			{
				key
			};

			// TODO: this is assuming that the client code is consistent (e.g. all bracket notation or all dot notation)
			// TODO: I am unsure this is a wise course of action... :-/
			var str = Regex.Replace(key, BracketExpressionString, @".$1");

			if (str != key)
			{
				result.Add(str);
			}

			/*
			var str = key;

			while (BracketExpression.IsMatch(str))
			{
				str = BracketExpression.Replace(str, @".$1", 1);

				result.Add(str);
			}
			*/

			return result;
		}

		private readonly IUnvalidatedValueProvider _valueProvider;
		private readonly IValueProvider _dictionaryValueProvider;

		public FormValueProvider(ControllerContext controllerContext)
			: this(controllerContext.HttpContext.Request.Form, controllerContext.HttpContext.Request.Unvalidated.Form)
		{
		}

		public FormValueProvider(NameValueCollection validatedCollection, NameValueCollection unvalidatedCollection)
		{
			var mapping = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

			foreach (string key in unvalidatedCollection)
			{
				var keys = ParseKey(key);

				foreach (var k in keys)
				{
					mapping[k] = key;
				}
			}

			_valueProvider = new NameValueCollectionValueProvider(validatedCollection, unvalidatedCollection, CultureInfo.CurrentCulture);
			_dictionaryValueProvider = new DictionaryValueProvider<string>(mapping, CultureInfo.CurrentCulture);
		}

		public bool ContainsPrefix(string prefix)
		{
			var result = _dictionaryValueProvider.ContainsPrefix(prefix);

			return result;
		}

		public ValueProviderResult GetValue(string key)
		{
			return GetValue(key, false);
		}

		public ValueProviderResult GetValue(string key, bool skipValidation)
		{
			ValueProviderResult result = null;

			var dictionaryValue = _dictionaryValueProvider.GetValue(key);

			if (dictionaryValue != null)
			{
				var k = dictionaryValue.ConvertTo(typeof (string)) as string;

				if (k != null)
				{
					result = _valueProvider.GetValue(k, skipValidation);
				}
			}

			return result;
		}
	}
}
