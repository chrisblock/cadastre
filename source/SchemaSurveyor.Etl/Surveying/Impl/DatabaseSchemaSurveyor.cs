using System.Data.SqlClient;
using System.Diagnostics;

using SchemaSurveyor.Core;
using SchemaSurveyor.Core.Surveys;

namespace SchemaSurveyor.Etl.Surveying.Impl
{
	public class DatabaseSchemaSurveyor : BaseSchemaSurveyRepository, IDatabaseSchemaSurveyor
	{
		public DatabaseSurvey Survey(DatabaseSurvey database)
		{
			var destination = GetSchemaSurveyConnectionString();

			var source = database.GetConnectionStringBuilder();

			var etlProcess = new SchemaSurveyEtlProcess(database.Id, source, destination);

			var stopwatch = new Stopwatch();

			stopwatch.Start();

			etlProcess.Execute();

			stopwatch.Stop();

			var result = new DatabaseSurvey
			{
				Id = database.Id,
				SurveyId = database.SurveyId,
				Server = database.Server,
				Database = database.Database,
				Duration = stopwatch.ElapsedMilliseconds,
				HadConnectionError = database.HadConnectionError,
				Errors = etlProcess.GetAllErrors()
			};

			return result;
		}
	}
}
