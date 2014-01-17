using System.Web.Mvc;

using SchemaSurveyor.Core;
using SchemaSurveyor.Core.ExtraObjects;

namespace Cadastre.Controllers
{
	public class ExtraObjectsController : Controller
	{
		private readonly IExtraObjectRepository _extraObjectRepository;

		public ExtraObjectsController(IExtraObjectRepository extraObjectRepository)
		{
			_extraObjectRepository = extraObjectRepository;
		}

		[HttpGet]
		[ActionName("Columns")]
		public ActionResult Columns(int surveyId, int databaseSurveyId)
		{
			var extraColumns = _extraObjectRepository.GetExtraObjects(surveyId, databaseSurveyId, ObjectType.Column);

			return Json(extraColumns, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		[ActionName("Functions")]
		public ActionResult Functions(int surveyId, int databaseSurveyId)
		{
			var extraFunctions = _extraObjectRepository.GetExtraObjects(surveyId, databaseSurveyId, ObjectType.Function);

			return Json(extraFunctions, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		[ActionName("Indexes")]
		public ActionResult Indexes(int surveyId, int databaseSurveyId)
		{
			var extraIndexes = _extraObjectRepository.GetExtraObjects(surveyId, databaseSurveyId, ObjectType.Index);

			return Json(extraIndexes, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		[ActionName("Schemas")]
		public ActionResult Schemas(int surveyId, int databaseSurveyId)
		{
			var extraSchemas = _extraObjectRepository.GetExtraObjects(surveyId, databaseSurveyId, ObjectType.Schema);

			return Json(extraSchemas, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		[ActionName("Servers")]
		public ActionResult Servers(int surveyId, int databaseSurveyId)
		{
			var extraServers = _extraObjectRepository.GetExtraObjects(surveyId, databaseSurveyId, ObjectType.Server);

			return Json(extraServers, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		[ActionName("StoredProcedures")]
		public ActionResult StoredProcedures(int surveyId, int databaseSurveyId)
		{
			var extraStoredProcedures = _extraObjectRepository.GetExtraObjects(surveyId, databaseSurveyId, ObjectType.StoredProcedure);

			return Json(extraStoredProcedures, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		[ActionName("Synonyms")]
		public ActionResult Synonyms(int surveyId, int databaseSurveyId)
		{
			var extraSynonyms = _extraObjectRepository.GetExtraObjects(surveyId, databaseSurveyId, ObjectType.Synonym);

			return Json(extraSynonyms, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		[ActionName("Tables")]
		public ActionResult Tables(int surveyId, int databaseSurveyId)
		{
			var extraTables = _extraObjectRepository.GetExtraObjects(surveyId, databaseSurveyId, ObjectType.Table);

			return Json(extraTables, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		[ActionName("Triggers")]
		public ActionResult Triggers(int surveyId, int databaseSurveyId)
		{
			var extraTriggers = _extraObjectRepository.GetExtraObjects(surveyId, databaseSurveyId, ObjectType.Trigger);

			return Json(extraTriggers, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		[ActionName("Views")]
		public ActionResult Views(int surveyId, int databaseSurveyId)
		{
			var extraViews = _extraObjectRepository.GetExtraObjects(surveyId, databaseSurveyId, ObjectType.View);

			return Json(extraViews, JsonRequestBehavior.AllowGet);
		}
	}
}
