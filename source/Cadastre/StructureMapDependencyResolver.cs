using System;
using System.Collections.Generic;
using System.Linq;

using StructureMap;

namespace Cadastre
{
	public class StructureMapDependencyResolver : System.Web.Mvc.IDependencyResolver, System.Web.Http.Dependencies.IDependencyResolver
	{
		private readonly IContainer _container;

		public StructureMapDependencyResolver() : this(ObjectFactory.Container)
		{
		}

		public StructureMapDependencyResolver(IContainer container)
		{
			_container = container;
		}

		public object GetService(Type serviceType)
		{
			object result;

			if (serviceType.IsInterface || serviceType.IsAbstract)
			{
				result = _container.TryGetInstance(serviceType);
			}
			else
			{
				result = _container.GetInstance(serviceType);
			}

			return result;
		}

		public IEnumerable<object> GetServices(Type serviceType)
		{
			return _container.GetAllInstances(serviceType).Cast<object>();
		}

		public System.Web.Http.Dependencies.IDependencyScope BeginScope()
		{
			var container = _container.GetNestedContainer();

			return new StructureMapDependencyResolver(container);
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
