using System;
using System.Collections.Generic;
using System.Linq;

namespace SchemaSurveyor.Core.Surveys.Impl
{
	public class SurveyRepository : BaseSchemaSurveyRepository, ISurveyRepository
	{
		public IQueryable<Survey> Get()
		{
			var result = Enumerable.Empty<Survey>().AsQueryable();

			using (var connection = GetSchemaSurveyConnection())
			{
				connection.Open();

				using (var command = connection.CreateCommand("SELECT [id], [name], [machine_name], [user_name], [start_time], [end_time] FROM [dbo].[Surveys]"))
				{
					using (var reader = command.ExecuteReader())
					{
						var surveys = new List<Survey>();

						while (reader.Read())
						{
							var survey = new Survey
							{
								Id = (int) reader["id"],
								Name = (string) reader["name"],
								Machine = (string) reader["machine_name"],
								User = (string) reader["user_name"],
								Start = (DateTime) reader["start_time"],
								End = reader["end_time"] == DBNull.Value ? null : (DateTime?) reader["end_time"]
							};

							surveys.Add(survey);
						}

						if (surveys.Any())
						{
							result = surveys.AsQueryable();
						}

						reader.Close();
					}
				}

				connection.Close();
			}

			return result;
		}

		public Survey Get(int surveyId)
		{
			Survey result = null;

			using (var connection = GetSchemaSurveyConnection())
			{
				connection.Open();

				using (var command = connection.CreateCommand("SELECT TOP 1 [id], [name], [machine_name], [user_name], [start_time], [end_time] FROM [dbo].[Surveys] WHERE [id] = @id"))
				{
					command.AddParameter("id", surveyId);

					using (var reader = command.ExecuteReader())
					{
						if (reader.Read())
						{
							result = new Survey
							{
								Id = (int) reader["id"],
								Name = (string) reader["name"],
								Machine = (string) reader["machine_name"],
								User = (string) reader["user_name"],
								Start = (DateTime) reader["start_time"],
								End = reader["end_time"] == DBNull.Value ? null : (DateTime?) reader["end_time"]
							};
						}

						reader.Close();
					}
				}

				connection.Close();
			}

			return result;
		}

		public int Insert(string surveyName)
		{
			int result;

			using (var connection = GetSchemaSurveyConnection())
			{
				connection.Open();

				using (var command = connection.CreateCommand("INSERT INTO [dbo].[Surveys] ([name], [user_name], [machine_name], [start_time]) VALUES (@name, @user, @machine, GETDATE()); SELECT CONVERT(INT, SCOPE_IDENTITY());"))
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

		public void Update(int surveyId)
		{
			using (var connection = GetSchemaSurveyConnection())
			{
				connection.Open();

				using (var command = connection.CreateCommand("UPDATE [dbo].[Surveys] SET [end_time] = GETDATE() WHERE [id] = @survey"))
				{
					command.AddParameter("survey", surveyId);

					command.ExecuteNonQuery();
				}

				connection.Close();
			}
		}

		public IQueryable<DatabaseSurvey> GetDatabaseSurveys(int surveyId)
		{
			var result = Enumerable.Empty<DatabaseSurvey>().AsQueryable();

			using (var connection = GetSchemaSurveyConnection())
			{
				connection.Open();

				using (var command = connection.CreateCommand("SELECT [id], [survey], [instance_name], [database_name], [is_reference_schema], [had_connection_error], [had_etl_error], [duration] FROM [dbo].[DatabaseSurveys] WHERE [survey] = @surveyId"))
				{
					command.AddParameter("surveyId", surveyId);

					using (var reader = command.ExecuteReader())
					{
						var databaseSurveys = new List<DatabaseSurvey>();

						while (reader.Read())
						{
							var databaseSurvey = new DatabaseSurvey
							{
								Id = (int) reader["id"],
								SurveyId = (int) reader["survey"],
								Server = (string) reader["instance_name"],
								Database = (string) reader["database_name"],
								IsReferenceSchema = (bool) reader["is_reference_schema"],
								HadConnectionError = (bool?) reader["had_connection_error"],
								//HadEtlError = (bool?) reader["had_etl_error"],
								Duration = (long?) reader["duration"],
							};

							databaseSurveys.Add(databaseSurvey);
						}

						if (databaseSurveys.Any())
						{
							result = databaseSurveys.AsQueryable();
						}

						reader.Close();
					}
				}

				connection.Close();
			}

			return result;
		}

		public void InsertDatabaseSurvey(DatabaseSurvey databaseSurvey)
		{
			using (var connection = GetSchemaSurveyConnection())
			{
				connection.Open();

				using (var command = connection.CreateCommand("INSERT INTO [dbo].[DatabaseSurveys] ([survey], [instance_name], [database_name], [is_reference_schema], [had_connection_error]) VALUES (@survey, @server, @database, @isReferenceSchema, @hadConnectionError); SELECT CONVERT(INT, SCOPE_IDENTITY());"))
				{
					command.AddParameter("survey", databaseSurvey.SurveyId);
					command.AddParameter("server", databaseSurvey.Server);
					command.AddParameter("database", databaseSurvey.Database);
					command.AddParameter("isReferenceSchema", databaseSurvey.IsReferenceSchema);
					command.AddParameter("hadConnectionError", databaseSurvey.HadConnectionError);

					databaseSurvey.Id = (int) command.ExecuteScalar();
				}

				connection.Close();
			}
		}

		public void UpdateDatabaseSurvey(DatabaseSurvey databaseSurvey)
		{
			using (var connection = GetSchemaSurveyConnection())
			{
				connection.Open();

				using (var command = connection.CreateCommand("UPDATE [dbo].[DatabaseSurveys] SET [had_etl_error] = @hadEtlError, [duration] = @duration WHERE [survey] = @survey AND [instance_name] = @server AND [database_name] = @database;"))
				{
					command.AddParameter("survey", databaseSurvey.SurveyId);
					command.AddParameter("server", databaseSurvey.Server);
					command.AddParameter("database", databaseSurvey.Database);
					command.AddParameter("hadEtlError", databaseSurvey.HadEtlError);
					command.AddParameter("duration", databaseSurvey.Duration);

					command.ExecuteNonQuery();
				}

				connection.Close();
			}
		}
	}
}
