using SchemaSurveyor.Etl;

using StructureMap;

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
