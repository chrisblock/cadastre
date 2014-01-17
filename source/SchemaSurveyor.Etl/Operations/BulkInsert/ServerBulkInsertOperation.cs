using System;
using System.Data.SqlClient;

namespace SchemaSurveyor.Etl.Operations.BulkInsert
{
	public class ServerBulkInsertOperation : SimpleBulkInsertOperation
	{
		private const string TableName = "Servers";

		public ServerBulkInsertOperation(string server, string database) : base(server, database, TableName)
		{
		}

		public ServerBulkInsertOperation(string server, string database, int timeout) : base(server, database, TableName, timeout)
		{
		}

		public ServerBulkInsertOperation(string connectionString) : base(connectionString, TableName)
		{
		}

		public ServerBulkInsertOperation(string connectionString, int timeout) : base(connectionString, TableName, timeout)
		{
		}

		public ServerBulkInsertOperation(SqlConnectionStringBuilder connectionStringBuilder) : base(connectionStringBuilder, TableName)
		{
		}

		public ServerBulkInsertOperation(SqlConnectionStringBuilder connectionStringBuilder, int timeout) : base(connectionStringBuilder, TableName, timeout)
		{
		}

		protected override void PrepareSchema()
		{
			Schema["database_survey"] = typeof (int);
			Schema["server_id"] = typeof (int);
			Schema["name"] = typeof (string);
			Schema["product"] = typeof (string);
			Schema["provider"] = typeof (string);
			Schema["data_source"] = typeof (string);
			Schema["location"] = typeof (string);
			Schema["provider_string"] = typeof (string);
			Schema["catalog"] = typeof (string);
			Schema["connect_timeout"] = typeof (int?);
			Schema["query_timeout"] = typeof (int?);
			Schema["is_linked"] = typeof (bool);
			Schema["is_remote_login_enabled"] = typeof (bool);
			Schema["is_rpc_out_enabled"] = typeof (bool);
			Schema["is_data_access_enabled"] = typeof (bool);
			Schema["is_collation_compatible"] = typeof (bool);
			Schema["uses_remote_collation"] = typeof (bool);
			Schema["collation_name"] = typeof (string);
			Schema["lazy_schema_validation"] = typeof (bool);
			Schema["is_system"] = typeof (bool);
			Schema["is_publisher"] = typeof (bool);
			Schema["is_subscriber"] = typeof (bool?);
			Schema["is_distributor"] = typeof (bool?);
			Schema["is_nonsql_subscriber"] = typeof (bool?);
			Schema["is_remote_proc_transaction_promotion_enabled"] = typeof (bool?);
			Schema["modify_date"] = typeof (DateTime);
		}
	}
}
