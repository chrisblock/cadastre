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
			object result = null;

			if (serviceType.IsInterface || serviceType.IsAbstract)
			{
				result = ObjectFactory.TryGetInstance(serviceType);
			}
			else
			{
				result = ObjectFactory.GetInstance(serviceType);
			}

			return result;
		}

		public IEnumerable<object> GetServices(Type serviceType)
		{
			return ObjectFactory.GetAllInstances(serviceType).Cast<object>();
		}
	}
}
