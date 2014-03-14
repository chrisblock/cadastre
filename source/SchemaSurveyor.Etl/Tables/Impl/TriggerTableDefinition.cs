using System;
using System.Text;
using System.Threading;

namespace SchemaSurveyor.Etl.Tables.Impl
{
	public class TriggerTableDefinition : AbstractTableDefinition
	{
		private static readonly Lazy<string> LazySelectSql = new Lazy<string>(BuildSelectSql, LazyThreadSafetyMode.ExecutionAndPublication);
		private static string SelectSql { get { return LazySelectSql.Value; } }

		private static string BuildSelectSql()
		{
			var stringBuilder = new StringBuilder();

			stringBuilder.AppendLine("SELECT");
			stringBuilder.AppendLine("	  [name]");
			stringBuilder.AppendLine("	, [object_id]");
			stringBuilder.AppendLine("	, [parent_class]");
			stringBuilder.AppendLine("	, [parent_class_desc]");
			stringBuilder.AppendLine("	, [parent_id]");
			stringBuilder.AppendLine("	, [type]");
			stringBuilder.AppendLine("	, [type_desc]");
			stringBuilder.AppendLine("	, [create_date]");
			stringBuilder.AppendLine("	, [modify_date]");
			stringBuilder.AppendLine("	, [is_ms_shipped]");
			stringBuilder.AppendLine("	, [is_disabled]");
			stringBuilder.AppendLine("	, [is_not_for_replication]");
			stringBuilder.AppendLine("	, [is_instead_of_trigger]");
			stringBuilder.AppendLine("FROM [sys].[triggers]");

			return stringBuilder.ToString();
		}

		public TriggerTableDefinition() : base("Triggers")
		{
			//RegisterColumn("database_survey", typeof (int));
			RegisterColumn("name", typeof (string));
			RegisterColumn("object_id", typeof (int));
			RegisterColumn("parent_class", typeof (byte));
			RegisterColumn("parent_class_desc", typeof (string));
			RegisterColumn("parent_id", typeof (int));
			RegisterColumn("type", typeof (string));
			RegisterColumn("type_desc", typeof (string));
			RegisterColumn("create_date", typeof (DateTime));
			RegisterColumn("modify_date", typeof (DateTime));
			RegisterColumn("is_ms_shipped", typeof (bool));
			RegisterColumn("is_disabled", typeof (bool));
			RegisterColumn("is_not_for_replication", typeof (bool));
			RegisterColumn("is_instead_of_trigger", typeof (bool));
		}

		public override string GetSelectStatement()
		{
			return SelectSql;
		}
	}
}
