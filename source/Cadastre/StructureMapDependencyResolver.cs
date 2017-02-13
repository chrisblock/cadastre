using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using StructureMap;

namespace Cadastre
{
	public class StructureMapDependencyResolver : System.Web.Mvc.IDependencyResolver, System.Web.Http.Dependencies.IDependencyResolver
	{
		private readonly IContainer _container;

		public StructureMapDependencyResolver(IContainer container)
		{
			_container = container;
		}

		public object GetService(Type serviceType)
		{
			object result;

			var container = GetCurrentContainer();

			if (serviceType.IsInterface || serviceType.IsAbstract)
			{
				result = container.TryGetInstance(serviceType);
			}
			else
			{
				result = container.GetInstance(serviceType);
			}

			return result;
		}

		public IEnumerable<object> GetServices(Type serviceType)
		{
			var container = GetCurrentContainer();

			return container.GetAllInstances(serviceType).Cast<object>();
		}

		public System.Web.Http.Dependencies.IDependencyScope BeginScope()
		{
			var container = _container.GetNestedContainer();

			return new StructureMapDependencyScope(container);
		}

		private IContainer GetCurrentContainer()
		{
			var result = _container;

			var context = HttpContext.Current;

			if (context != null)
			{
				var contextContainer = context.Items[StructureMapHttpModule.NestedContainer] as IContainer;

				if (contextContainer != null)
				{
					result = contextContainer;
				}
			}

			return result;
		}

		~StructureMapDependencyResolver()
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

				_container.Dispose();
			}

			// dispose native resources
		}
	}
}
