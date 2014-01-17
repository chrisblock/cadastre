using System;
using System.Data.SqlClient;

namespace SchemaSurveyor.Etl.Operations.BulkInsert
{
	public class RemoteLoginBulkInsertOperation : SimpleBulkInsertOperation
	{
		private const string TableName = "RemoteLogins";

		public RemoteLoginBulkInsertOperation(string server, string database) : base(server, database, TableName)
		{
		}

		public RemoteLoginBulkInsertOperation(string server, string database, int timeout) : base(server, database, TableName, timeout)
		{
		}

		public RemoteLoginBulkInsertOperation(string connectionString) : base(connectionString, TableName)
		{
		}

		public RemoteLoginBulkInsertOperation(string connectionString, int timeout) : base(connectionString, TableName, timeout)
		{
		}

		public RemoteLoginBulkInsertOperation(SqlConnectionStringBuilder connectionStringBuilder) : base(connectionStringBuilder, TableName)
		{
		}

		public RemoteLoginBulkInsertOperation(SqlConnectionStringBuilder connectionStringBuilder, int timeout) : base(connectionStringBuilder, TableName, timeout)
		{
		}

		protected override void PrepareSchema()
		{
			Schema["database_survey"] = typeof (int);
			Schema["server_id"] = typeof (int);
			Schema["remote_name"] = typeof (string);
			Schema["local_principal_id"] = typeof (int?);
			Schema["modify_date"] = typeof (DateTime);
		}
	}
}
