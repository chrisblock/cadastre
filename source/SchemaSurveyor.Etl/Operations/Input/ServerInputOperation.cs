using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading;

namespace SchemaSurveyor.Etl.Operations.Input
{
	public class ServerInputOperation : SimpleInputCommandOperation
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

		public ServerInputOperation(string connectionString) : base(connectionString)
		{
		}

		public ServerInputOperation(string server, string database) : base(server, database)
		{
		}

		public ServerInputOperation(SqlConnectionStringBuilder connectionStringBuilder) : base(connectionStringBuilder)
		{
		}

		protected override void PrepareCommand(IDbCommand cmd)
		{
			cmd.CommandText = SelectSql;
		}
	}
}
