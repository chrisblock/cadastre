using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading;

namespace SchemaSurveyor.Etl.Operations.Input
{
	public class IndexInputOperation : SimpleInputCommandOperation
	{
		private static readonly Lazy<string> LazySelectSql = new Lazy<string>(BuildSelectSql, LazyThreadSafetyMode.ExecutionAndPublication);
		private static string SelectSql { get { return LazySelectSql.Value; } }

		private static string BuildSelectSql()
		{
			var stringBuilder = new StringBuilder();

			stringBuilder.AppendLine("SELECT");
			stringBuilder.AppendLine("	  [object_id]");
			stringBuilder.AppendLine("	, [name]");
			stringBuilder.AppendLine("	, [index_id]");
			stringBuilder.AppendLine("	, [type]");
			stringBuilder.AppendLine("	, [type_desc]");
			stringBuilder.AppendLine("	, [is_unique]");
			stringBuilder.AppendLine("	, [data_space_id]");
			stringBuilder.AppendLine("	, [ignore_dup_key]");
			stringBuilder.AppendLine("	, [is_primary_key]");
			stringBuilder.AppendLine("	, [is_unique_constraint]");
			stringBuilder.AppendLine("	, [fill_factor]");
			stringBuilder.AppendLine("	, [is_padded]");
			stringBuilder.AppendLine("	, [is_disabled]");
			stringBuilder.AppendLine("	, [is_hypothetical]");
			stringBuilder.AppendLine("	, [allow_row_locks]");
			stringBuilder.AppendLine("	, [allow_page_locks]");
			// TODO: these columns were added in MSSQL2008
			//stringBuilder.AppendLine("	, [has_filter]");
			//stringBuilder.AppendLine("	, [filter_definition]");
			stringBuilder.AppendLine("FROM [sys].[indexes]");

			return stringBuilder.ToString();
		}

		public IndexInputOperation(string connectionString) : base(connectionString)
		{
		}

		public IndexInputOperation(string server, string database) : base(server, database)
		{
		}

		public IndexInputOperation(SqlConnectionStringBuilder connectionStringBuilder) : base(connectionStringBuilder)
		{
		}

		protected override void PrepareCommand(IDbCommand cmd)
		{
			cmd.CommandText = SelectSql;
		}
	}
}
