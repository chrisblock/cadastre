using System.Linq;
using System.Web.Http;

using SchemaSurveyor.Core;
using SchemaSurveyor.Core.MissingObjects;

namespace Cadastre.Controllers.Api
{
	public class MissingObjectsController : ApiController
	{
		private readonly IMissingObjectRepository _missingObjectRepository;

		public MissingObjectsController(IMissingObjectRepository missingObjectRepository)
		{
			_missingObjectRepository = missingObjectRepository;
		}

		[HttpGet]
		[Route("api/Surveys/{surveyId:int}/Databases/{databaseSurveyId:int}/MissingObjects", Name = "MissingObjectsApi")]
		public MissingObjectCollection Get(int surveyId, int databaseSurveyId)
		{
			var result = _missingObjectRepository.GetMissingObjects(surveyId, databaseSurveyId);

			return result;
		}

		[HttpGet]
		[Route("api/Surveys/{surveyId:int}/Databases/{databaseSurveyId:int}/MissingObjects/{objectType:alpha}", Name = "MissingObjectTypeApi")]
		public IQueryable<MissingObject> Get(int surveyId, int databaseSurveyId, string objectType)
		{
			var type = Enums.Parse<ObjectType>(objectType);

			var result = _missingObjectRepository.GetMissingObjects(surveyId, databaseSurveyId, type);

			return result;
		}
	}
}
