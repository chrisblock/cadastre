using System.Data.SqlClient;

namespace SchemaSurveyor.Etl.Operations.BulkInsert
{
	public class DatabaseRoleMemberBulkInsertOperation : SimpleBulkInsertOperation
	{
		private const string TableName = "DatabaseRoleMembers";

		public DatabaseRoleMemberBulkInsertOperation(string server, string database) : base(server, database, TableName)
		{
		}

		public DatabaseRoleMemberBulkInsertOperation(string server, string database, int timeout) : base(server, database, TableName, timeout)
		{
		}

		public DatabaseRoleMemberBulkInsertOperation(string connectionString) : base(connectionString, TableName)
		{
		}

		public DatabaseRoleMemberBulkInsertOperation(string connectionString, int timeout) : base(connectionString, TableName, timeout)
		{
		}

		public DatabaseRoleMemberBulkInsertOperation(SqlConnectionStringBuilder connectionStringBuilder) : base(connectionStringBuilder, TableName)
		{
		}

		public DatabaseRoleMemberBulkInsertOperation(SqlConnectionStringBuilder connectionStringBuilder, int timeout) : base(connectionStringBuilder, TableName, timeout)
		{
		}

		protected override void PrepareSchema()
		{
			Schema["database_survey"] = typeof (int);
			Schema["role_principal_id"] = typeof (int);
			Schema["member_principal_id"] = typeof (int);
		}
	}
}
