using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using StructureMap;

namespace Cadastre
{
	public class StructureMapDependencyResolver : IDependencyResolver
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
	}
}
