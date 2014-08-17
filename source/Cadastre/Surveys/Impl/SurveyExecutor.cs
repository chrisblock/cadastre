using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

using Cadastre.Models;

namespace Cadastre.Surveys.Impl
{
	public class SurveyExecutor : ISurveyExecutor
	{
		private static string BuildInputString(IEnumerable<DatabaseSurveyRequest> databases)
		{
			var stringBuilder = new StringBuilder();

			foreach (var databaseSurveyRequest in databases)
			{
				stringBuilder.AppendFormat("{0},{1},{2}", databaseSurveyRequest.Server, databaseSurveyRequest.Database, databaseSurveyRequest.IsReference);

				stringBuilder.AppendLine();
			}

			return stringBuilder.ToString();
		}

		private string GetExecutablePath()
		{
#if DEBUG
			const string compilationMode = "Debug";
#else
			const string compilationMode = "Release";
#endif

			var assemblyUri = new Uri(GetType().Assembly.CodeBase);

			var assemblyPath = assemblyUri.LocalPath;

			var assemblyDirectory = Path.GetDirectoryName(assemblyPath);

			var executablePath = Path.GetFullPath(Path.Combine(assemblyDirectory, "..", "..", "SchemaSurveyor", "bin", compilationMode, "SchemaSurveyor.exe"));

			return executablePath;
		}

		private Process CreateProcess(string executable, string arguments)
		{
			var process = new Process
			{
				StartInfo = new ProcessStartInfo(executable, arguments)
				{
					RedirectStandardInput = true,
					RedirectStandardOutput = true,
					UseShellExecute = false
				}
			};

			return process;
		}

		private bool ExecuteSurvey(string surveyName, IEnumerable<DatabaseSurveyRequest> databases)
		{
			var result = false;

			var executablePath = GetExecutablePath();

			var input = BuildInputString(databases);

			using (var process = CreateProcess(executablePath, String.Format("-name=\"{0}\"", surveyName)))
			{
				process.OutputDataReceived += (sender, args) => Debug.WriteLine(args.Data);

				process.Start();

				process.BeginOutputReadLine();

				process.StandardInput.Write(input);

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

		public bool Execute(string surveyName, IEnumerable<DatabaseSurveyRequest> databases)
		{
			var result = false;

			try
			{
				result = ExecuteSurvey(surveyName, databases);
			}
			catch (Exception e)
			{
				// om nom nom; delicious
			}

			return result;
		}
	}
}
