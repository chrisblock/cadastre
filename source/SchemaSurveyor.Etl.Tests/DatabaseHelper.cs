using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

using SchemaSurveyor.Core;

namespace SchemaSurveyor.Etl.Tests
{
	public static class DatabaseHelper
	{
		private static void RunQuery(IDbConnection connection, string sql)
		{
			using (var command = connection.CreateCommand(sql))
			{
				command.ExecuteNonQuery();
			}
		}

		public static void DropDatabase(string server, string database)
		{
			var connectionString = new SqlConnectionStringBuilder
			{
				DataSource = server,
				IntegratedSecurity = true
			};

			using (var connection = new SqlConnection(connectionString.ToString()))
			{
				connection.Open();

				var stringBuilder = new StringBuilder();

				stringBuilder.AppendLine(String.Format("IF EXISTS (SELECT TOP 1 NULL FROM [sys].[databases] WHERE [name] = '{0}')", database));
				stringBuilder.AppendLine("BEGIN");
				stringBuilder.AppendLine(String.Format("ALTER DATABASE [{0}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;", database));
				stringBuilder.AppendLine(String.Format("DROP DATABASE [{0}];", database));
				stringBuilder.AppendLine("END");

				RunQuery(connection, stringBuilder.ToString());

				connection.Close();
			}
		}

		public static void CreateDatabase(string server, string database)
		{
			var connectionString = new SqlConnectionStringBuilder
			{
				DataSource = server,
				IntegratedSecurity = true
			};

			using (var connection = new SqlConnection(connectionString.ToString()))
			{
				connection.Open();

				var sql = String.Format("CREATE DATABASE [{0}]; ALTER DATABASE [{0}] SET RECOVERY SIMPLE;", database);

				using (var command = connection.CreateCommand(sql))
				{
					command.ExecuteNonQuery();
				}

				connection.Close();
			}
		}

		public static void CreateReferenceObjects(string server, string database)
		{
			var connectionString = new SqlConnectionStringBuilder
			{
				DataSource = server,
				InitialCatalog = database,
				IntegratedSecurity = true
			};

			using (var connection = new SqlConnection(connectionString.ToString()))
			{
				connection.Open();

				CreateObjects(connection);

				DropExtraObjects(connection);

				connection.Close();
			}
		}

		public static void CreateOtherObjects(string server, string database)
		{
			var connectionString = new SqlConnectionStringBuilder
			{
				DataSource = server,
				InitialCatalog = database,
				IntegratedSecurity = true
			};

			using (var connection = new SqlConnection(connectionString.ToString()))
			{
				connection.Open();

				CreateObjects(connection);

				DropMissingObjects(connection);

				connection.Close();
			}
		}

