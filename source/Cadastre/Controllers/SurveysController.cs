using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;

using Cadastre.Models;

using SchemaSurveyor.Core.Servers;
using SchemaSurveyor.Core.Surveys;

namespace Cadastre.Controllers
{
	public class SurveysController : Controller
	{
		private readonly ISurveyRepository _surveyRepository;
		private readonly IDatabaseRepository _databaseRepository;

		public SurveysController(ISurveyRepository surveyRepository, IDatabaseRepository databaseRepository)
		{
			_surveyRepository = surveyRepository;
			_databaseRepository = databaseRepository;
		}

		[HttpGet]
		[ActionName("Index")]
		public ActionResult Index()
		{
			return View();
		}

		[HttpGet]
		[ActionName("Surveys")]
		public ActionResult Surveys()
		{
			var surveys = _surveyRepository.Get();

			var result = Json(surveys, JsonRequestBehavior.AllowGet);

			return result;
		}

		[HttpGet]
		[ActionName("Survey")]
		public ActionResult Survey(int surveyId)
		{
			ActionResult result;

			var survey = _surveyRepository.Get(surveyId);

			var acceptHeader = Request.Headers.GetValues("Accept");

			if ((acceptHeader != null) && acceptHeader.Any(x => x.Contains("application/json")))
			{
				result = Json(survey, JsonRequestBehavior.AllowGet);
			}
			else
			{
				var model = new SurveyViewModel
				{
					Id = survey.Id,
					Name = survey.Name,
					Machine = survey.Machine,
					User = survey.User,
					Start = survey.Start,
					End = survey.End
				};

				result = View(model);
			}

			return result;
		}

		[HttpGet]
		[ActionName("Databases")]
		public ActionResult Databases(int surveyId)
		{
			var databases = _surveyRepository.GetDatabaseSurveys(surveyId);

			return Json(databases, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		[ActionName("Create")]
		public ActionResult Create()
		{
			var model = new CreateSurveyViewModel
			{
				AvailableServers = _databaseRepository.GetServers().Select((x, i) => new SelectListItem
				{
					Selected = (i == 0),
					Text = x.Name,
					Value = x.Name,
				})
			};

			return View(model);
		}

		[HttpPost]
		[ActionName("Request")]
		public ActionResult RequestSurvey(SurveyRequest request)
		{
			var result = false;

			try
			{
				var stringBuilder = new StringBuilder();

				foreach (var databaseSurveyRequest in request.Databases)
				{
					stringBuilder.AppendFormat("{0},{1},{2}", databaseSurveyRequest.Server, databaseSurveyRequest.Database, databaseSurveyRequest.IsReference);

					stringBuilder.AppendLine();
				}

				var assemblyUri = new Uri(GetType().Assembly.CodeBase);

				var assemblyPath = assemblyUri.LocalPath;

				var assemblyDirectory = Path.GetDirectoryName(assemblyPath);

				var executablePath = Path.GetFullPath(Path.Combine(assemblyDirectory, "..", "..", "SchemaSurveyor", "bin", "Debug", "SchemaSurveyor.exe"));

				using (var process = new System.Diagnostics.Process())
				{
					process.StartInfo = new System.Diagnostics.ProcessStartInfo
					{
						FileName = executablePath,
						Arguments = String.Format("-name=\"{0}\"", request.Name),
						RedirectStandardInput = true,
						RedirectStandardOutput = true,
						UseShellExecute = false
					};

					process.OutputDataReceived += (sender, args) => System.Diagnostics.Debug.WriteLine(args.Data);

					process.Start();

					process.BeginOutputReadLine();

					process.StandardInput.Write(stringBuilder.ToString());

					process.StandardInput.Close();

					process.WaitForExit();

					if (process.ExitCode == 0)
					{
						result = true;
					}

					process.CancelOutputRead();
				}
			}
			catch (Exception e)
			{
			}

			return Json(result);
		}
	}
}
