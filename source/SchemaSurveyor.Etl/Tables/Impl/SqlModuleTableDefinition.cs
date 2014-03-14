using System;
using System.Text;
using System.Threading;

namespace SchemaSurveyor.Etl.Tables.Impl
{
	public class SqlModuleTableDefinition : AbstractTableDefinition
	{
		private static readonly Lazy<string> LazySelectSql = new Lazy<string>(BuildSelectSql, LazyThreadSafetyMode.ExecutionAndPublication);
		private static string SelectSql { get { return LazySelectSql.Value; } }

		private static string BuildSelectSql()
		{
			var stringBuilder = new StringBuilder();

			stringBuilder.AppendLine("SELECT");
			stringBuilder.AppendLine("	  [object_id]");
			stringBuilder.AppendLine("	, DATALENGTH([definition]) AS [definition_length]");
			stringBuilder.AppendLine("	, BINARY_CHECKSUM([definition]) AS [definition_checksum]");
			stringBuilder.AppendLine("	, [uses_ansi_nulls]");
			stringBuilder.AppendLine("	, [uses_quoted_identifier]");
			stringBuilder.AppendLine("	, [is_schema_bound]");
			stringBuilder.AppendLine("	, [uses_database_collation]");
			stringBuilder.AppendLine("	, [is_recompiled]");
			stringBuilder.AppendLine("	, [null_on_null_input]");
			stringBuilder.AppendLine("	, [execute_as_principal_id]");
			stringBuilder.AppendLine("FROM [sys].[sql_modules]");

			return stringBuilder.ToString();
		}

		public SqlModuleTableDefinition() : base("SqlModules")
		{
			//RegisterColumn("database_survey", typeof (int));
			RegisterColumn("object_id", typeof (int));
			RegisterColumn("definition_length", typeof (int));
			RegisterColumn("definition_checksum", typeof (int));
			RegisterColumn("uses_database_collation", typeof (bool?));
			RegisterColumn("uses_ansi_nulls", typeof (bool?));
			RegisterColumn("uses_quoted_identifier", typeof (bool?));
			RegisterColumn("is_schema_bound", typeof (bool?));
			RegisterColumn("uses_database_collation", typeof (bool?));
			RegisterColumn("is_recompiled", typeof (bool?));
			RegisterColumn("null_on_null_input", typeof (bool?));
			RegisterColumn("execute_as_principal_id", typeof (int?));
		}

		public override string GetSelectStatement()
		{
			return SelectSql;
		}
	}
}
