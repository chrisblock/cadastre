using System;
using System.Text;
using System.Threading;

namespace SchemaSurveyor.Etl.Tables.Impl
{
	public class DatabaseRoleMemberTableDefinition : AbstractTableDefinition
	{
		private static readonly Lazy<string> LazySelectSql = new Lazy<string>(BuildSelectSql, LazyThreadSafetyMode.ExecutionAndPublication);
		private static string SelectSql { get { return LazySelectSql.Value; } }

		private static string BuildSelectSql()
		{
			var stringBuilder = new StringBuilder();

			stringBuilder.AppendLine("SELECT");
			stringBuilder.AppendLine("	  [role_principal_id]");
			stringBuilder.AppendLine("	, [member_principal_id]");
			stringBuilder.AppendLine("FROM [sys].[database_role_members]");

			return stringBuilder.ToString();
		}

		public DatabaseRoleMemberTableDefinition() : base("DatabaseRoleMembers")
		{
			RegisterColumn("database_survey", typeof (int));
			RegisterColumn("role_principal_id", typeof (int));
			RegisterColumn("member_principal_id", typeof (int));
		}

		public override string GetSelectStatement()
		{
			return SelectSql;
		}
	}
}
