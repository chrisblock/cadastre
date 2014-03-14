using System;
using System.Text;
using System.Threading;

namespace SchemaSurveyor.Etl.Tables.Impl
{
	public class IndexColumnTableDefinition : AbstractTableDefinition
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

		public IndexColumnTableDefinition() : base("IndexColumns")
		{
			//RegisterColumn("database_survey", typeof (int));
			RegisterColumn("object_id", typeof (int));
			RegisterColumn("index_id", typeof (int));
			RegisterColumn("index_column_id", typeof (int));
			RegisterColumn("column_id", typeof (int));
			RegisterColumn("key_ordinal", typeof (byte));
			RegisterColumn("partition_ordinal", typeof (byte));
			RegisterColumn("is_descending_key", typeof (bool?));
			RegisterColumn("is_included_column", typeof (bool?));
		}

		public override string GetSelectStatement()
		{
			return SelectSql;
		}
	}
}
