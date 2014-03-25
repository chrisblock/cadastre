using System;
using System.Collections.Generic;
using System.Linq;

namespace SchemaSurveyor.Core.ExtraObjects.Impl
{
	public class ExtraObjectRepository : BaseSchemaSurveyRepository, IExtraObjectRepository
	{
		public ExtraObjectCollection GetExtraObjects(int surveyId, int databaseSurveyId)
		{
			var result = new ExtraObjectCollection
			{
				Columns = GetExtraObjects(surveyId, databaseSurveyId, ObjectType.Column),
				Functions = GetExtraObjects(surveyId, databaseSurveyId, ObjectType.Function),
				Indexes = GetExtraObjects(surveyId, databaseSurveyId, ObjectType.Index),
				Principals = GetExtraObjects(surveyId, databaseSurveyId, ObjectType.Principal),
				Schemas = GetExtraObjects(surveyId, databaseSurveyId, ObjectType.Schema),
				Servers = GetExtraObjects(surveyId, databaseSurveyId, ObjectType.Server),
				StoredProcedures = GetExtraObjects(surveyId, databaseSurveyId, ObjectType.StoredProcedure),
				Synonyms = GetExtraObjects(surveyId, databaseSurveyId, ObjectType.Synonym),
				Tables = GetExtraObjects(surveyId, databaseSurveyId, ObjectType.Table),
				Triggers = GetExtraObjects(surveyId, databaseSurveyId, ObjectType.Trigger),
				Views = GetExtraObjects(surveyId, databaseSurveyId, ObjectType.View)
			};

			return result;
		}

		public IQueryable<ExtraObject> GetExtraObjects(int surveyId, int databaseSurveyId, ObjectType type)
		{
			IQueryable<ExtraObject> result;

			switch (type)
			{
				case ObjectType.Column:
					result = GetExtraColumns(surveyId, databaseSurveyId);
					break;
				case ObjectType.Function:
					result = GetExtraFunctions(surveyId, databaseSurveyId);
					break;
				case ObjectType.Index:
					result = GetExtraIndexes(surveyId, databaseSurveyId);
					break;
				case ObjectType.Principal:
					result = GetExtraPrincipals(surveyId, databaseSurveyId);
					break;
				case ObjectType.Schema:
					result = GetExtraSchemas(surveyId, databaseSurveyId);
					break;
				case ObjectType.Server:
					result = GetExtraServers(surveyId, databaseSurveyId);
					break;
				case ObjectType.StoredProcedure:
					result = GetExtraStoredProcedures(surveyId, databaseSurveyId);
					break;
				case ObjectType.Synonym:
					result = GetExtraSynonyms(surveyId, databaseSurveyId);
					break;
				case ObjectType.Table:
					result = GetExtraTables(surveyId, databaseSurveyId);
					break;
				case ObjectType.Trigger:
					result = GetExtraTriggers(surveyId, databaseSurveyId);
					break;
				case ObjectType.View:
					result = GetExtraViews(surveyId, databaseSurveyId);
					break;
				default:
					throw new ArgumentOutOfRangeException("type");
			}

			return result;
		}

		private IQueryable<ExtraObject> GetExtraColumns(int surveyId, int databaseSurveyId)
		{

			var result = new List<ExtraObject>();

			using (var connection = GetSchemaSurveyConnection())
			{
				connection.Open();

				using (var command = connection.CreateCommand("SELECT [survey], [database_survey], [table], [column] FROM [dbo].[ExtraColumns] WHERE [survey] = @surveyId AND [database_survey] = @databaseSurveyId"))
				{
					command.AddParameter("surveyId", surveyId);
					command.AddParameter("databaseSurveyId", databaseSurveyId);

					using (var reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							var survey = reader["survey"] as int?;
							var databaseSurvey = reader["database_survey"] as int?;
							var table = reader["table"] as string;
							var column = reader["column"] as string;

							var obj = new ExtraObject
							{
								Survey = survey.GetValueOrDefault(),
								Database = databaseSurvey.GetValueOrDefault(),
								Parent = table,
								Name = column,
								Type = ObjectType.Column
							};

							result.Add(obj);
						}

						reader.Close();
					}
				}

				connection.Close();
			}

			return result.AsQueryable();
		}

		private IQueryable<ExtraObject> GetExtraFunctions(int surveyId, int databaseSurveyId)
		{
			var result = new List<ExtraObject>();

			using (var connection = GetSchemaSurveyConnection())
			{
				connection.Open();

				using (var command = connection.CreateCommand("SELECT [survey], [database_survey], [function] FROM [dbo].[ExtraFunctions] WHERE [survey] = @surveyId AND [database_survey] = @databaseSurveyId"))
				{
					command.AddParameter("surveyId", surveyId);
					command.AddParameter("databaseSurveyId", databaseSurveyId);

					using (var reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							var survey = reader["survey"] as int?;
							var databaseSurvey = reader["database_survey"] as int?;
							var function = reader["function"] as string;

							var obj = new ExtraObject
							{
								Survey = survey.GetValueOrDefault(),
								Database = databaseSurvey.GetValueOrDefault(),
								Name = function,
								Type = ObjectType.Function
							};

							result.Add(obj);
						}

						reader.Close();
					}
				}

				connection.Close();
			}

