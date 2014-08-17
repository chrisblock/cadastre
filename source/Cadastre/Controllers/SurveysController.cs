using System.Web.Mvc;

using Cadastre.Models;

using SchemaSurveyor.Core.Surveys;

namespace Cadastre.Controllers
{
	public class SurveysController : Controller
	{
		private readonly ISurveyRepository _surveyRepository;

		public SurveysController(ISurveyRepository surveyRepository)
		{
			_surveyRepository = surveyRepository;
		}

		[HttpGet]
		[ActionName("Index")]
		public ActionResult Index()
		{
			return View();
		}

		[HttpGet]
		[ActionName("Survey")]
		public ActionResult Survey(int surveyId)
		{
			var survey = _surveyRepository.Get(surveyId);

			var model = new SurveyViewModel
			{
				Id = survey.Id,
				Name = survey.Name,
				Machine = survey.Machine,
				User = survey.User,
				Start = survey.Start,
				End = survey.End
			};

			return View(model);
		}

		[HttpGet]
		[ActionName("Create")]
		public ActionResult Create()
		{
			return View();
		}
	}
}
