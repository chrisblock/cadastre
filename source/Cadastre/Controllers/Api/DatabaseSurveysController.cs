using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

using SchemaSurveyor.Core.Surveys;

namespace Cadastre.Controllers.Api
{
	public class DatabaseSurveysController : ApiController
	{
		private readonly ISurveyRepository _surveyRepository;

		public DatabaseSurveysController(ISurveyRepository surveyRepository)
		{
			_surveyRepository = surveyRepository;
		}

		public IEnumerable<DatabaseSurvey> Get(int surveyId)
		{
			var result = _surveyRepository.GetDatabaseSurveys(surveyId);

			return result;
		}

		public DatabaseSurvey Get(int surveyId, int databaseSurveyId)
		{
			var result = _surveyRepository
				.GetDatabaseSurveys(surveyId)
				.SingleOrDefault(x => x.Id == databaseSurveyId);

			return result;
		}
	}
}