			return result.AsQueryable();
		}

		private IQueryable<ExtraObject> GetExtraIndexes(int surveyId, int databaseSurveyId)
		{

			var result = new List<ExtraObject>();

			using (var connection = GetSchemaSurveyConnection())
			{
				connection.Open();

				using (var command = connection.CreateCommand("SELECT [survey], [database_survey], [table], [index] FROM [dbo].[ExtraIndexes] WHERE [survey] = @surveyId AND [database_survey] = @databaseSurveyId"))
				{
					command.AddParameter("surveyId", surveyId);
					command.AddParameter("databaseSurveyId", databaseSurveyId);

					using (var reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							var survey = reader["survey"] as int?;
							var databaseSurvey = reader["database_survey"] as int?;
							var table = reader["table"] as string;
							var index = reader["index"] as string;

							var obj = new ExtraObject
							{
								Survey = survey.GetValueOrDefault(),
								Database = databaseSurvey.GetValueOrDefault(),
								Parent = table,
								Name = index,
								Type = ObjectType.Index
							};

							result.Add(obj);
						}

						reader.Close();
					}
				}

				connection.Close();
			}

			return result.AsQueryable();
		}

		private IQueryable<ExtraObject> GetExtraPrincipals(int surveyId, int databaseSurveyId)
		{

			var result = new List<ExtraObject>();

			using (var connection = GetSchemaSurveyConnection())
			{
				connection.Open();

				using (var command = connection.CreateCommand("SELECT [survey], [database_survey], [principal] FROM [dbo].[ExtraPrincipals] WHERE [survey] = @surveyId AND [database_survey] = @databaseSurveyId"))
				{
					command.AddParameter("surveyId", surveyId);
					command.AddParameter("databaseSurveyId", databaseSurveyId);

					using (var reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							var survey = reader["survey"] as int?;
							var databaseSurvey = reader["database_survey"] as int?;
							var principal = reader["principal"] as string;

							var obj = new ExtraObject
							{
								Survey = survey.GetValueOrDefault(),
								Database = databaseSurvey.GetValueOrDefault(),
								Name = principal,
								Type = ObjectType.Principal
							};

							result.Add(obj);
						}

						reader.Close();
					}
				}

				connection.Close();
			}

			return result.AsQueryable();
		}

		private IQueryable<ExtraObject> GetExtraSchemas(int surveyId, int databaseSurveyId)
		{
			var result = new List<ExtraObject>();

			using (var connection = GetSchemaSurveyConnection())
			{
				connection.Open();

				using (var command = connection.CreateCommand("SELECT [survey], [database_survey], [schema] FROM [dbo].[ExtraSchemas] WHERE [survey] = @surveyId AND [database_survey] = @databaseSurveyId"))
				{
					command.AddParameter("surveyId", surveyId);
					command.AddParameter("databaseSurveyId", databaseSurveyId);

					using (var reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							var survey = reader["survey"] as int?;
							var databaseSurvey = reader["database_survey"] as int?;
							var schema = reader["schema"] as string;

							var obj = new ExtraObject
							{
								Survey = survey.GetValueOrDefault(),
								Database = databaseSurvey.GetValueOrDefault(),
								Name = schema,
								Type = ObjectType.Schema
							};

							result.Add(obj);
						}

						reader.Close();
					}
				}

				connection.Close();
			}

			return result.AsQueryable();
		}

		private IQueryable<ExtraObject> GetExtraServers(int surveyId, int databaseSurveyId)
		{

			var result = new List<ExtraObject>();

			using (var connection = GetSchemaSurveyConnection())
			{
				connection.Open();

				using (var command = connection.CreateCommand("SELECT [survey], [database_survey], [server] FROM [dbo].[ExtraServers] WHERE [survey] = @surveyId AND [database_survey] = @databaseSurveyId"))
				{
					command.AddParameter("surveyId", surveyId);
					command.AddParameter("databaseSurveyId", databaseSurveyId);

					using (var reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							var survey = reader["survey"] as int?;
							var databaseSurvey = reader["database_survey"] as int?;
							var server = reader["server"] as string;

							var obj = new ExtraObject
							{
								Survey = survey.GetValueOrDefault(),
								Database = databaseSurvey.GetValueOrDefault(),
								Name = server,
								Type = ObjectType.Server
							};

							result.Add(obj);
						}

						reader.Close();
					}
				}

				connection.Close();
			}

			return result.AsQueryable();
		}

		private IQueryable<ExtraObject> GetExtraStoredProcedures(int surveyId, int databaseSurveyId)
		{
			var result = new List<ExtraObject>();

			using (var connection = GetSchemaSurveyConnection())
			{
				connection.Open();

				using (var command = connection.CreateCommand("SELECT [survey], [database_survey], [stored_procedure] FROM [dbo].[ExtraStoredProcedures] WHERE [survey] = @surveyId AND [database_survey] = @databaseSurveyId"))
				{
					command.AddParameter("surveyId", surveyId);
					command.AddParameter("databaseSurveyId", databaseSurveyId);

					using (var reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							var survey = reader["survey"] as int?;
							var databaseSurvey = reader["database_survey"] as int?;
							var procedure = reader["stored_procedure"] as string;

							var obj = new ExtraObject
							{
								Survey = survey.GetValueOrDefault(),
								Database = databaseSurvey.GetValueOrDefault(),
								Name = procedure,
								Type = ObjectType.StoredProcedure
							};

							result.Add(obj);
						}

						reader.Close();
					}
				}

				connection.Close();
			}

			return result.AsQueryable();
		}

		private IQueryable<ExtraObject> GetExtraSynonyms(int surveyId, int databaseSurveyId)
		{

			var result = new List<ExtraObject>();

			using (var connection = GetSchemaSurveyConnection())
			{
				connection.Open();

				using (var command = connection.CreateCommand("SELECT [survey], [database_survey], [synonym] FROM [dbo].[ExtraSynonyms] WHERE [survey] = @surveyId AND [database_survey] = @databaseSurveyId"))
				{
					command.AddParameter("surveyId", surveyId);
					command.AddParameter("databaseSurveyId", databaseSurveyId);

					using (var reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							var survey = reader["survey"] as int?;
							var databaseSurvey = reader["database_survey"] as int?;
							var synonym = reader["synonym"] as string;

							var obj = new ExtraObject
							{
								Survey = survey.GetValueOrDefault(),
								Database = databaseSurvey.GetValueOrDefault(),
								Name = synonym,
								Type = ObjectType.Synonym
							};

							result.Add(obj);
						}

						reader.Close();
					}
				}

				connection.Close();
			}

			return result.AsQueryable();
		}

		private IQueryable<ExtraObject> GetExtraTables(int surveyId, int databaseSurveyId)
		{
			var result = new List<ExtraObject>();

			using (var connection = GetSchemaSurveyConnection())
			{
				connection.Open();

				using (var command = connection.CreateCommand("SELECT [survey], [database_survey], [table] FROM [dbo].[ExtraTables] WHERE [survey] = @surveyId AND [database_survey] = @databaseSurveyId"))
				{
					command.AddParameter("surveyId", surveyId);
					command.AddParameter("databaseSurveyId", databaseSurveyId);

					using (var reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							var survey = reader["survey"] as int?;
							var databaseSurvey = reader["database_survey"] as int?;
							var table = reader["table"] as string;

							var obj = new ExtraObject
							{
								Survey = survey.GetValueOrDefault(),
								Database = databaseSurvey.GetValueOrDefault(),
								Name = table,
								Type = ObjectType.Table
							};

							result.Add(obj);
						}

						reader.Close();
					}
				}

				connection.Close();
			}

			return result.AsQueryable();
		}

		private IQueryable<ExtraObject> GetExtraTriggers(int surveyId, int databaseSurveyId)
		{

			var result = new List<ExtraObject>();

			using (var connection = GetSchemaSurveyConnection())
			{
				connection.Open();

				using (var command = connection.CreateCommand("SELECT [survey], [database_survey], [table], [trigger] FROM [dbo].[ExtraTriggers] WHERE [survey] = @surveyId AND [database_survey] = @databaseSurveyId"))
				{
					command.AddParameter("surveyId", surveyId);
					command.AddParameter("databaseSurveyId", databaseSurveyId);

					using (var reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							var survey = reader["survey"] as int?;
							var databaseSurvey = reader["database_survey"] as int?;
							var table = reader["table"] as string;
							var trigger = reader["trigger"] as string;

							var obj = new ExtraObject
							{
								Survey = survey.GetValueOrDefault(),
								Database = databaseSurvey.GetValueOrDefault(),
								Parent = table,
								Name = trigger,
								Type = ObjectType.Function
							};

							result.Add(obj);
						}

						reader.Close();
					}
				}

				connection.Close();
			}

			return result.AsQueryable();
		}

		private IQueryable<ExtraObject> GetExtraViews(int surveyId, int databaseSurveyId)
		{
			var result = new List<ExtraObject>();

			using (var connection = GetSchemaSurveyConnection())
			{
				connection.Open();

				using (var command = connection.CreateCommand("SELECT [survey], [database_survey], [view] FROM [dbo].[ExtraViews] WHERE [survey] = @surveyId AND [database_survey] = @databaseSurveyId"))
				{
					command.AddParameter("surveyId", surveyId);
					command.AddParameter("databaseSurveyId", databaseSurveyId);

					using (var reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							var survey = reader["survey"] as int?;
							var databaseSurvey = reader["database_survey"] as int?;
							var view = reader["view"] as string;

							var obj = new ExtraObject
							{
								Survey = survey.GetValueOrDefault(),
								Database = databaseSurvey.GetValueOrDefault(),
								Name = view,
								Type = ObjectType.View
							};

							result.Add(obj);
						}

						reader.Close();
					}
				}

				connection.Close();
			}

			return result.AsQueryable();
		}
	}
}
