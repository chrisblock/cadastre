using System;
using System.Text;
using System.Threading;

namespace SchemaSurveyor.Etl.Tables.Impl
{
	public class RemoteLoginTableDefinition : AbstractTableDefinition
	{
		private static readonly Lazy<string> LazySelectSql = new Lazy<string>(BuildSelectSql, LazyThreadSafetyMode.ExecutionAndPublication);
		private static string SelectSql { get { return LazySelectSql.Value; } }

		private static string BuildSelectSql()
		{
			var stringBuilder = new StringBuilder();

			stringBuilder.AppendLine("SELECT");
			stringBuilder.AppendLine("	  [server_id]");
			stringBuilder.AppendLine("	, [remote_name]");
			stringBuilder.AppendLine("	, [local_principal_id]");
			stringBuilder.AppendLine("	, [modify_date]");
			stringBuilder.AppendLine("FROM [sys].[remote_logins]");

			return stringBuilder.ToString();
		}

		public RemoteLoginTableDefinition() : base("RemoteLogins")
		{
			//RegisterColumn("database_survey", typeof (int));
			RegisterColumn("server_id", typeof (int));
			RegisterColumn("remote_name", typeof (string));
			RegisterColumn("local_principal_id", typeof (int?));
			RegisterColumn("modify_date", typeof (DateTime));
		}

		public override string GetSelectStatement()
		{
			return SelectSql;
		}
	}
}
