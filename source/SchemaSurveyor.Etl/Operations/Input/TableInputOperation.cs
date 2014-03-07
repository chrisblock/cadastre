using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading;

namespace SchemaSurveyor.Etl.Operations.Input
{
	public class TableInputOperation : SimpleInputCommandOperation
	{
		private static readonly Lazy<string> LazySelectSql = new Lazy<string>(BuildSelectSql, LazyThreadSafetyMode.ExecutionAndPublication);
		private static string SelectSql { get { return LazySelectSql.Value; } }

		private static string BuildSelectSql()
		{
			var stringBuilder = new StringBuilder();

			stringBuilder.AppendLine("SELECT");
			stringBuilder.AppendLine("	  [name]");
			stringBuilder.AppendLine("	, [object_id]");
			stringBuilder.AppendLine("	, [principal_id]");
			stringBuilder.AppendLine("	, [schema_id]");
			stringBuilder.AppendLine("	, [parent_object_id]");
			stringBuilder.AppendLine("	, [type]");
			stringBuilder.AppendLine("	, [type_desc]");
			stringBuilder.AppendLine("	, [create_date]");
			stringBuilder.AppendLine("	, [modify_date]");
			stringBuilder.AppendLine("	, [is_ms_shipped]");
			stringBuilder.AppendLine("	, [is_published]");
			stringBuilder.AppendLine("	, [is_schema_published]");
			stringBuilder.AppendLine("	, [lob_data_space_id]");
			stringBuilder.AppendLine("	, [filestream_data_space_id]");
			stringBuilder.AppendLine("	, [max_column_id_used]");
			stringBuilder.AppendLine("	, [lock_on_bulk_load]");
			stringBuilder.AppendLine("	, [uses_ansi_nulls]");
			stringBuilder.AppendLine("	, [is_replicated]");
			stringBuilder.AppendLine("	, [has_replication_filter]");
			stringBuilder.AppendLine("	, [is_merge_published]");
			stringBuilder.AppendLine("	, [is_sync_tran_subscribed]");
			stringBuilder.AppendLine("	, [has_unchecked_assembly_data]");
			stringBuilder.AppendLine("	, [text_in_row_limit]");
			stringBuilder.AppendLine("	, [large_value_types_out_of_row]");
			// TODO: these columns appear in MSSQL2008
			//stringBuilder.AppendLine("	, [is_tracked_by_cdc]");
			//stringBuilder.AppendLine("	, [lock_escalation]");
			//stringBuilder.AppendLine("	, [lock_escalation_desc]");
			stringBuilder.AppendLine("FROM [sys].[tables]");

			return stringBuilder.ToString();
		}

		public TableInputOperation(string connectionString) : base(connectionString)
		{
		}

		public TableInputOperation(string server, string database) : base(server, database)
		{
		}

		public TableInputOperation(SqlConnectionStringBuilder connectionStringBuilder) : base(connectionStringBuilder)
		{
		}

		protected override void PrepareCommand(IDbCommand cmd)
		{
			cmd.CommandText = SelectSql;
		}
	}
}
