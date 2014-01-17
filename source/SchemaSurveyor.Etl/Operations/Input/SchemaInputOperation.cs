using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading;

namespace SchemaSurveyor.Etl.Operations.Input
{
	public class SchemaInputOperation : SimpleInputCommandOperation
	{
		private static readonly Lazy<string> LazySelectSql = new Lazy<string>(BuildSelectSql, LazyThreadSafetyMode.ExecutionAndPublication);
		private static string SelectSql { get { return LazySelectSql.Value; } }

		private static string BuildSelectSql()
		{
			var stringBuilder = new StringBuilder();

			stringBuilder.AppendLine("SELECT");
			stringBuilder.AppendLine("	  @@SERVERNAME AS [server]");
			stringBuilder.AppendLine("	, DB_NAME() as [database]");
			stringBuilder.AppendLine("	, [name]");
			stringBuilder.AppendLine("	, [schema_id]");
			stringBuilder.AppendLine("	, [principal_id]");
			stringBuilder.AppendLine("FROM [sys].[schemas]");

			return stringBuilder.ToString();
		}

		public SchemaInputOperation(string connectionString) : base(connectionString)
		{
		}

		public SchemaInputOperation(string server, string database) : base(server, database)
		{
		}

		public SchemaInputOperation(SqlConnectionStringBuilder connectionStringBuilder) : base(connectionStringBuilder)
		{
		}

		protected override void PrepareCommand(IDbCommand cmd)
		{
			cmd.CommandText = SelectSql;
		}
	}
}
