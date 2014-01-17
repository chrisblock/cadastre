using System;
using System.Data.SqlClient;

namespace SchemaSurveyor.Etl.Operations.BulkInsert
{
	public class ForeignKeyBulkInsertOperation : SimpleBulkInsertOperation
	{
		private const string TableName = "ForeignKeys";

		public ForeignKeyBulkInsertOperation(string server, string database) : base(server, database, TableName)
		{
		}

		public ForeignKeyBulkInsertOperation(string server, string database, int timeout) : base(server, database, TableName, timeout)
		{
		}

		public ForeignKeyBulkInsertOperation(string connectionString) : base(connectionString, TableName)
		{
		}

		public ForeignKeyBulkInsertOperation(string connectionString, int timeout) : base(connectionString, TableName, timeout)
		{
		}

		public ForeignKeyBulkInsertOperation(SqlConnectionStringBuilder connectionStringBuilder) : base(connectionStringBuilder, TableName)
		{
		}

		public ForeignKeyBulkInsertOperation(SqlConnectionStringBuilder connectionStringBuilder, int timeout) : base(connectionStringBuilder, TableName, timeout)
		{
		}

		protected override void PrepareSchema()
		{
			Schema["database_survey"] = typeof (int);
			Schema["name"] = typeof (string);
			Schema["object_id"] = typeof (int);
			Schema["principal_id"] = typeof (int?);
			Schema["schema_id"] = typeof (int);
			Schema["parent_object_id"] = typeof (int);
			Schema["type"] = typeof (string);
			Schema["type_desc"] = typeof (string);
			Schema["create_date"] = typeof (DateTime);
			Schema["modify_date"] = typeof (DateTime);
			Schema["is_ms_shipped"] = typeof (bool);
			Schema["is_published"] = typeof (bool);
			Schema["is_schema_published"] = typeof (bool);
			Schema["referenced_object_id"] = typeof (int?);
			Schema["key_index_id"] = typeof (int?);
			Schema["is_disabled"] = typeof (bool);
			Schema["is_not_for_replication"] = typeof (bool);
			Schema["is_not_trusted"] = typeof (bool);
			Schema["delete_referential_action"] = typeof (byte?);
			Schema["delete_referential_action_desc"] = typeof (string);
			Schema["update_referential_action"] = typeof (byte?);
			Schema["update_referential_action_desc"] = typeof (string);
			Schema["is_system_named"] = typeof (bool);
		}
	}
}
