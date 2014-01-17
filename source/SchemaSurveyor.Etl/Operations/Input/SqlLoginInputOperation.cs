using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading;

namespace SchemaSurveyor.Etl.Operations.Input
{
	public class SqlLoginInputOperation : SimpleInputCommandOperation
	{
		private static readonly Lazy<string> LazySelectSql = new Lazy<string>(BuildSelectSql, LazyThreadSafetyMode.ExecutionAndPublication);
		private static string SelectSql { get { return LazySelectSql.Value; } }

		private static string BuildSelectSql()
		{
			var stringBuilder = new StringBuilder();

			stringBuilder.AppendLine("SELECT");
			stringBuilder.AppendLine("	  @@SERVERNAME AS [server]");
			stringBuilder.AppendLine("	, DB_NAME() AS [database]");
			stringBuilder.AppendLine("	, [name]");
			stringBuilder.AppendLine("	, [principal_id]");
			stringBuilder.AppendLine("	, [sid]");
			stringBuilder.AppendLine("	, [type]");
			stringBuilder.AppendLine("	, [type_desc]");
			stringBuilder.AppendLine("	, [is_disabled]");
			stringBuilder.AppendLine("	, [create_date]");
			stringBuilder.AppendLine("	, [modify_date]");
			stringBuilder.AppendLine("	, [default_database_name]");
			stringBuilder.AppendLine("	, [default_language_name]");
			stringBuilder.AppendLine("	, [credential_id]");
			stringBuilder.AppendLine("	, [is_policy_checked]");
			stringBuilder.AppendLine("	, [is_expiration_checked]");
			stringBuilder.AppendLine("	, [password_hash]");
			stringBuilder.AppendLine("FROM [sys].[sql_logins]");

			return stringBuilder.ToString();
		}

		public SqlLoginInputOperation(string connectionString) : base(connectionString)
		{
		}

		public SqlLoginInputOperation(string server, string database) : base(server, database)
		{
		}

		public SqlLoginInputOperation(SqlConnectionStringBuilder connectionStringBuilder) : base(connectionStringBuilder)
		{
		}

		protected override void PrepareCommand(IDbCommand cmd)
		{
			cmd.CommandText = SelectSql;
		}
	}
}
