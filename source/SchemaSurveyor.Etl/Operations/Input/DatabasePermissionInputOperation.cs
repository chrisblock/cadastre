using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading;

namespace SchemaSurveyor.Etl.Operations.Input
{
	public class DatabasePermissionInputOperation : SimpleInputCommandOperation
	{
		private static readonly Lazy<string> LazySelectSql = new Lazy<string>(BuildSelectSql, LazyThreadSafetyMode.ExecutionAndPublication);
		private static string SelectSql { get { return LazySelectSql.Value; } }

		private static string BuildSelectSql()
		{
			var stringBuilder = new StringBuilder();

			stringBuilder.AppendLine("SELECT");
			stringBuilder.AppendLine("	  @@SERVERNAME AS [server]");
			stringBuilder.AppendLine("	, DB_NAME() AS [database]");
			stringBuilder.AppendLine("	, [class]");
			stringBuilder.AppendLine("	, [class_desc]");
			stringBuilder.AppendLine("	, [major_id]");
			stringBuilder.AppendLine("	, [minor_id]");
			stringBuilder.AppendLine("	, [grantee_principal_id]");
			stringBuilder.AppendLine("	, [grantor_principal_id]");
			stringBuilder.AppendLine("	, [type]");
			stringBuilder.AppendLine("	, [permission_name]");
			stringBuilder.AppendLine("	, [state]");
			stringBuilder.AppendLine("	, [state_desc]");
			stringBuilder.AppendLine("FROM [sys].[database_permissions]");

			return stringBuilder.ToString();
		}

		public DatabasePermissionInputOperation(string connectionString) : base(connectionString)
		{
		}

		public DatabasePermissionInputOperation(string server, string database) : base(server, database)
		{
		}

		public DatabasePermissionInputOperation(SqlConnectionStringBuilder connectionStringBuilder) : base(connectionStringBuilder)
		{
		}

		protected override void PrepareCommand(IDbCommand cmd)
		{
			cmd.CommandText = SelectSql;
		}
	}
}
