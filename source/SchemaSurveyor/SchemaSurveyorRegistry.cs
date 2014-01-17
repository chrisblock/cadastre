using SchemaSurveyor.Etl;

using StructureMap.Configuration.DSL;

namespace SchemaSurveyor
{
	public class SchemaSurveyorRegistry : Registry
	{
		public SchemaSurveyorRegistry()
		{
			Scan(scan =>
			{
				scan.AssemblyContainingType<SchemaSurveyorRegistry>();

				scan.SingleImplementationsOfInterface();
				scan.WithDefaultConventions();
			});

			IncludeRegistry<SchemaSurveyorEtlRegistry>();
		}
	}
}
