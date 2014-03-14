using System.Diagnostics;

using SchemaSurveyor.Core;
using SchemaSurveyor.Core.Surveys;

namespace SchemaSurveyor.Etl.Surveying.Impl
{
	public class DatabaseSchemaSurveyor : BaseSchemaSurveyRepository, IDatabaseSchemaSurveyor
	{
		public DatabaseSurvey Survey(DatabaseSurvey databaseSurvey)
		{
			var destination = GetSchemaSurveyConnectionString();

			var source = databaseSurvey.GetConnectionStringBuilder();

			var etlProcess = new SchemaSurveyEtlProcess(databaseSurvey.Id, source, destination);

			var stopwatch = new Stopwatch();

			stopwatch.Start();

			etlProcess.Execute();

			stopwatch.Stop();

			var result = new DatabaseSurvey
			{
				Id = databaseSurvey.Id,
				SurveyId = databaseSurvey.SurveyId,
				Server = databaseSurvey.Server,
				Database = databaseSurvey.Database,
				Duration = stopwatch.ElapsedMilliseconds,
				HadConnectionError = databaseSurvey.HadConnectionError,
				Errors = etlProcess.GetAllErrors()
			};

			return result;
		}
	}
}
