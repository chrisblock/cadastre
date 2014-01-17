using StructureMap.Configuration.DSL;

namespace SchemaSurveyor.Core
{
	public class SchemaSurveyorCoreRegistry : Registry
	{
		public SchemaSurveyorCoreRegistry()
		{
			Scan(scan =>
			{
				scan.AssemblyContainingType<SchemaSurveyorCoreRegistry>();

				scan.SingleImplementationsOfInterface();
				scan.WithDefaultConventions();
			});
		}
	}
}
