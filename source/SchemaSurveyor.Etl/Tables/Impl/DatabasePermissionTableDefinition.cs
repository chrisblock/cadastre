using System;
using System.Text;
using System.Threading;

namespace SchemaSurveyor.Etl.Tables.Impl
{
	public class DatabasePermissionTableDefinition : AbstractTableDefinition
	{
		private static readonly Lazy<string> LazySelectSql = new Lazy<string>(BuildSelectSql, LazyThreadSafetyMode.ExecutionAndPublication);
		private static string SelectSql { get { return LazySelectSql.Value; } }

		private static string BuildSelectSql()
		{
			var stringBuilder = new StringBuilder();

			stringBuilder.AppendLine("SELECT");
			stringBuilder.AppendLine("	  [class]");
			stringBuilder.AppendLine("	, [class_desc]");
			stringBuilder.AppendLine("	, [major_id]");
			stringBuilder.AppendLine("	, [minor_id]");
			stringBuilder.AppendLine("	, [grantee_principal_id]");
			stringBuilder.AppendLine("	, [grantor_principal_id]");
			stringBuilder.AppendLine("	, [type]");
			stringBuilder.AppendLine("	, [permission_name]");
			stringBuilder.AppendLine("	, [state]");
			stringBuilder.AppendLine("	, [state_desc]");
			stringBuilder.AppendLine("FROM [sys].[database_permissions]");

			return stringBuilder.ToString();
		}

		public DatabasePermissionTableDefinition() : base("DatabasePermissions")
		{
			// RegisterColumn("database_survey", typeof (int));
			RegisterColumn("class", typeof (byte));
			RegisterColumn("class_desc", typeof (string));
			RegisterColumn("major_id", typeof (int));
			RegisterColumn("minor_id", typeof (int));
			RegisterColumn("grantee_principal_id", typeof (int));
			RegisterColumn("grantor_principal_id", typeof (int));
			RegisterColumn("type", typeof (string));
			RegisterColumn("permission_name", typeof (string));
			RegisterColumn("state", typeof (char));
			RegisterColumn("state_desc", typeof (string));
		}

		public override string GetSelectStatement()
		{
			return SelectSql;
		}
	}
}
