using System;
using System.Text;
using System.Threading;

namespace SchemaSurveyor.Etl.Tables.Impl
{
	public class ForeignKeyTableDefinition : AbstractTableDefinition
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
			stringBuilder.AppendLine("	, [referenced_object_id]");
			stringBuilder.AppendLine("	, [key_index_id]");
			stringBuilder.AppendLine("	, [is_disabled]");
			stringBuilder.AppendLine("	, [is_not_for_replication]");
			stringBuilder.AppendLine("	, [is_not_trusted]");
			stringBuilder.AppendLine("	, [delete_referential_action]");
			stringBuilder.AppendLine("	, [delete_referential_action_desc]");
			stringBuilder.AppendLine("	, [update_referential_action]");
			stringBuilder.AppendLine("	, [update_referential_action_desc]");
			stringBuilder.AppendLine("	, [is_system_named]");
			stringBuilder.AppendLine("FROM [sys].[foreign_keys]");

			return stringBuilder.ToString();
		}

		public ForeignKeyTableDefinition() : base("ForeignKeys")
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
			RegisterColumn("referenced_object_id", typeof (int?));
			RegisterColumn("key_index_id", typeof (int?));
			RegisterColumn("is_disabled", typeof (bool));
			RegisterColumn("is_not_for_replication", typeof (bool));
			RegisterColumn("is_not_trusted", typeof (bool));
			RegisterColumn("delete_referential_action", typeof (byte?));
			RegisterColumn("delete_referential_action_desc", typeof (string));
			RegisterColumn("update_referential_action", typeof (byte?));
			RegisterColumn("update_referential_action_desc", typeof (string));
			RegisterColumn("is_system_named", typeof (bool));
		}

		public override string GetSelectStatement()
		{
			return SelectSql;
		}
	}
}
