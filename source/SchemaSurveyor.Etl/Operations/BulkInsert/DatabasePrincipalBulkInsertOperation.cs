using System;
using System.Data.SqlClient;

namespace SchemaSurveyor.Etl.Operations.BulkInsert
{
	public class DatabasePrincipalBulkInsertOperation : SimpleBulkInsertOperation
	{
		private const string TableName = "DatabasePrincipals";

		public DatabasePrincipalBulkInsertOperation(string server, string database) : base(server, database, TableName)
		{
		}

		public DatabasePrincipalBulkInsertOperation(string server, string database, int timeout) : base(server, database, TableName, timeout)
		{
		}

		public DatabasePrincipalBulkInsertOperation(string connectionString) : base(connectionString, TableName)
		{
		}

		public DatabasePrincipalBulkInsertOperation(string connectionString, int timeout) : base(connectionString, TableName, timeout)
		{
		}

		public DatabasePrincipalBulkInsertOperation(SqlConnectionStringBuilder connectionStringBuilder) : base(connectionStringBuilder, TableName)
		{
		}

		public DatabasePrincipalBulkInsertOperation(SqlConnectionStringBuilder connectionStringBuilder, int timeout) : base(connectionStringBuilder, TableName, timeout)
		{
		}

		protected override void PrepareSchema()
		{
			Schema["database_survey"] = typeof (int);
			Schema["name"] = typeof (string);
			Schema["principal_id"] = typeof (int);
			Schema["type"] = typeof (char);
			Schema["type_desc"] = typeof (string);
			Schema["default_schema_name"] = typeof (string);
			Schema["create_date"] = typeof (DateTime);
			Schema["modify_date"] = typeof (DateTime);
			Schema["owning_principal_id"] = typeof (int?);
			Schema["sid"] = typeof (byte[]);
			Schema["is_fixed_role"] = typeof (bool);
		}
	}
}
