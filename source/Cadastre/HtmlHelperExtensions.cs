using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Cadastre
{
	public static class HtmlHelperExtensions
	{
		public static MvcHtmlString BuildNavListItem(this HtmlHelper htmlHelper, string text, string action, string controller, string area = null)
		{
			var routeData = htmlHelper.ViewContext.RequestContext.RouteData.Values;

			var thisArea = String.Format("{0}", routeData["area"]);
			var thisController = String.Format("{0}", routeData["controller"]);
			var thisAction = String.Format("{0}", routeData["action"]);

			MvcHtmlString result;

			var link = htmlHelper.ActionLink(text, action, controller);

			if (((String.IsNullOrWhiteSpace(thisArea) && String.IsNullOrWhiteSpace(area)) || (thisArea == area)) && (thisController == controller) && (thisAction == action))
			{
				result = MvcHtmlString.Create(String.Format("<li class=\"active\">{0}</li>", link));
			}
			else
			{
				result = MvcHtmlString.Create(String.Format("<li>{0}</li>", link));
			}

			return result;
		}

		public static MvcHtmlString Grid<TGridItem>(this HtmlHelper htmlHelper)
		{
			var type = typeof (TGridItem);

			var properties = type
				.GetProperties(BindingFlags.Instance | BindingFlags.Public);

			var columnNames = properties
				.Select(x => Regex.Replace(x.Name, @"[A-Z]", " $&").Trim());

			var stringBuilder = new StringBuilder();

			stringBuilder.Append("<table>");
			stringBuilder.Append("<thead>");
			stringBuilder.Append("<tr>");
			stringBuilder.AppendFormat("<th>{0}</th>", String.Join("</th><th>", columnNames));
			stringBuilder.Append("</tr>");
			stringBuilder.Append("</thead>");
			stringBuilder.Append("<tbody>");

			stringBuilder.Append("<tr>");
			
			foreach (var property in properties)
			{
				stringBuilder.AppendFormat("<td data-bind=\"text: {0}\"></td>", property.Name);
			}

			stringBuilder.Append("</tr>");

			stringBuilder.Append("</tbody>");
			stringBuilder.Append("</table>");

			return MvcHtmlString.Create(stringBuilder.ToString());
		}

		public static MvcHtmlString Grid<TGridItem>(this HtmlHelper htmlHelper, IEnumerable<TGridItem> items)
		{
			var type = typeof (TGridItem);

			var properties = type
				.GetProperties(BindingFlags.Instance | BindingFlags.Public);

			var columnNames = properties
				.Select(x => Regex.Replace(x.Name, @"[A-Z]", " $&").Trim());

			var stringBuilder = new StringBuilder();

			stringBuilder.Append("<table>");
			stringBuilder.Append("<thead>");
			stringBuilder.Append("<tr>");
			stringBuilder.AppendFormat("<th>{0}</th>", String.Join("</th><th>", columnNames));
			stringBuilder.Append("</tr>");
			stringBuilder.Append("</thead>");
			stringBuilder.Append("<tbody>");

			foreach (var item in items)
			{
				stringBuilder.Append("<tr>");

				foreach (var property in properties)
				{
					stringBuilder.AppendFormat("<td>{0}</td>", property.GetValue(item));
				}

				stringBuilder.Append("</tr>");
			}

			stringBuilder.Append("</tbody>");
			stringBuilder.Append("</table>");

			return MvcHtmlString.Create(stringBuilder.ToString());
		}

		public static MvcHtmlString Button(this HtmlHelper htmlHelper, string text)
		{
			var dictionary = new Dictionary<string, object>();

			return Button(htmlHelper, text, dictionary);
		}

		public static MvcHtmlString Button(this HtmlHelper htmlHelper, string text, object htmlAttributes)
		{
			IDictionary<string, object> dictionary = TypeDescriptor.GetProperties(htmlAttributes)
				.Cast<PropertyDescriptor>()
				.ToDictionary(key => key.Name, value => value.GetValue(htmlAttributes));

			return Button(htmlHelper, text, dictionary);
		}

		public static MvcHtmlString Button(this HtmlHelper htmlHelper, string text, IDictionary<string, object> htmlAttributes)
		{
			if (htmlAttributes.ContainsKey("type") == false)
			{
				htmlAttributes["type"] = "button";
			}

			var stringBuilder = new StringBuilder("<button");

			foreach (var attribute in htmlAttributes)
			{
				stringBuilder.AppendFormat(" {0}=\"{1}\"", attribute.Key, attribute.Value);
			}

			stringBuilder.AppendFormat(">{0}</button>", text);

			return MvcHtmlString.Create(stringBuilder.ToString());
		}

		public static MvcHtmlString SubmitButton(this HtmlHelper htmlHelper, string text)
		{
			var dictionary = new Dictionary<string, object>();

			return SubmitButton(htmlHelper, text, dictionary);
		}

		public static MvcHtmlString SubmitButton(this HtmlHelper htmlHelper, string text, object htmlAttributes)
		{
			IDictionary<string, object> dictionary = TypeDescriptor.GetProperties(htmlAttributes)
				.Cast<PropertyDescriptor>()
				.ToDictionary(key => key.Name, value => value.GetValue(htmlAttributes));

			return SubmitButton(htmlHelper, text, dictionary);
		}

		public static MvcHtmlString SubmitButton(this HtmlHelper htmlHelper, string text, IDictionary<string, object> htmlAttributes)
		{
			htmlAttributes["type"] = "submit";

			return Button(htmlHelper, text, htmlAttributes);
		}
	}
}
