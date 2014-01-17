using System;
using System.Data.SqlClient;

namespace SchemaSurveyor.Etl.Operations.BulkInsert
{
	public class TriggerBulkInsertOperation : SimpleBulkInsertOperation
	{
		private const string TableName = "Triggers";

		public TriggerBulkInsertOperation(string server, string database) : base(server, database, TableName)
		{
		}

		public TriggerBulkInsertOperation(string server, string database, int timeout) : base(server, database, TableName, timeout)
		{
		}

		public TriggerBulkInsertOperation(string connectionString) : base(connectionString, TableName)
		{
		}

		public TriggerBulkInsertOperation(string connectionString, int timeout) : base(connectionString, TableName, timeout)
		{
		}

		public TriggerBulkInsertOperation(SqlConnectionStringBuilder connectionStringBuilder) : base(connectionStringBuilder, TableName)
		{
		}

		public TriggerBulkInsertOperation(SqlConnectionStringBuilder connectionStringBuilder, int timeout) : base(connectionStringBuilder, TableName, timeout)
		{
		}

		protected override void PrepareSchema()
		{
			Schema["database_survey"] = typeof (int);
			Schema["name"] = typeof (string);
			Schema["object_id"] = typeof (int);
			Schema["parent_class"] = typeof (byte);
			Schema["parent_class_desc"] = typeof (string);
			Schema["parent_id"] = typeof (int);
			Schema["type"] = typeof (string);
			Schema["type_desc"] = typeof (string);
			Schema["create_date"] = typeof (DateTime);
			Schema["modify_date"] = typeof (DateTime);
			Schema["is_ms_shipped"] = typeof (bool);
			Schema["is_disabled"] = typeof (bool);
			Schema["is_not_for_replication"] = typeof (bool);
			Schema["is_instead_of_trigger"] = typeof (bool);
		}
	}
}
