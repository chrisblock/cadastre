using System;
using System.Text;
using System.Threading;

namespace SchemaSurveyor.Etl.Tables.Impl
{
	public class IndexTableDefinition : AbstractTableDefinition
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

		public IndexTableDefinition() : base("Indexes")
		{
			//RegisterColumn("database_survey", typeof (int));
			RegisterColumn("object_id", typeof (int));
			RegisterColumn("name", typeof (string));
			RegisterColumn("index_id", typeof (int));
			RegisterColumn("type", typeof (byte));
			RegisterColumn("type_desc", typeof (string));
			RegisterColumn("is_unique", typeof (bool?));
			RegisterColumn("data_space_id", typeof (int));
			RegisterColumn("ignore_dup_key", typeof (bool?));
			RegisterColumn("is_primary_key", typeof (bool?));
			RegisterColumn("is_unique_constraint", typeof (bool?));
			RegisterColumn("fill_factor", typeof (byte));
			RegisterColumn("is_padded", typeof (bool?));
			RegisterColumn("is_disabled", typeof (bool?));
			RegisterColumn("is_hypothetical", typeof (bool?));
			RegisterColumn("allow_row_locks", typeof (bool?));
			RegisterColumn("allow_page_locks", typeof (bool?));
			// TODO: these are added in MSSQL2008
			//RegisterColumn("has_filter", typeof (bool?));
			//RegisterColumn("filter_definition", typeof (string));
		}

		public override string GetSelectStatement()
		{
			return SelectSql;
		}
	}
}
