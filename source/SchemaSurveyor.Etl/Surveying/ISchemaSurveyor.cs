using SchemaSurveyor.Core.Surveys;

namespace SchemaSurveyor.Etl.Surveying
{
	public interface ISchemaSurveyor
	{
		Survey Survey(string surveyName);
	}
}
