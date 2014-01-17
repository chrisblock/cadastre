using System.Data.SqlClient;
using System.Diagnostics;

using SchemaSurveyor.Core;
using SchemaSurveyor.Core.Surveys;

namespace SchemaSurveyor.Etl.Surveying.Impl
{
	public class DatabaseSchemaSurveyor : IDatabaseSchemaSurveyor
	{
		private const string SchemaSurveyConnectionStringName = "SchemaSurvey";

		private readonly SqlConnectionStringBuilder _destination;

		public DatabaseSchemaSurveyor()
		{
			_destination = ConnectionStrings.GetNamedConnectionString(SchemaSurveyConnectionStringName);
		}

		public DatabaseSurvey Survey(DatabaseSurvey database)
		{
			var source = database.GetConnectionStringBuilder();

			var etlProcess = new SchemaSurveyEtlProcess(database.Id, source, _destination);

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
