using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading;

namespace SchemaSurveyor.Etl.Operations.Input
{
	public class DatabaseTypeMappingInputOperation : SimpleInputCommandOperation
	{
		private static readonly Lazy<string> LazySelectSql = new Lazy<string>(BuildSelectSql, LazyThreadSafetyMode.ExecutionAndPublication);
		private static string SelectSql { get { return LazySelectSql.Value; } }

		private static string BuildSelectSql()
		{
			var stringBuilder = new StringBuilder();

			stringBuilder.AppendLine("SELECT");
			stringBuilder.AppendLine("	  [server]");
			stringBuilder.AppendLine("	, [database]");
			stringBuilder.AppendLine("	, 'DatabaseType' AS [database_type]");
			stringBuilder.AppendLine("FROM [dbo].[DB]");
			stringBuilder.AppendLine("WHERE LEN(COALESCE([server], '')) > 0");
			stringBuilder.AppendLine("AND LEN(COALESCE([database], '')) > 0");

			return stringBuilder.ToString();
		}

		public DatabaseTypeMappingInputOperation(string connectionString) : base(connectionString)
		{
		}

		public DatabaseTypeMappingInputOperation(string server, string database) : base(server, database)
		{
		}

		public DatabaseTypeMappingInputOperation(SqlConnectionStringBuilder connectionStringBuilder) : base(connectionStringBuilder)
		{
		}

		protected override void PrepareCommand(IDbCommand cmd)
		{
			cmd.CommandText = SelectSql;
		}
	}
}