		private static void CreateObjects(IDbConnection connection)
		{
			var stringBuilder = new StringBuilder();

			stringBuilder.AppendLine("CREATE TABLE [dbo].[MissingTable]");
			stringBuilder.AppendLine("(");
			stringBuilder.AppendLine("	[MissingTableColumn1] INT IDENTITY(1, 1),");
			stringBuilder.AppendLine("	CONSTRAINT [PK__MissingTable] PRIMARY KEY ([MissingTableColumn1] ASC)");
			stringBuilder.AppendLine(");");

			RunQuery(connection, stringBuilder.ToString());

			stringBuilder.Clear();

			stringBuilder.AppendLine("CREATE TABLE [dbo].[ExistingTable]");
			stringBuilder.AppendLine("(");
			stringBuilder.AppendLine("	[MissingColumn] INT IDENTITY(1, 1),");
			stringBuilder.AppendLine("	[ExistingColumn] INT,");
			stringBuilder.AppendLine("	[ExtraColumn] INT,");
			stringBuilder.AppendLine("	CONSTRAINT [PK__ExistingTable] PRIMARY KEY ([ExistingColumn] ASC)");
			stringBuilder.AppendLine(");");

			RunQuery(connection, stringBuilder.ToString());

			stringBuilder.Clear();

			stringBuilder.AppendLine("CREATE NONCLUSTERED INDEX [IDX__ExistingTable__MissingIndex] ON [dbo].[ExistingTable] ([MissingColumn] ASC);");
			stringBuilder.AppendLine("CREATE NONCLUSTERED INDEX [IDX__ExistingTable__ExistingIndex] ON [dbo].[ExistingTable] ([ExistingColumn] ASC);");
			stringBuilder.AppendLine("CREATE NONCLUSTERED INDEX [IDX__ExistingTable__ExtraIndex] ON [dbo].[ExistingTable] ([ExtraColumn] ASC);");

			RunQuery(connection, stringBuilder.ToString());

			stringBuilder.Clear();

			stringBuilder.AppendLine("CREATE TABLE [dbo].[ExtraTable]");
			stringBuilder.AppendLine("(");
			stringBuilder.AppendLine("	[ExtraTableColumn1] INT IDENTITY(1, 1),");
			stringBuilder.AppendLine("	CONSTRAINT [PK__ExtraTable] PRIMARY KEY ([ExtraTableColumn1] ASC)");
			stringBuilder.AppendLine(");");

			RunQuery(connection, stringBuilder.ToString());

			stringBuilder.Clear();

			stringBuilder.AppendLine("CREATE VIEW [dbo].[MissingView]");
			stringBuilder.AppendLine("(");
			stringBuilder.AppendLine("	[MissingTableColumn1]");
			stringBuilder.AppendLine(")");
			stringBuilder.AppendLine("AS (");
			stringBuilder.AppendLine("	SELECT [MissingTableColumn1] FROM [dbo].[MissingTable]");
			stringBuilder.AppendLine(")");

			RunQuery(connection, stringBuilder.ToString());

			stringBuilder.Clear();

			stringBuilder.AppendLine("CREATE VIEW [dbo].[ExistingView]");
			stringBuilder.AppendLine("(");
			stringBuilder.AppendLine("	[ExistingColumn]");
			stringBuilder.AppendLine(")");
			stringBuilder.AppendLine("AS (");
			stringBuilder.AppendLine("	SELECT [ExistingColumn] FROM [dbo].[ExistingTable]");
			stringBuilder.AppendLine(")");

			RunQuery(connection, stringBuilder.ToString());

			stringBuilder.Clear();

			stringBuilder.AppendLine("CREATE VIEW [dbo].[ExtraView]");
			stringBuilder.AppendLine("(");
			stringBuilder.AppendLine("	[ExtraTableColumn1]");
			stringBuilder.AppendLine(")");
			stringBuilder.AppendLine("AS (");
			stringBuilder.AppendLine("	SELECT [ExtraTableColumn1] FROM [dbo].[ExtraTable]");
			stringBuilder.AppendLine(")");

			RunQuery(connection, stringBuilder.ToString());

			stringBuilder.Clear();

			stringBuilder.AppendLine("CREATE PROCEDURE [dbo].[MissingProcedure]");
			stringBuilder.AppendLine("AS");
			stringBuilder.AppendLine("BEGIN");
			stringBuilder.AppendLine("	RETURN 1");
			stringBuilder.AppendLine("END");

			RunQuery(connection, stringBuilder.ToString());

			stringBuilder.Clear();

			stringBuilder.AppendLine("CREATE PROCEDURE [dbo].[ExistingProcedure]");
			stringBuilder.AppendLine("AS");
			stringBuilder.AppendLine("BEGIN");
			stringBuilder.AppendLine("	RETURN 1");
			stringBuilder.AppendLine("END");

			RunQuery(connection, stringBuilder.ToString());

			stringBuilder.Clear();

			stringBuilder.AppendLine("CREATE PROCEDURE [dbo].[ExtraProcedure]");
			stringBuilder.AppendLine("AS");
			stringBuilder.AppendLine("BEGIN");
			stringBuilder.AppendLine("	RETURN 1");
			stringBuilder.AppendLine("END");

			RunQuery(connection, stringBuilder.ToString());

			stringBuilder.Clear();

			stringBuilder.AppendLine("CREATE FUNCTION [dbo].[MissingFunction] ()");
			stringBuilder.AppendLine("RETURNS INT");
			stringBuilder.AppendLine("BEGIN");
			stringBuilder.AppendLine("	RETURN 1");
			stringBuilder.AppendLine("END");

			RunQuery(connection, stringBuilder.ToString());

			stringBuilder.Clear();

			stringBuilder.AppendLine("CREATE FUNCTION [dbo].[ExistingFunction] ()");
			stringBuilder.AppendLine("RETURNS INT");
			stringBuilder.AppendLine("BEGIN");
			stringBuilder.AppendLine("	RETURN 1");
			stringBuilder.AppendLine("END");

			RunQuery(connection, stringBuilder.ToString());

			stringBuilder.Clear();

			stringBuilder.AppendLine("CREATE FUNCTION [dbo].[ExtraFunction] ()");
			stringBuilder.AppendLine("RETURNS INT");
			stringBuilder.AppendLine("BEGIN");
			stringBuilder.AppendLine("	RETURN 1");
			stringBuilder.AppendLine("END");

			RunQuery(connection, stringBuilder.ToString());

			stringBuilder.Clear();

			stringBuilder.AppendLine("CREATE TRIGGER [dbo].[MissingTrigger]");
			stringBuilder.AppendLine("ON [dbo].[ExistingTable]");
			stringBuilder.AppendLine("FOR UPDATE");
			stringBuilder.AppendLine("AS SELECT * FROM [dbo].[ExistingTable]");

			RunQuery(connection, stringBuilder.ToString());

			stringBuilder.Clear();

			stringBuilder.AppendLine("CREATE TRIGGER [dbo].[ExistingTrigger]");
			stringBuilder.AppendLine("ON [dbo].[ExistingTable]");
			stringBuilder.AppendLine("FOR UPDATE");
			stringBuilder.AppendLine("AS SELECT * FROM [dbo].[ExistingTable]");

			RunQuery(connection, stringBuilder.ToString());

			stringBuilder.Clear();

			stringBuilder.AppendLine("CREATE TRIGGER [dbo].[ExtraTrigger]");
			stringBuilder.AppendLine("ON [dbo].[ExistingTable]");
			stringBuilder.AppendLine("FOR UPDATE");
			stringBuilder.AppendLine("AS SELECT * FROM [dbo].[ExistingTable]");

			RunQuery(connection, stringBuilder.ToString());

			stringBuilder.Clear();

			stringBuilder.AppendLine("CREATE SCHEMA [MissingSchema];");

			RunQuery(connection, stringBuilder.ToString());

			stringBuilder.Clear();

			stringBuilder.AppendLine("CREATE SCHEMA [ExistingSchema];");

			RunQuery(connection, stringBuilder.ToString());

			stringBuilder.Clear();

			stringBuilder.AppendLine("CREATE SCHEMA [ExtraSchema];");

			RunQuery(connection, stringBuilder.ToString());
		}

