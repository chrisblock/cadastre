﻿using System.Web.Optimization;

namespace Cadastre
{
	public static class BundleConfig
	{
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new ScriptBundle("~/bundles/jquery")
				.Include("~/Scripts/jquery-{version}.js"));

			bundles.Add(new ScriptBundle("~/bundles/knockout")
				.Include("~/Scripts/knockout-{version}.js"));

			bundles.Add(new ScriptBundle("~/bundles/bootstrap")
				.Include("~/Scripts/bootstrap.js"));

			// Use the development version of Modernizr to develop with and learn from. Then, when you're
			// ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
			bundles.Add(new ScriptBundle("~/bundles/modernizr")
				.Include("~/Scripts/modernizr-*"));

			bundles.Add(new StyleBundle("~/Content/css")
				.Include("~/Content/bootstrap.css")
				.Include("~/Content/site.css"));
		}
	}
}
