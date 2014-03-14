using System;
using System.Text;
using System.Threading;

namespace SchemaSurveyor.Etl.Tables.Impl
{
	public class TableTableDefinition : AbstractTableDefinition
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

		public TableTableDefinition() : base("Tables")
		{
			//RegisterColumn("database_survey", typeof (int));
			RegisterColumn("name", typeof (string));
			RegisterColumn("object_id", typeof (int));
			RegisterColumn("principal_id", typeof (int?));
			RegisterColumn("schema_id", typeof (int));
			RegisterColumn("parent_object_id", typeof (int));
			RegisterColumn("type", typeof (string));
			RegisterColumn("type_desc", typeof (string));
			RegisterColumn("create_date", typeof (DateTime));
			RegisterColumn("modify_date", typeof (DateTime));
			RegisterColumn("is_ms_shipped", typeof (bool));
			RegisterColumn("is_published", typeof (bool));
			RegisterColumn("is_schema_published", typeof (bool));
			RegisterColumn("lob_data_space_id", typeof (int?));
			RegisterColumn("filestream_data_space_id", typeof (int?));
			RegisterColumn("max_column_id_used", typeof (int));
			RegisterColumn("lock_on_bulk_load", typeof (bool));
			RegisterColumn("uses_ansi_nulls", typeof (bool?));
			RegisterColumn("is_replicated", typeof (bool?));
			RegisterColumn("has_replication_filter", typeof (bool?));
			RegisterColumn("is_merge_published", typeof (bool?));
			RegisterColumn("is_sync_tran_subscribed", typeof (bool?));
			RegisterColumn("has_unchecked_assembly_data", typeof (bool));
			RegisterColumn("text_in_row_limit", typeof (int?));
			RegisterColumn("large_value_types_out_of_row", typeof (bool?));
			// TODO: these columns are added in MSSQL2008
			//RegisterColumn("is_tracked_by_cdc", typeof (bool?));
			//RegisterColumn("lock_escalation", typeof (byte?));
			//RegisterColumn("lock_escalation_desc", typeof (string));
		}

		public override string GetSelectStatement()
		{
			return SelectSql;
		}
	}
}
