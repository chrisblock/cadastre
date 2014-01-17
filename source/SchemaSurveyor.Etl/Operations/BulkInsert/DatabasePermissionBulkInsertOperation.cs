using System.Data.SqlClient;

namespace SchemaSurveyor.Etl.Operations.BulkInsert
{
	public class DatabasePermissionBulkInsertOperation : SimpleBulkInsertOperation
	{
		private const string TableName = "DatabasePermissions";

		public DatabasePermissionBulkInsertOperation(string server, string database) : base(server, database, TableName)
		{
		}

		public DatabasePermissionBulkInsertOperation(string server, string database, int timeout) : base(server, database, TableName, timeout)
		{
		}

		public DatabasePermissionBulkInsertOperation(string connectionString) : base(connectionString, TableName)
		{
		}

		public DatabasePermissionBulkInsertOperation(string connectionString, int timeout) : base(connectionString, TableName, timeout)
		{
		}

		public DatabasePermissionBulkInsertOperation(SqlConnectionStringBuilder connectionStringBuilder) : base(connectionStringBuilder, TableName)
		{
		}

		public DatabasePermissionBulkInsertOperation(SqlConnectionStringBuilder connectionStringBuilder, int timeout) : base(connectionStringBuilder, TableName, timeout)
		{
		}

		protected override void PrepareSchema()
		{
			Schema["database_survey"] = typeof (int);
			Schema["class"] = typeof (byte);
			Schema["class_desc"] = typeof (string);
			Schema["major_id"] = typeof (int);
			Schema["minor_id"] = typeof (int);
			Schema["grantee_principal_id"] = typeof (int);
			Schema["grantor_principal_id"] = typeof (int);
			Schema["type"] = typeof (string);
			Schema["permission_name"] = typeof (string);
			Schema["state"] = typeof (char);
			Schema["state_desc"] = typeof (string);
		}
	}
}
