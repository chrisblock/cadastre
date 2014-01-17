using System.Web.Mvc;

using SchemaSurveyor.Core;
using SchemaSurveyor.Core.MissingObjects;

namespace Cadastre.Controllers
{
	public class MissingObjectsController : Controller
	{
		private readonly IMissingObjectRepository _missingObjectRepository;

		public MissingObjectsController(IMissingObjectRepository missingObjectRepository)
		{
			_missingObjectRepository = missingObjectRepository;
		}

		[HttpGet]
		[ActionName("Columns")]
		public ActionResult Columns(int surveyId, int databaseSurveyId)
		{
			var missingColumns = _missingObjectRepository.GetMissingObjects(surveyId, databaseSurveyId, ObjectType.Column);

			return Json(missingColumns, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		[ActionName("Functions")]
		public ActionResult Functions(int surveyId, int databaseSurveyId)
		{
			var missingFunctions = _missingObjectRepository.GetMissingObjects(surveyId, databaseSurveyId, ObjectType.Function);

			return Json(missingFunctions, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		[ActionName("Indexes")]
		public ActionResult Indexes(int surveyId, int databaseSurveyId)
		{
			var missingIndexes = _missingObjectRepository.GetMissingObjects(surveyId, databaseSurveyId, ObjectType.Index);

			return Json(missingIndexes, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		[ActionName("Schemas")]
		public ActionResult Schemas(int surveyId, int databaseSurveyId)
		{
			var missingSchemas = _missingObjectRepository.GetMissingObjects(surveyId, databaseSurveyId, ObjectType.Schema);

			return Json(missingSchemas, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		[ActionName("Servers")]
		public ActionResult Servers(int surveyId, int databaseSurveyId)
		{
			var missingServers = _missingObjectRepository.GetMissingObjects(surveyId, databaseSurveyId, ObjectType.Server);

			return Json(missingServers, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		[ActionName("StoredProcedures")]
		public ActionResult StoredProcedures(int surveyId, int databaseSurveyId)
		{
			var missingStoredProcedures = _missingObjectRepository.GetMissingObjects(surveyId, databaseSurveyId, ObjectType.StoredProcedure);

			return Json(missingStoredProcedures, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		[ActionName("Synonyms")]
		public ActionResult Synonyms(int surveyId, int databaseSurveyId)
		{
			var missingSynonyms = _missingObjectRepository.GetMissingObjects(surveyId, databaseSurveyId, ObjectType.Synonym);

			return Json(missingSynonyms, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		[ActionName("Tables")]
		public ActionResult Tables(int surveyId, int databaseSurveyId)
		{
			var missingTables = _missingObjectRepository.GetMissingObjects(surveyId, databaseSurveyId, ObjectType.Table);

			return Json(missingTables, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		[ActionName("Triggers")]
		public ActionResult Triggers(int surveyId, int databaseSurveyId)
		{
			var missingTriggers = _missingObjectRepository.GetMissingObjects(surveyId, databaseSurveyId, ObjectType.Trigger);

			return Json(missingTriggers, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		[ActionName("Views")]
		public ActionResult Views(int surveyId, int databaseSurveyId)
		{
			var missingViews = _missingObjectRepository.GetMissingObjects(surveyId, databaseSurveyId, ObjectType.View);

			return Json(missingViews, JsonRequestBehavior.AllowGet);
		}
	}
}
