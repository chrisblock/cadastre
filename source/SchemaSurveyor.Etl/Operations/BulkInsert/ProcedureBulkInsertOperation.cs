using System;
using System.Data.SqlClient;

namespace SchemaSurveyor.Etl.Operations.BulkInsert
{
	public class ProcedureBulkInsertOperation : SimpleBulkInsertOperation
	{
		private const string TableName = "Procedures";

		public ProcedureBulkInsertOperation(string server, string database) : base(server, database, TableName)
		{
		}

		public ProcedureBulkInsertOperation(string server, string database, int timeout) : base(server, database, TableName, timeout)
		{
		}

		public ProcedureBulkInsertOperation(string connectionString) : base(connectionString, TableName)
		{
		}

		public ProcedureBulkInsertOperation(string connectionString, int timeout) : base(connectionString, TableName, timeout)
		{
		}

		public ProcedureBulkInsertOperation(SqlConnectionStringBuilder connectionStringBuilder) : base(connectionStringBuilder, TableName)
		{
		}

		public ProcedureBulkInsertOperation(SqlConnectionStringBuilder connectionStringBuilder, int timeout) : base(connectionStringBuilder, TableName, timeout)
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
			Schema["is_auto_executed"] = typeof (bool);
			Schema["is_execution_replicated"] = typeof (bool?);
			Schema["is_repl_serializable_only"] = typeof (bool?);
			Schema["skips_repl_constraints"] = typeof (bool?);
		}
	}
}
