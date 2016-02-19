using SchemaSurveyor.Core.Servers;

using StructureMap;

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

				scan.AddAllTypesOf<IServerSource>();
			});
		}
	}
}