		private static void DropExtraObjects(IDbConnection connection)
		{
			var stringBuilder = new StringBuilder();

			stringBuilder.AppendLine("IF EXISTS (SELECT TOP 1 NULL FROM [sys].[indexes] WHERE [object_id] = OBJECT_ID('[dbo].[ExistingTable]') AND [name] = 'IDX__ExistingTable__ExtraIndex') DROP INDEX [IDX__ExistingTable__ExtraIndex] ON [dbo].[ExistingTable]");
			stringBuilder.AppendLine("IF EXISTS (SELECT TOP 1 NULL FROM [sys].[tables] WHERE [name] = 'ExtraTable') DROP TABLE [dbo].[ExtraTable]");
			stringBuilder.AppendLine("IF EXISTS (SELECT TOP 1 NULL FROM [sys].[views] WHERE [name] = 'ExtraView') DROP VIEW [dbo].[ExtraView]");
			stringBuilder.AppendLine("IF EXISTS (SELECT TOP 1 NULL FROM [sys].[procedures] WHERE [name] = 'ExtraProcedure') DROP PROCEDURE [dbo].[ExtraProcedure]");
			stringBuilder.AppendLine("IF EXISTS (SELECT TOP 1 NULL FROM [sys].[objects] WHERE [name] = 'ExtraFunction') DROP FUNCTION [dbo].[ExtraFunction]");
			stringBuilder.AppendLine("IF EXISTS (SELECT TOP 1 NULL FROM [sys].[columns] WHERE [name] = 'ExtraColumn' AND [object_id] = OBJECT_ID('[dbo].[ExistingTable]')) ALTER TABLE [dbo].[ExistingTable] DROP COLUMN [ExtraColumn]");
			stringBuilder.AppendLine("IF EXISTS (SELECT TOP 1 NULL FROM [sys].[triggers] WHERE [name] = 'ExtraTrigger') DROP TRIGGER [ExtraTrigger]");
			stringBuilder.AppendLine("IF EXISTS (SELECT TOP 1 NULL FROM [sys].[schemas] WHERE [name] = 'ExtraSchema') DROP SCHEMA [ExtraSchema]");

			RunQuery(connection, stringBuilder.ToString());
		}

		private static void DropMissingObjects(IDbConnection connection)
		{
			var stringBuilder = new StringBuilder();

			stringBuilder.AppendLine("IF EXISTS (SELECT TOP 1 NULL FROM [sys].[indexes] WHERE [object_id] = OBJECT_ID('[dbo].[ExistingTable]') AND [name] = 'IDX__ExistingTable__MissingIndex') DROP INDEX [IDX__ExistingTable__MissingIndex] ON [dbo].[ExistingTable]");
			stringBuilder.AppendLine("IF EXISTS (SELECT TOP 1 NULL FROM [sys].[tables] WHERE [name] = 'MissingTable') DROP TABLE [dbo].[MissingTable]");
			stringBuilder.AppendLine("IF EXISTS (SELECT TOP 1 NULL FROM [sys].[views] WHERE [name] = 'MissingView') DROP VIEW [dbo].[MissingView]");
			stringBuilder.AppendLine("IF EXISTS (SELECT TOP 1 NULL FROM [sys].[procedures] WHERE [name] = 'MissingProcedure') DROP PROCEDURE [dbo].[MissingProcedure]");
			stringBuilder.AppendLine("IF EXISTS (SELECT TOP 1 NULL FROM [sys].[objects] WHERE [name] = 'MissingFunction') DROP FUNCTION [dbo].[MissingFunction]");
			stringBuilder.AppendLine("IF EXISTS (SELECT TOP 1 NULL FROM [sys].[columns] WHERE [name] = 'MissingColumn' AND [object_id] = OBJECT_ID('[dbo].[ExistingTable]')) ALTER TABLE [dbo].[ExistingTable] DROP COLUMN [MissingColumn]");
			stringBuilder.AppendLine("IF EXISTS (SELECT TOP 1 NULL FROM [sys].[triggers] WHERE [name] = 'MissingTrigger') DROP TRIGGER [MissingTrigger]");
			stringBuilder.AppendLine("IF EXISTS (SELECT TOP 1 NULL FROM [sys].[schemas] WHERE [name] = 'MissingSchema') DROP SCHEMA [MissingSchema]");

			RunQuery(connection, stringBuilder.ToString());
		}
	}
}
