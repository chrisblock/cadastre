using System;
using System.Web;
using System.Web.Mvc;

using Cadastre;

using Microsoft.Web.Infrastructure.DynamicModuleHelper;

using StructureMap;

[assembly: PreApplicationStartMethod(typeof (HttpModuleConfig), "Configure")]

namespace Cadastre
{
	public static class HttpModuleConfig
	{
		public static void Configure()
		{
			DynamicModuleUtility.RegisterModule(typeof (StructureMapHttpModule));
		}
	}

	public class StructureMapHttpModule : IHttpModule
	{
		public const string NestedContainer = @"StructureMap.Nested.Container";

		public void Init(HttpApplication application)
		{
			application.BeginRequest -= OnRequestBegin;
			application.BeginRequest += OnRequestBegin;

			application.EndRequest -= OnRequestEnd;
			application.EndRequest += OnRequestEnd;
		}

		private static void OnRequestBegin(object sender, EventArgs args)
		{
			var context = GetHttpContext(sender);

			CreateNestedContainer(context);
		}

		private static void OnRequestEnd(object sender, EventArgs args)
		{
			var context = GetHttpContext(sender);

			DisposeCurrentContainer(context);
		}

		private static HttpContext GetHttpContext(object sender)
		{
			var application = sender as HttpApplication;

			if (application == null)
			{
				throw new Exception();
			}

			var context = application.Context;

			if (context == null)
			{
				throw new Exception();
			}

			return context;
		}

		private static void DisposeCurrentContainer(HttpContext context)
		{
			if (context.Items.Contains(NestedContainer))
			{
				var container = context.Items[NestedContainer] as IContainer;

				if (container != null)
				{
					container.Dispose();
				}

				context.Items.Remove(NestedContainer);
			}
		}

		private static void CreateNestedContainer(HttpContext context)
		{
			DisposeCurrentContainer(context);

			var container = GetCurrentContainer();

			context.Items.Add(NestedContainer, container.GetNestedContainer());
		}

		private static IContainer GetCurrentContainer()
		{
			return DependencyResolver.Current.GetService<IContainer>();
		}

		~StructureMapHttpModule()
		{
			Dispose(false);
		}

		public void Dispose()
		{
			Dispose(true);

			GC.SuppressFinalize(this);
		}

		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				// dispose managed resources
			}

			// dispose native resources
		}
	}
}
