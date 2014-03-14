using System;
using System.Text;
using System.Threading;

namespace SchemaSurveyor.Etl.Tables.Impl
{
	public class ColumnTableDefinition : AbstractTableDefinition
	{
		private static readonly Lazy<string> LazySelectSql = new Lazy<string>(BuildSelectSql, LazyThreadSafetyMode.ExecutionAndPublication);
		private static string SelectSql { get { return LazySelectSql.Value; } }

		private static string BuildSelectSql()
		{
			var stringBuilder = new StringBuilder();

			stringBuilder.AppendLine("SELECT");
			stringBuilder.AppendLine("	  [object_id]");
			stringBuilder.AppendLine("	, [name]");
			stringBuilder.AppendLine("	, [column_id]");
			stringBuilder.AppendLine("	, [system_type_id]");
			stringBuilder.AppendLine("	, [user_type_id]");
			stringBuilder.AppendLine("	, [max_length]");
			stringBuilder.AppendLine("	, [precision]");
			stringBuilder.AppendLine("	, [scale]");
			stringBuilder.AppendLine("	, [collation_name]");
			stringBuilder.AppendLine("	, [is_nullable]");
			stringBuilder.AppendLine("	, [is_ansi_padded]");
			stringBuilder.AppendLine("	, [is_rowguidcol]");
			stringBuilder.AppendLine("	, [is_identity]");
			stringBuilder.AppendLine("	, [is_computed]");
			stringBuilder.AppendLine("	, [is_filestream]");
			stringBuilder.AppendLine("	, [is_replicated]");
			stringBuilder.AppendLine("	, [is_non_sql_subscribed]");
			stringBuilder.AppendLine("	, [is_merge_published]");
			stringBuilder.AppendLine("	, [is_dts_replicated]");
			stringBuilder.AppendLine("	, [is_xml_document]");
			stringBuilder.AppendLine("	, [xml_collection_id]");
			stringBuilder.AppendLine("	, [default_object_id]");
			stringBuilder.AppendLine("	, [rule_object_id]");
			// TODO: these columns appear in MSSQL2008
			//stringBuilder.AppendLine("	, [is_sparse]");
			//stringBuilder.AppendLine("	, [is_column_set]");
			stringBuilder.AppendLine("FROM [sys].[columns]");

			return stringBuilder.ToString();
		}

		public ColumnTableDefinition() : base("Columns")
		{
			//RegisterColumn("database_survey", typeof (int));
			RegisterColumn("object_id", typeof (int));
			RegisterColumn("name", typeof (string));
			RegisterColumn("column_id", typeof (int));
			RegisterColumn("system_type_id", typeof (byte));
			RegisterColumn("user_type_id", typeof (int));
			RegisterColumn("max_length", typeof (short));
			RegisterColumn("precision", typeof (byte));
			RegisterColumn("scale", typeof (byte));
			RegisterColumn("collation_name", typeof (string));
			RegisterColumn("is_nullable", typeof (bool?));
			RegisterColumn("is_ansi_padded", typeof (bool));
			RegisterColumn("is_rowguidcol", typeof (bool));
			RegisterColumn("is_identity", typeof (bool));
			RegisterColumn("is_computed", typeof (bool));
			RegisterColumn("is_filestream", typeof (bool));
			RegisterColumn("is_replicated", typeof (bool?));
			RegisterColumn("is_non_sql_subscribed", typeof (bool?));
			RegisterColumn("is_merge_published", typeof (bool?));
			RegisterColumn("is_dts_replicated", typeof (bool?));
			RegisterColumn("is_xml_document", typeof (bool));
			RegisterColumn("xml_collection_id", typeof (int));
			RegisterColumn("default_object_id", typeof (int));
			RegisterColumn("rule_object_id", typeof (int));
			// TODO: these columns appear in MSSQL2008
			//RegisterColumn("is_sparse", typeof (bool?);
			//RegisterColumn("is_column_set", typeof (bool?);
		}

		public override string GetSelectStatement()
		{
			return SelectSql;
		}
	}
}
