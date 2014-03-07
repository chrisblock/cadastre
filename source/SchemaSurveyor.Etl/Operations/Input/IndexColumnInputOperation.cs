using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading;

namespace SchemaSurveyor.Etl.Operations.Input
{
	public class IndexColumnInputOperation : SimpleInputCommandOperation
	{
		private static readonly Lazy<string> LazySelectSql = new Lazy<string>(BuildSelectSql, LazyThreadSafetyMode.ExecutionAndPublication);
		private static string SelectSql { get { return LazySelectSql.Value; } }

		private static string BuildSelectSql()
		{
			var stringBuilder = new StringBuilder();

			stringBuilder.AppendLine("SELECT");
			stringBuilder.AppendLine("	  [object_id]");
			stringBuilder.AppendLine("	, [index_id]");
			stringBuilder.AppendLine("	, [index_column_id]");
			stringBuilder.AppendLine("	, [column_id]");
			stringBuilder.AppendLine("	, [key_ordinal]");
			stringBuilder.AppendLine("	, [partition_ordinal]");
			stringBuilder.AppendLine("	, [is_descending_key]");
			stringBuilder.AppendLine("	, [is_included_column]");
			stringBuilder.AppendLine("FROM [sys].[index_columns]");

			return stringBuilder.ToString();
		}

		public IndexColumnInputOperation(string connectionString) : base(connectionString)
		{
		}

		public IndexColumnInputOperation(string server, string database) : base(server, database)
		{
		}

		public IndexColumnInputOperation(SqlConnectionStringBuilder connectionStringBuilder) : base(connectionStringBuilder)
		{
		}

		protected override void PrepareCommand(IDbCommand cmd)
		{
			cmd.CommandText = SelectSql;
		}
	}
}
