using System;
using System.Text;
using System.Threading;

namespace SchemaSurveyor.Etl.Tables.Impl
{
	public class SqlModuleDefinitionTableDefinition : AbstractTableDefinition
	{
		private static readonly Lazy<string> LazySelectSql = new Lazy<string>(BuildSelectSql, LazyThreadSafetyMode.ExecutionAndPublication);
		private static string SelectSql { get { return LazySelectSql.Value; } }

		private static string BuildSelectSql()
		{
			var stringBuilder = new StringBuilder();

			stringBuilder.AppendLine("SELECT");
			stringBuilder.AppendLine("	  [object_id]");
			stringBuilder.AppendLine("	, [definition]");
			stringBuilder.AppendLine("FROM [sys].[sql_modules]");

			return stringBuilder.ToString();
		}

		public SqlModuleDefinitionTableDefinition() : base("SqlModuleDefinitions")
		{
			//RegisterColumn("database_survey", typeof (int));
			RegisterColumn("object_id", typeof (int));
			RegisterColumn("definition", typeof (string));
		}

		public override string GetSelectStatement()
		{
			return SelectSql;
		}
	}
}
