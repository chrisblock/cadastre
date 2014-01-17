using System;
using System.Collections.Generic;
using System.Linq;

namespace SchemaSurveyor.Core.MissingObjects.Impl
{
	public class MissingObjectRepository : BaseSchemaSurveyRepository, IMissingObjectRepository
	{
		public IQueryable<MissingObject> GetMissingObjects(int surveyId, int databaseSurveyId, ObjectType type)
		{
			IQueryable<MissingObject> result;

			switch (type)
			{
				case ObjectType.Column:
					result = GetMissingColumns(surveyId, databaseSurveyId);
					break;
				case ObjectType.Function:
					result = GetMissingFunctions(surveyId, databaseSurveyId);
					break;
				case ObjectType.Index:
					result = GetMissingIndexes(surveyId, databaseSurveyId);
					break;
				case ObjectType.Schema:
					result = GetMissingSchemas(surveyId, databaseSurveyId);
					break;
				case ObjectType.Server:
					result = GetMissingServers(surveyId, databaseSurveyId);
					break;
				case ObjectType.StoredProcedure:
					result = GetMissingStoredProcedures(surveyId, databaseSurveyId);
					break;
				case ObjectType.Synonym:
					result = GetMissingSynonyms(surveyId, databaseSurveyId);
					break;
				case ObjectType.Table:
					result = GetMissingTables(surveyId, databaseSurveyId);
					break;
				case ObjectType.Trigger:
					result = GetMissingTriggers(surveyId, databaseSurveyId);
					break;
				case ObjectType.View:
					result = GetMissingViews(surveyId, databaseSurveyId);
					break;
				default:
					throw new ArgumentOutOfRangeException("type");
			}

			return result;
		}

		private IQueryable<MissingObject> GetMissingColumns(int surveyId, int databaseSurveyId)
		{
			var result = new List<MissingObject>();

			using (var connection = GetSchemaSurveyConnection())
			{
				connection.Open();

				using (var command = connection.CreateCommand("SELECT [survey], [database_survey], [table], [column] FROM [dbo].[MissingColumns] WHERE [survey] = @surveyId AND [database_survey] = @databaseSurveyId"))
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

							var obj = new MissingObject
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

		private IQueryable<MissingObject> GetMissingFunctions(int surveyId, int databaseSurveyId)
		{
			var result = new List<MissingObject>();

			using (var connection = GetSchemaSurveyConnection())
			{
				connection.Open();

				using (var command = connection.CreateCommand("SELECT [survey], [database_survey], [function] FROM [dbo].[MissingFunctions] WHERE [survey] = @surveyId AND [database_survey] = @databaseSurveyId"))
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

							var obj = new MissingObject
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

		private IQueryable<MissingObject> GetMissingIndexes(int surveyId, int databaseSurveyId)
		{
			var result = new List<MissingObject>();

			using (var connection = GetSchemaSurveyConnection())
			{
				connection.Open();

				using (var command = connection.CreateCommand("SELECT [survey], [database_survey], [table], [index] FROM [dbo].[MissingIndexes] WHERE [survey] = @surveyId AND [database_survey] = @databaseSurveyId"))
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

							var obj = new MissingObject
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

		private IQueryable<MissingObject> GetMissingSchemas(int surveyId, int databaseSurveyId)
		{
			var result = new List<MissingObject>();

			using (var connection = GetSchemaSurveyConnection())
			{
				connection.Open();

				using (var command = connection.CreateCommand("SELECT [survey], [database_survey], [schema] FROM [dbo].[MissingSchemas] WHERE [survey] = @surveyId AND [database_survey] = @databaseSurveyId"))
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

							var obj = new MissingObject
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

		private IQueryable<MissingObject> GetMissingServers(int surveyId, int databaseSurveyId)
		{
			var result = new List<MissingObject>();

			using (var connection = GetSchemaSurveyConnection())
			{
				connection.Open();

				using (var command = connection.CreateCommand("SELECT [survey], [database_survey], [server] FROM [dbo].[MissingServers] WHERE [survey] = @surveyId AND [database_survey] = @databaseSurveyId"))
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

							var obj = new MissingObject
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

		private IQueryable<MissingObject> GetMissingStoredProcedures(int surveyId, int databaseSurveyId)
		{
			var result = new List<MissingObject>();

			using (var connection = GetSchemaSurveyConnection())
			{
				connection.Open();

				using (var command = connection.CreateCommand("SELECT [survey], [database_survey], [stored_procedure] FROM [dbo].[MissingStoredProcedures] WHERE [survey] = @surveyId AND [database_survey] = @databaseSurveyId"))
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

							var obj = new MissingObject
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

		private IQueryable<MissingObject> GetMissingSynonyms(int surveyId, int databaseSurveyId)
		{
			var result = new List<MissingObject>();

			using (var connection = GetSchemaSurveyConnection())
			{
				connection.Open();

				using (var command = connection.CreateCommand("SELECT [survey], [database_survey], [synonym] FROM [dbo].[MissingSynonyms] WHERE [survey] = @surveyId AND [database_survey] = @databaseSurveyId"))
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

							var obj = new MissingObject
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

		private IQueryable<MissingObject> GetMissingTables(int surveyId, int databaseSurveyId)
		{
			var result = new List<MissingObject>();

			using (var connection = GetSchemaSurveyConnection())
			{
				connection.Open();

				using (var command = connection.CreateCommand("SELECT [survey], [database_survey], [table] FROM [dbo].[MissingTables] WHERE [survey] = @surveyId AND [database_survey] = @databaseSurveyId"))
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

							var obj = new MissingObject
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

		private IQueryable<MissingObject> GetMissingTriggers(int surveyId, int databaseSurveyId)
		{
			var result = new List<MissingObject>();

			using (var connection = GetSchemaSurveyConnection())
			{
				connection.Open();

				using (var command = connection.CreateCommand("SELECT [survey], [database_survey], [table], [trigger] FROM [dbo].[MissingTriggers] WHERE [survey] = @surveyId AND [database_survey] = @databaseSurveyId"))
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

							var obj = new MissingObject
							{
								Survey = survey.GetValueOrDefault(),
								Database = databaseSurvey.GetValueOrDefault(),
								Parent = table,
								Name = trigger,
								Type = ObjectType.Trigger
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

		private IQueryable<MissingObject> GetMissingViews(int surveyId, int databaseSurveyId)
		{
			var result = new List<MissingObject>();

			using (var connection = GetSchemaSurveyConnection())
			{
				connection.Open();

				using (var command = connection.CreateCommand("SELECT [survey], [database_survey], [view] FROM [dbo].[MissingViews] WHERE [survey] = @surveyId AND [database_survey] = @databaseSurveyId"))
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

							var obj = new MissingObject
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
