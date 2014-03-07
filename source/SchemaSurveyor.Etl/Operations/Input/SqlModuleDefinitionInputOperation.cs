using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading;

namespace SchemaSurveyor.Etl.Operations.Input
{
	public class SqlModuleDefinitionInputOperation : SimpleInputCommandOperation
	{
		private static readonly Lazy<string> LazySelectSql = new Lazy<string>(BuildSelectSql, LazyThreadSafetyMode.ExecutionAndPublication);
		private static string SelectSql { get { return LazySelectSql.Value; } }

		private static string BuildSelectSql()
		{
			var stringBuilder = new StringBuilder();

			stringBuilder.AppendLine("SELECT");
			stringBuilder.AppendLine("	  [object_id]");
			stringBuilder.AppendLine("	, [definition]");
			stringBuilder.AppendLine("FROM [sys].[sql_modules]");

			return stringBuilder.ToString();
		}

		public SqlModuleDefinitionInputOperation(string connectionString) : base(connectionString)
		{
		}

		public SqlModuleDefinitionInputOperation(string server, string database) : base(server, database)
		{
		}

		public SqlModuleDefinitionInputOperation(SqlConnectionStringBuilder connectionStringBuilder) : base(connectionStringBuilder)
		{
		}

		protected override void PrepareCommand(IDbCommand cmd)
		{
			cmd.CommandText = SelectSql;
		}
	}
}
