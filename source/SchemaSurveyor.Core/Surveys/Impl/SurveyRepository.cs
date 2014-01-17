using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

				using (var command = connection.CreateCommand("SELECT [id], [name], [machine], [user], [start], [end] FROM [dbo].[Surveys]"))
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
								Machine = (string) reader["machine"],
								User = (string) reader["user"],
								Start = (DateTime) reader["start"],
								End = reader["end"] == DBNull.Value ? null : (DateTime?) reader["end"]
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

				using (var command = connection.CreateCommand("SELECT TOP 1 [id], [name], [machine], [user], [start], [end] FROM [dbo].[Surveys] WHERE [id] = @id"))
				{
					command.AddParameter("id", surveyId);

					using (var reader = command.ExecuteReader())
					{
						if (reader.Read())
						{
							result = new Survey
							{
								Id = (int)reader["id"],
								Name = (string)reader["name"],
								Machine = (string)reader["machine"],
								User = (string)reader["user"],
								Start = (DateTime)reader["start"],
								End = (DateTime)reader["end"]
							};
						}

						reader.Close();
					}
				}

				connection.Close();
			}

			return result;
		}

		public IQueryable<DatabaseSurvey> GetDatabaseSurveys(int surveyId)
		{
			var result = Enumerable.Empty<DatabaseSurvey>().AsQueryable();

			using (var connection = GetSchemaSurveyConnection())
			{
				connection.Open();

				using (var command = connection.CreateCommand("SELECT [id], [survey], [server], [database], [is_reference_schema], [had_connection_error], [had_etl_error], [duration] FROM [dbo].[DatabaseSurveys] WHERE [survey] = @surveyId"))
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
								Server = (string) reader["server"],
								Database = (string) reader["database"],
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
	}
}
