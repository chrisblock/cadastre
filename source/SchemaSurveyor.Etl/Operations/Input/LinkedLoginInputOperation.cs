using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading;

namespace SchemaSurveyor.Etl.Operations.Input
{
	public class LinkedLoginInputOperation : SimpleInputCommandOperation
	{
		private static readonly Lazy<string> LazySelectSql = new Lazy<string>(BuildSelectSql, LazyThreadSafetyMode.ExecutionAndPublication);
		private static string SelectSql { get { return LazySelectSql.Value; } }

		private static string BuildSelectSql()
		{
			var stringBuilder = new StringBuilder();

			stringBuilder.AppendLine("SELECT");
			stringBuilder.AppendLine("	  @@SERVERNAME AS [server]");
			stringBuilder.AppendLine("	, DB_NAME() AS [database]");
			stringBuilder.AppendLine("	, [server_id]");
			stringBuilder.AppendLine("	, [local_principal_id]");
			stringBuilder.AppendLine("	, [uses_self_credential]");
			stringBuilder.AppendLine("	, [remote_name]");
			stringBuilder.AppendLine("	, [modify_date]");
			stringBuilder.AppendLine("FROM [sys].[linked_logins]");

			return stringBuilder.ToString();
		}

		public LinkedLoginInputOperation(string connectionString) : base(connectionString)
		{
		}

		public LinkedLoginInputOperation(string server, string database) : base(server, database)
		{
		}

		public LinkedLoginInputOperation(SqlConnectionStringBuilder connectionStringBuilder) : base(connectionStringBuilder)
		{
		}

		protected override void PrepareCommand(IDbCommand cmd)
		{
			cmd.CommandText = SelectSql;
		}
	}
}
