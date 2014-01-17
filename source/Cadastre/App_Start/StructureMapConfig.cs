using System.Web.Mvc;

using StructureMap;

namespace Cadastre
{
	public static class StructureMapConfig
	{
		public static void RegisterDependencyResolver()
		{
			ObjectFactory.Initialize(init => init.AddRegistry<CadastreRegistry>());

			DependencyResolver.SetResolver(new StructureMapDependencyResolver());
		}
	}
}
