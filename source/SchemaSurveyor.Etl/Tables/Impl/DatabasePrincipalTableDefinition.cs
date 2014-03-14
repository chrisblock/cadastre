using System;
using System.Text;
using System.Threading;

namespace SchemaSurveyor.Etl.Tables.Impl
{
	public class DatabasePrincipalTableDefinition : AbstractTableDefinition
	{
		private static readonly Lazy<string> LazySelectSql = new Lazy<string>(BuildSelectSql, LazyThreadSafetyMode.ExecutionAndPublication);
		private static string SelectSql { get { return LazySelectSql.Value; } }

		private static string BuildSelectSql()
		{
			var stringBuilder = new StringBuilder();

			stringBuilder.AppendLine("SELECT");
			stringBuilder.AppendLine("	  [name]");
			stringBuilder.AppendLine("	, [principal_id]");
			stringBuilder.AppendLine("	, [type]");
			stringBuilder.AppendLine("	, [type_desc]");
			stringBuilder.AppendLine("	, [default_schema_name]");
			stringBuilder.AppendLine("	, [create_date]");
			stringBuilder.AppendLine("	, [modify_date]");
			stringBuilder.AppendLine("	, [owning_principal_id]");
			stringBuilder.AppendLine("	, [sid]");
			stringBuilder.AppendLine("	, [is_fixed_role]");
			stringBuilder.AppendLine("FROM [sys].[database_principals]");

			return stringBuilder.ToString();
		}

		public DatabasePrincipalTableDefinition() : base("DatabasePrincipals")
		{
			// RegisterColumn("database_survey", typeof (int));
			RegisterColumn("name", typeof (string));
			RegisterColumn("principal_id", typeof (int));
			RegisterColumn("type", typeof (char));
			RegisterColumn("type_desc", typeof (string));
			RegisterColumn("default_schema_name", typeof (string));
			RegisterColumn("create_date", typeof (DateTime));
			RegisterColumn("modify_date", typeof (DateTime));
			RegisterColumn("owning_principal_id", typeof (int?));
			RegisterColumn("sid", typeof (byte[]));
			RegisterColumn("is_fixed_role", typeof (bool));
		}

		public override string GetSelectStatement()
		{
			return SelectSql;
		}
	}
}
