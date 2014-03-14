using System;
using System.Text;
using System.Threading;

namespace SchemaSurveyor.Etl.Tables.Impl
{
	public class ParameterTableDefinition : AbstractTableDefinition
	{
		private static readonly Lazy<string> LazySelectSql = new Lazy<string>(BuildSelectSql, LazyThreadSafetyMode.ExecutionAndPublication);
		private static string SelectSql { get { return LazySelectSql.Value; } }

		private static string BuildSelectSql()
		{
			var stringBuilder = new StringBuilder();

			stringBuilder.AppendLine("SELECT");
			stringBuilder.AppendLine("	  [object_id]");
			stringBuilder.AppendLine("	, [name]");
			stringBuilder.AppendLine("	, [parameter_id]");
			stringBuilder.AppendLine("	, [system_type_id]");
			stringBuilder.AppendLine("	, [user_type_id]");
			stringBuilder.AppendLine("	, [max_length]");
			stringBuilder.AppendLine("	, [precision]");
			stringBuilder.AppendLine("	, [scale]");
			stringBuilder.AppendLine("	, [is_output]");
			stringBuilder.AppendLine("	, [is_cursor_ref]");
			stringBuilder.AppendLine("	, [has_default_value]");
			stringBuilder.AppendLine("	, [is_xml_document]");
			stringBuilder.AppendLine("	, [default_value]");
			stringBuilder.AppendLine("	, [xml_collection_id]");
			// TODO: this column is added in MSSQL2008
			//stringBuilder.AppendLine("	, [is_readonly]");
			stringBuilder.AppendLine("FROM [sys].[parameters]");

			return stringBuilder.ToString();
		}

		public ParameterTableDefinition() : base("Parameters")
		{
			//RegisterColumn("database_survey", typeof (int));
			RegisterColumn("object_id", typeof (int));
			RegisterColumn("name", typeof (string));
			RegisterColumn("parameter_id", typeof (int));
			RegisterColumn("system_type_id", typeof (byte));
			RegisterColumn("user_type_id", typeof (int));
			RegisterColumn("max_length", typeof (short));
			RegisterColumn("precision", typeof (byte));
			RegisterColumn("scale", typeof (byte));
			RegisterColumn("is_output", typeof (bool));
			RegisterColumn("is_cursor_ref", typeof (bool));
			RegisterColumn("has_default_value", typeof (bool));
			RegisterColumn("is_xml_document", typeof (bool));
			// TODO: default_value is a sql_variant...i have no idea if this is gonna work
			RegisterColumn("default_value", typeof (object));
			RegisterColumn("xml_collection_id", typeof (int));
			// TODO: this columns is added in MSSQL2008
			//RegisterColumn("is_readonly", typeof (bool));
		}

		public override string GetSelectStatement()
		{
			return SelectSql;
		}
	}
}
