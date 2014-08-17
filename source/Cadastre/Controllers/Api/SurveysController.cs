using System.Linq;
using System.Web.Http;

using Cadastre.Models;
using Cadastre.Surveys;

using SchemaSurveyor.Core.Surveys;

namespace Cadastre.Controllers.Api
{
	public class SurveysController : ApiController
	{
		private readonly ISurveyRepository _surveyRepository;
		private readonly ISurveyExecutor _surveyExecutor;

		public SurveysController(ISurveyRepository surveyRepository, ISurveyExecutor surveyExecutor)
		{
			_surveyRepository = surveyRepository;
			_surveyExecutor = surveyExecutor;
		}

		[HttpGet]
		[Route("api/Surveys", Name = "SurveysApi")]
		public IQueryable<Survey> Get()
		{
			var result = _surveyRepository.Get();

			return result;
		}

		[HttpGet]
		[Route("api/Surveys/{surveyId:int}", Name = "SurveyApi")]
		public Survey Get(int surveyId)
		{
			var result = _surveyRepository.Get(surveyId);

			return result;
		}

		[HttpPost]
		[Route("api/Surveys", Name = "AddSurveyApi")]
		public bool Post(SurveyRequest request)
		{
			var result = _surveyExecutor.Execute(request.Name, request.Databases);

			return result;
		}
	}
}
