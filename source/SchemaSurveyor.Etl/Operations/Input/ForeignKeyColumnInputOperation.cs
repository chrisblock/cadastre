using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading;

namespace SchemaSurveyor.Etl.Operations.Input
{
	public class ForeignKeyColumnInputOperation : SimpleInputCommandOperation
	{
		private static readonly Lazy<string> LazySelectSql = new Lazy<string>(BuildSelectSql, LazyThreadSafetyMode.ExecutionAndPublication);
		private static string SelectSql { get { return LazySelectSql.Value; } }

		private static string BuildSelectSql()
		{
			var stringBuilder = new StringBuilder();

			stringBuilder.AppendLine("SELECT");
			stringBuilder.AppendLine("	  [constraint_object_id]");
			stringBuilder.AppendLine("	, [constraint_column_id]");
			stringBuilder.AppendLine("	, [parent_object_id]");
			stringBuilder.AppendLine("	, [parent_column_id]");
			stringBuilder.AppendLine("	, [referenced_object_id]");
			stringBuilder.AppendLine("	, [referenced_column_id]");
			stringBuilder.AppendLine("FROM [sys].[foreign_key_columns]");

			return stringBuilder.ToString();
		}

		public ForeignKeyColumnInputOperation(string connectionString) : base(connectionString)
		{
		}

		public ForeignKeyColumnInputOperation(string server, string database) : base(server, database)
		{
		}

		public ForeignKeyColumnInputOperation(SqlConnectionStringBuilder connectionStringBuilder) : base(connectionStringBuilder)
		{
		}

		protected override void PrepareCommand(IDbCommand cmd)
		{
			cmd.CommandText = SelectSql;
		}
	}
}
