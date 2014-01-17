using SchemaSurveyor.Core.Surveys;

namespace SchemaSurveyor.Etl.Surveying
{
	public interface IDatabaseSchemaSurveyor
	{
		DatabaseSurvey Survey(DatabaseSurvey database);
	}
}
