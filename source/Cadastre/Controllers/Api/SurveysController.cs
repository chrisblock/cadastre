using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Http;

using Cadastre.Models;

using SchemaSurveyor.Core.Surveys;

namespace Cadastre.Controllers.Api
{
	public class SurveysController : ApiController
	{
		private readonly ISurveyRepository _surveyRepository;

		public SurveysController(ISurveyRepository surveyRepository)
		{
			_surveyRepository = surveyRepository;
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
			var result = false;

			var stringBuilder = new StringBuilder();

			foreach (var databaseSurveyRequest in request.Databases)
			{
				stringBuilder.AppendFormat("{0},{1},{2}", databaseSurveyRequest.Server, databaseSurveyRequest.Database, databaseSurveyRequest.IsReference);

				stringBuilder.AppendLine();
			}

			var assemblyUri = new Uri(GetType().Assembly.CodeBase);

			var assemblyPath = assemblyUri.LocalPath;

			var assemblyDirectory = Path.GetDirectoryName(assemblyPath);

#if DEBUG
			var executablePath = Path.GetFullPath(Path.Combine(assemblyDirectory, "..", "..", "SchemaSurveyor", "bin", "Debug", "SchemaSurveyor.exe"));
#else
			var executablePath = Path.GetFullPath(Path.Combine(assemblyDirectory, "..", "..", "SchemaSurveyor", "bin", "Release", "SchemaSurveyor.exe"));
#endif

			using (var process = new Process())
			{
				process.StartInfo = new ProcessStartInfo
				{
					FileName = executablePath,
					Arguments = String.Format("-name=\"{0}\"", request.Name),
					RedirectStandardInput = true,
					RedirectStandardOutput = true,
					UseShellExecute = false
				};

				process.OutputDataReceived += (sender, args) => Debug.WriteLine(args.Data);

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

			return result;
		}
	}
}
