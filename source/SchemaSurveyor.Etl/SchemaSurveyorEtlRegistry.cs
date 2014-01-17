using SchemaSurveyor.Core;
using SchemaSurveyor.Etl.Surveying;
using SchemaSurveyor.Etl.Surveying.Impl;

using StructureMap.Configuration.DSL;

namespace SchemaSurveyor.Etl
{
	public class SchemaSurveyorEtlRegistry : Registry
	{
		public SchemaSurveyorEtlRegistry()
		{
			Scan(scan =>
			{
				scan.AssemblyContainingType<SchemaSurveyorEtlRegistry>();

				scan.SingleImplementationsOfInterface();
				scan.WithDefaultConventions();
			});

			IncludeRegistry<SchemaSurveyorCoreRegistry>();

			For<IDatabaseListFactory>()
				.Use<StandardInputDatabaseListFactory>();
		}
	}
}
