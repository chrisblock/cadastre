using StructureMap;

namespace SchemaSurveyor
{
	public static class StructureMapBootstrapper
	{
		public static void Bootstrap()
		{
			ObjectFactory.Initialize(init => init.AddRegistry<SchemaSurveyorRegistry>());
		}
	}
}
