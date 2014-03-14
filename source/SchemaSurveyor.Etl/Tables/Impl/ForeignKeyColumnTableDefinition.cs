using System;
using System.Text;
using System.Threading;

namespace SchemaSurveyor.Etl.Tables.Impl
{
	public class ForeignKeyColumnTableDefinition : AbstractTableDefinition
	{
		private static readonly Lazy<string> LazySelectSql = new Lazy<string>(BuildSelectSql, LazyThreadSafetyMode.ExecutionAndPublication);
		private static string SelectSql { get { return LazySelectSql.Value; } }

		private static string BuildSelectSql()
		{
			var stringBuilder = new StringBuilder();

			stringBuilder.AppendLine("SELECT");
			stringBuilder.AppendLine("	  [constraint_object_id]");
			stringBuilder.AppendLine("	, [constraint_column_id]");
			stringBuilder.AppendLine("	, [parent_object_id]");
			stringBuilder.AppendLine("	, [parent_column_id]");
			stringBuilder.AppendLine("	, [referenced_object_id]");
			stringBuilder.AppendLine("	, [referenced_column_id]");
			stringBuilder.AppendLine("FROM [sys].[foreign_key_columns]");

			return stringBuilder.ToString();
		}

		public ForeignKeyColumnTableDefinition() : base("ForeignKeyColumns")
		{
			//RegisterColumn("database_survey", typeof (int));
			RegisterColumn("constraint_object_id", typeof (int));
			RegisterColumn("constraint_column_id", typeof (int));
			RegisterColumn("parent_object_id", typeof (int));
			RegisterColumn("parent_column_id", typeof (int));
			RegisterColumn("referenced_object_id", typeof (int));
			RegisterColumn("referenced_column_id", typeof (int));
		}

		public override string GetSelectStatement()
		{
			return SelectSql;
		}
	}
}
