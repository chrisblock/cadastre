using System;
using System.Text;
using System.Threading;

namespace SchemaSurveyor.Etl.Tables.Impl
{
	public class SqlLoginTableDefinition : AbstractTableDefinition
	{
		private static readonly Lazy<string> LazySelectSql = new Lazy<string>(BuildSelectSql, LazyThreadSafetyMode.ExecutionAndPublication);
		private static string SelectSql { get { return LazySelectSql.Value; } }

		private static string BuildSelectSql()
		{
			var stringBuilder = new StringBuilder();

			stringBuilder.AppendLine("SELECT");
			stringBuilder.AppendLine("	  [name]");
			stringBuilder.AppendLine("	, [principal_id]");
			stringBuilder.AppendLine("	, [sid]");
			stringBuilder.AppendLine("	, [type]");
			stringBuilder.AppendLine("	, [type_desc]");
			stringBuilder.AppendLine("	, [is_disabled]");
			stringBuilder.AppendLine("	, [create_date]");
			stringBuilder.AppendLine("	, [modify_date]");
			stringBuilder.AppendLine("	, [default_database_name]");
			stringBuilder.AppendLine("	, [default_language_name]");
			stringBuilder.AppendLine("	, [credential_id]");
			stringBuilder.AppendLine("	, [is_policy_checked]");
			stringBuilder.AppendLine("	, [is_expiration_checked]");
			stringBuilder.AppendLine("	, [password_hash]");
			stringBuilder.AppendLine("FROM [sys].[sql_logins]");

			return stringBuilder.ToString();
		}

		public SqlLoginTableDefinition() : base("SqlLogins")
		{
			//RegisterColumn("database_survey", typeof (int));
			RegisterColumn("name", typeof (string));
			RegisterColumn("principal_id", typeof (int));
			RegisterColumn("sid", typeof (byte[]));
			RegisterColumn("type", typeof (char));
			RegisterColumn("type_desc", typeof (string));
			RegisterColumn("is_disabled", typeof (bool?));
			RegisterColumn("create_date", typeof (DateTime));
			RegisterColumn("modify_date", typeof (DateTime));
			RegisterColumn("default_database_name", typeof (string));
			RegisterColumn("default_language_name", typeof (string));
			RegisterColumn("credential_id", typeof (int?));
			RegisterColumn("is_policy_checked", typeof (bool?));
			RegisterColumn("is_expiration_checked", typeof (bool?));
			RegisterColumn("password_hash", typeof (byte[]));
		}

		public override string GetSelectStatement()
		{
			return SelectSql;
		}
	}
}
