using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading;

namespace SchemaSurveyor.Etl.Operations.Input
{
	public class DatabaseRoleMemberInputOperation : SimpleInputCommandOperation
	{
		private static readonly Lazy<string> LazySelectSql = new Lazy<string>(BuildSelectSql, LazyThreadSafetyMode.ExecutionAndPublication);
		private static string SelectSql { get { return LazySelectSql.Value; } }

		private static string BuildSelectSql()
		{
			var stringBuilder = new StringBuilder();

			stringBuilder.AppendLine("SELECT");
			stringBuilder.AppendLine("	  @@SERVERNAME AS [server]");
			stringBuilder.AppendLine("	, DB_NAME() AS [database]");
			stringBuilder.AppendLine("	, [role_principal_id]");
			stringBuilder.AppendLine("	, [member_principal_id]");
			stringBuilder.AppendLine("FROM [sys].[database_role_members]");

			return stringBuilder.ToString();
		}

		public DatabaseRoleMemberInputOperation(string connectionString) : base(connectionString)
		{
		}

		public DatabaseRoleMemberInputOperation(string server, string database) : base(server, database)
		{
		}

		public DatabaseRoleMemberInputOperation(SqlConnectionStringBuilder connectionStringBuilder) : base(connectionStringBuilder)
		{
		}

		protected override void PrepareCommand(IDbCommand cmd)
		{
			cmd.CommandText = SelectSql;
		}
	}
}
