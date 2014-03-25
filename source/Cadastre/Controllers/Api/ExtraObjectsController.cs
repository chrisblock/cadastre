using System;
using System.Linq;
using System.Web.Http;

using SchemaSurveyor.Core;
using SchemaSurveyor.Core.ExtraObjects;

namespace Cadastre.Controllers.Api
{
	public class ExtraObjectsController : ApiController
	{
		private readonly IExtraObjectRepository _extraObjectRepository;

		public ExtraObjectsController(IExtraObjectRepository extraObjectRepository)
		{
			_extraObjectRepository = extraObjectRepository;
		}

		public ExtraObjectCollection Get(int surveyId, int databaseSurveyId)
		{
			var result = _extraObjectRepository.GetExtraObjects(surveyId, databaseSurveyId);

			return result;
		}

		public IQueryable<ExtraObject> Get(int surveyId, int databaseSurveyId, string objectType)
		{
			var type = (ObjectType) Enum.Parse(typeof (ObjectType), objectType);

			var result = _extraObjectRepository.GetExtraObjects(surveyId, databaseSurveyId, type);

			return result;
		}
	}
}
