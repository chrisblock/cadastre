using System;
using System.Text;
using System.Threading;

namespace SchemaSurveyor.Etl.Tables.Impl
{
	public class ViewTableDefinition : AbstractTableDefinition
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
			stringBuilder.AppendLine("	, [is_replicated]");
			stringBuilder.AppendLine("	, [has_replication_filter]");
			stringBuilder.AppendLine("	, [has_opaque_metadata]");
			stringBuilder.AppendLine("	, [has_unchecked_assembly_data]");
			stringBuilder.AppendLine("	, [with_check_option]");
			stringBuilder.AppendLine("	, [is_date_correlation_view]");
			// TODO: this column is added in MSSQL2008
			//stringBuilder.AppendLine("	, [is_tracked_by_cdc]");
			stringBuilder.AppendLine("FROM [sys].[views]");

			return stringBuilder.ToString();
		}

		public ViewTableDefinition() : base("Views")
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
			RegisterColumn("is_replicated", typeof (bool?));
			RegisterColumn("has_replication_filter", typeof (bool?));
			RegisterColumn("has_opaque_metadata", typeof (bool));
			RegisterColumn("has_unchecked_assembly_data", typeof (bool));
			RegisterColumn("with_check_option", typeof (bool));
			RegisterColumn("is_date_correlation_view", typeof (bool));
			// TODO: this columns is added by MSSQL2008
			//RegisterColumn("is_tracked_by_cdc", typeof (bool?));
		}

		public override string GetSelectStatement()
		{
			return SelectSql;
		}
	}
}
