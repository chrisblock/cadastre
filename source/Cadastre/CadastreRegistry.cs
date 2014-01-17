﻿using SchemaSurveyor.Core;

using StructureMap.Configuration.DSL;

namespace Cadastre
{
	public class CadastreRegistry : Registry
	{
		public CadastreRegistry()
		{
			Scan(scan =>
			{
				scan.AssemblyContainingType<CadastreRegistry>();

				scan.SingleImplementationsOfInterface();
				scan.WithDefaultConventions();
			});

			IncludeRegistry<SchemaSurveyorCoreRegistry>();
		}
	}
}
