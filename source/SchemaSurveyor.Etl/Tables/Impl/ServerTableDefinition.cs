using System;
using System.Text;
using System.Threading;

namespace SchemaSurveyor.Etl.Tables.Impl
{
	public class ServerTableDefinition : AbstractTableDefinition
	{
		private static readonly Lazy<string> LazySelectSql = new Lazy<string>(BuildSelectSql, LazyThreadSafetyMode.ExecutionAndPublication);
		private static string SelectSql { get { return LazySelectSql.Value; } }

		private static string BuildSelectSql()
		{
			var stringBuilder = new StringBuilder();

			stringBuilder.AppendLine("SELECT");
			stringBuilder.AppendLine("	  [server_id]");
			stringBuilder.AppendLine("	, [name]");
			stringBuilder.AppendLine("	, [product]");
			stringBuilder.AppendLine("	, [provider]");
			stringBuilder.AppendLine("	, [data_source]");
			stringBuilder.AppendLine("	, [location]");
			stringBuilder.AppendLine("	, [provider_string]");
			stringBuilder.AppendLine("	, [catalog]");
			stringBuilder.AppendLine("	, [connect_timeout]");
			stringBuilder.AppendLine("	, [query_timeout]");
			stringBuilder.AppendLine("	, [is_linked]");
			stringBuilder.AppendLine("	, [is_remote_login_enabled]");
			stringBuilder.AppendLine("	, [is_rpc_out_enabled]");
			stringBuilder.AppendLine("	, [is_data_access_enabled]");
			stringBuilder.AppendLine("	, [is_collation_compatible]");
			stringBuilder.AppendLine("	, [uses_remote_collation]");
			stringBuilder.AppendLine("	, [collation_name]");
			stringBuilder.AppendLine("	, [lazy_schema_validation]");
			stringBuilder.AppendLine("	, [is_system]");
			stringBuilder.AppendLine("	, [is_publisher]");
			stringBuilder.AppendLine("	, [is_subscriber]");
			stringBuilder.AppendLine("	, [is_distributor]");
			stringBuilder.AppendLine("	, [is_nonsql_subscriber]");
			stringBuilder.AppendLine("	, [is_remote_proc_transaction_promotion_enabled]");
			stringBuilder.AppendLine("	, [modify_date]");
			stringBuilder.AppendLine("FROM [sys].[servers]");

			return stringBuilder.ToString();
		}

		public ServerTableDefinition() : base("Servers")
		{
			//RegisterColumn("database_survey", typeof (int));
			RegisterColumn("server_id", typeof (int));
			RegisterColumn("name", typeof (string));
			RegisterColumn("product", typeof (string));
			RegisterColumn("provider", typeof (string));
			RegisterColumn("data_source", typeof (string));
			RegisterColumn("location", typeof (string));
			RegisterColumn("provider_string", typeof (string));
			RegisterColumn("catalog", typeof (string));
			RegisterColumn("connect_timeout", typeof (int?));
			RegisterColumn("query_timeout", typeof (int?));
			RegisterColumn("is_linked", typeof (bool));
			RegisterColumn("is_remote_login_enabled", typeof (bool));
			RegisterColumn("is_rpc_out_enabled", typeof (bool));
			RegisterColumn("is_data_access_enabled", typeof (bool));
			RegisterColumn("is_collation_compatible", typeof (bool));
			RegisterColumn("uses_remote_collation", typeof (bool));
			RegisterColumn("collation_name", typeof (string));
			RegisterColumn("lazy_schema_validation", typeof (bool));
			RegisterColumn("is_system", typeof (bool));
			RegisterColumn("is_publisher", typeof (bool));
			RegisterColumn("is_subscriber", typeof (bool?));
			RegisterColumn("is_distributor", typeof (bool?));
			RegisterColumn("is_nonsql_subscriber", typeof (bool?));
			RegisterColumn("is_remote_proc_transaction_promotion_enabled", typeof (bool?));
			RegisterColumn("modify_date", typeof (DateTime));
		}

		public override string GetSelectStatement()
		{
			return SelectSql;
		}
	}
}
