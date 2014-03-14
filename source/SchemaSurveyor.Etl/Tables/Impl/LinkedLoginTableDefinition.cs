using System;
using System.Text;
using System.Threading;

namespace SchemaSurveyor.Etl.Tables.Impl
{
	public class LinkedLoginTableDefinition : AbstractTableDefinition
	{
		private static readonly Lazy<string> LazySelectSql = new Lazy<string>(BuildSelectSql, LazyThreadSafetyMode.ExecutionAndPublication);
		private static string SelectSql { get { return LazySelectSql.Value; } }

		private static string BuildSelectSql()
		{
			var stringBuilder = new StringBuilder();

			stringBuilder.AppendLine("SELECT");
			stringBuilder.AppendLine("	  [server_id]");
			stringBuilder.AppendLine("	, [local_principal_id]");
			stringBuilder.AppendLine("	, [uses_self_credential]");
			stringBuilder.AppendLine("	, [remote_name]");
			stringBuilder.AppendLine("	, [modify_date]");
			stringBuilder.AppendLine("FROM [sys].[linked_logins]");

			return stringBuilder.ToString();
		}

		public LinkedLoginTableDefinition() : base("LinkedLogins")
		{
			//RegisterColumn("database_survey", typeof (int));
			RegisterColumn("server_id", typeof (int));
			RegisterColumn("local_principal_id", typeof (int?));
			RegisterColumn("uses_self_credential", typeof (bool));
			RegisterColumn("remote_name", typeof (string));
			RegisterColumn("modify_date", typeof (DateTime));
		}

		public override string GetSelectStatement()
		{
			return SelectSql;
		}
	}
}
