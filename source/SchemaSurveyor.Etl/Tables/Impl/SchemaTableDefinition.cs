using System;
using System.Text;
using System.Threading;

namespace SchemaSurveyor.Etl.Tables.Impl
{
	public class SchemaTableDefinition : AbstractTableDefinition
	{
		private static readonly Lazy<string> LazySelectSql = new Lazy<string>(BuildSelectSql, LazyThreadSafetyMode.ExecutionAndPublication);
		private static string SelectSql { get { return LazySelectSql.Value; } }

		private static string BuildSelectSql()
		{
			var stringBuilder = new StringBuilder();

			stringBuilder.AppendLine("SELECT");
			stringBuilder.AppendLine("	  [name]");
			stringBuilder.AppendLine("	, [schema_id]");
			stringBuilder.AppendLine("	, [principal_id]");
			stringBuilder.AppendLine("FROM [sys].[schemas]");

			return stringBuilder.ToString();
		}

		public SchemaTableDefinition() : base("Schemas")
		{
			//RegisterColumn("database_survey", typeof (int));
			RegisterColumn("name", typeof (string));
			RegisterColumn("schema_id", typeof (int));
			RegisterColumn("principal_id", typeof (int?));
		}

		public override string GetSelectStatement()
		{
			return SelectSql;
		}
	}
}
