using System.Linq;

namespace SchemaSurveyor.Core.Surveys
{
	public interface ISurveyRepository
	{
		IQueryable<Survey> Get();

		Survey Get(int surveyId);

		int Insert(string surveyName);

		void Update(int surveyId);

		IQueryable<DatabaseSurvey> GetDatabaseSurveys(int surveyId);

		void InsertDatabaseSurvey(DatabaseSurvey databaseSurvey);

		void UpdateDatabaseSurvey(DatabaseSurvey databaseSurvey);
	}
}
