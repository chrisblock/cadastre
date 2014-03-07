using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading;

namespace SchemaSurveyor.Etl.Operations.Input
{
	public class SqlModuleInputOperation : SimpleInputCommandOperation
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

		public SqlModuleInputOperation(string connectionString) : base(connectionString)
		{
		}

		public SqlModuleInputOperation(string server, string database) : base(server, database)
		{
		}

		public SqlModuleInputOperation(SqlConnectionStringBuilder connectionStringBuilder) : base(connectionStringBuilder)
		{
		}

		protected override void PrepareCommand(IDbCommand cmd)
		{
			cmd.CommandText = SelectSql;
		}
	}
}
