using System;
using System.Data.SqlClient;

namespace SchemaSurveyor.Etl.Operations.BulkInsert
{
	public class LinkedLoginBulkInsertOperation : SimpleBulkInsertOperation
	{
		private const string TableName = "LinkedLogins";

		public LinkedLoginBulkInsertOperation(string server, string database) : base(server, database, TableName)
		{
		}

		public LinkedLoginBulkInsertOperation(string server, string database, int timeout) : base(server, database, TableName, timeout)
		{
		}

		public LinkedLoginBulkInsertOperation(string connectionString) : base(connectionString, TableName)
		{
		}

		public LinkedLoginBulkInsertOperation(string connectionString, int timeout) : base(connectionString, TableName, timeout)
		{
		}

		public LinkedLoginBulkInsertOperation(SqlConnectionStringBuilder connectionStringBuilder) : base(connectionStringBuilder, TableName)
		{
		}

		public LinkedLoginBulkInsertOperation(SqlConnectionStringBuilder connectionStringBuilder, int timeout) : base(connectionStringBuilder, TableName, timeout)
		{
		}

		protected override void PrepareSchema()
		{
			Schema["database_survey"] = typeof (int);
			Schema["server_id"] = typeof (int);
			Schema["local_principal_id"] = typeof (int?);
			Schema["uses_self_credential"] = typeof (bool);
			Schema["remote_name"] = typeof (string);
			Schema["modify_date"] = typeof (DateTime);
		}
	}
}
