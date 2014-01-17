using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

using SchemaSurveyor.Core;
using SchemaSurveyor.Core.Surveys;

namespace SchemaSurveyor.Etl.Surveying.Impl
{
	public class SchemaSurveyor : ISchemaSurveyor
	{
		private const string SchemaSurveyConnectionStringName = "SchemaSurvey";

		private readonly IDatabaseListFactory _databaseListFactory;
		private readonly IDatabaseSchemaSurveyor _databaseSchemaSurveyor;
		private readonly SqlConnectionStringBuilder _destination;

		public SchemaSurveyor(IDatabaseListFactory databaseListFactory, IDatabaseSchemaSurveyor databaseSchemaSurveyor)
		{
			_databaseListFactory = databaseListFactory;
			_databaseSchemaSurveyor = databaseSchemaSurveyor;
			_destination = ConnectionStrings.GetNamedConnectionString(SchemaSurveyConnectionStringName);
		}

		public Survey Survey(string surveyName)
		{
			var survey = new Survey
			{
				Name = surveyName
			};

			survey.Id = InsertSurveyRecord(surveyName);

			var databaseSurveys = new ConcurrentBag<DatabaseSurvey>();

			var connectableDatabases = new List<DatabaseSurvey>();

			var targets = GetTargetDatabases();

			foreach (var database in targets)
			{
				database.SurveyId = survey.Id;

				if (database.HadConnectionError == false)
				{
					connectableDatabases.Add(database);
				}

				// TODO: convert this to bulk insert using Rhino??
				InsertDatabaseSurvey(database);
			}

			// TODO: for many databases, this is untenable
			Parallel.ForEach(connectableDatabases, x =>
			{
				var databaseSurvey = _databaseSchemaSurveyor.Survey(x);

				databaseSurveys.Add(databaseSurvey);
			});

			foreach (var databaseSurvey in databaseSurveys)
			{
				UpdateDatabseSurvey(databaseSurvey);
			}

			UpdateSurveyRecord(survey.Id);

			survey.DatabaseSurveys = databaseSurveys;

			return survey;
		}

		private IEnumerable<DatabaseSurvey> GetTargetDatabases()
		{
			var targets = _databaseListFactory.BuildDatabaseList()
				.ToList();

			Parallel.ForEach(targets, x => x.HadConnectionError = (ConnectionTester.CanConnect(x.GetConnectionStringBuilder()) == false));

			return targets;
		}

		private int InsertSurveyRecord(string surveyName)
		{
			int result;

			using (var connection = new SqlConnection(_destination.ToString()))
			{
				connection.Open();

				using (var command = connection.CreateCommand("INSERT INTO [dbo].[Surveys] ([name], [user], [machine], [start]) VALUES (@name, @user, @machine, GETDATE()); SELECT CONVERT(INT, SCOPE_IDENTITY());"))
				{
					var userAndDomain = String.Format("{0}\\{1}", Environment.UserDomainName, Environment.UserName);

					command.AddParameter("name", surveyName);
					command.AddParameter("user", userAndDomain);
					command.AddParameter("machine", Environment.MachineName);

					result = (int) command.ExecuteScalar();
				}

				connection.Close();
			}

			return result;
		}

		private void UpdateSurveyRecord(int surveyId)
		{
			using (var connection = new SqlConnection(_destination.ToString()))
			{
				connection.Open();

				using (var command = connection.CreateCommand("UPDATE [dbo].[Surveys] SET [end] = GETDATE() WHERE [id] = @survey"))
				{
					command.AddParameter("survey", surveyId);

					command.ExecuteNonQuery();
				}

				connection.Close();
			}
		}

		private void InsertDatabaseSurvey(DatabaseSurvey survey)
		{
			using (var connection = new SqlConnection(_destination.ToString()))
			{
				connection.Open();

				using (var command = connection.CreateCommand("INSERT INTO [dbo].[DatabaseSurveys] ([survey], [server], [database], [is_reference_schema], [had_connection_error]) VALUES (@survey, @server, @database, @isReferenceSchema, @hadConnectionError); SELECT CONVERT(INT, SCOPE_IDENTITY());"))
				{
					command.AddParameter("survey", survey.SurveyId);
					command.AddParameter("server", survey.Server);
					command.AddParameter("database", survey.Database);
					command.AddParameter("isReferenceSchema", survey.IsReferenceSchema);
					command.AddParameter("hadConnectionError", survey.HadConnectionError);

					survey.Id = (int) command.ExecuteScalar();
				}

				connection.Close();
			}
		}

		private void UpdateDatabseSurvey(DatabaseSurvey survey)
		{
			using (var connection = new SqlConnection(_destination.ToString()))
			{
				connection.Open();

				using (var command = connection.CreateCommand("UPDATE [dbo].[DatabaseSurveys] SET [had_etl_error] = @hadEtlError, [duration] = @duration WHERE [survey] = @survey AND [server] = @server AND [database] = @database;"))
				{
					command.AddParameter("survey", survey.SurveyId);
					command.AddParameter("server", survey.Server);
					command.AddParameter("database", survey.Database);
					command.AddParameter("hadEtlError", survey.HadEtlError);
					command.AddParameter("duration", survey.Duration);

					command.ExecuteNonQuery();
				}

				connection.Close();
			}
		}
	}
}
