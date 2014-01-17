using System;
using System.Data.SqlClient;

namespace SchemaSurveyor.Etl.Operations.BulkInsert
{
	public class SqlLoginBulkInsertOperation : SimpleBulkInsertOperation
	{
		private const string TableName = "SqlLogins";

		public SqlLoginBulkInsertOperation(string server, string database) : base(server, database, TableName)
		{
		}

		public SqlLoginBulkInsertOperation(string server, string database, int timeout) : base(server, database, TableName, timeout)
		{
		}

		public SqlLoginBulkInsertOperation(string connectionString) : base(connectionString, TableName)
		{
		}

		public SqlLoginBulkInsertOperation(string connectionString, int timeout) : base(connectionString, TableName, timeout)
		{
		}

		public SqlLoginBulkInsertOperation(SqlConnectionStringBuilder connectionStringBuilder) : base(connectionStringBuilder, TableName)
		{
		}

		public SqlLoginBulkInsertOperation(SqlConnectionStringBuilder connectionStringBuilder, int timeout) : base(connectionStringBuilder, TableName, timeout)
		{
		}

		protected override void PrepareSchema()
		{
			Schema["database_survey"] = typeof (int);
			Schema["name"] = typeof (string);
			Schema["principal_id"] = typeof (int);
			Schema["sid"] = typeof (byte[]);
			Schema["type"] = typeof (char);
			Schema["type_desc"] = typeof (string);
			Schema["is_disabled"] = typeof (bool?);
			Schema["create_date"] = typeof (DateTime);
			Schema["modify_date"] = typeof (DateTime);
			Schema["default_database_name"] = typeof (string);
			Schema["default_language_name"] = typeof (string);
			Schema["credential_id"] = typeof (int?);
			Schema["is_policy_checked"] = typeof (bool?);
			Schema["is_expiration_checked"] = typeof (bool?);
			Schema["password_hash"] = typeof (byte[]);
		}
	}
}
