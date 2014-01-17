using System.Collections.Generic;

using SchemaSurveyor.Core.Surveys;

namespace SchemaSurveyor.Etl.Surveying
{
	public interface IDatabaseListFactory
	{
		IEnumerable<DatabaseSurvey> BuildDatabaseList();
	}
}
