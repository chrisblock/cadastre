using StructureMap;

namespace SchemaSurveyor
{
	public static class StructureMapContainerFactory
	{
		public static IContainer Create<TRegistry>() where TRegistry : Registry, new()
		{
			return new Container(new TRegistry());
		}
	}
}
