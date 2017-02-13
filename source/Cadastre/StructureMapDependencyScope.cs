using System;
using System.Collections.Generic;
using System.Linq;

using StructureMap;

namespace Cadastre
{
	public class StructureMapDependencyScope : System.Web.Http.Dependencies.IDependencyScope
	{
		private readonly IContainer _container;

		public StructureMapDependencyScope(IContainer container)
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

		~StructureMapDependencyScope()
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
