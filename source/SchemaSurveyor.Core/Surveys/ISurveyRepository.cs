using System.Linq;

namespace SchemaSurveyor.Core.Surveys
{
	public interface ISurveyRepository
	{
		IQueryable<Survey> Get();

		Survey Get(int surveyId);

		IQueryable<DatabaseSurvey> GetDatabaseSurveys(int surveyId);
	}
}
