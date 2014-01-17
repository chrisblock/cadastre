using System;
using System.Data.SqlClient;

namespace SchemaSurveyor.Etl.Operations.BulkInsert
{
	public class FunctionBulkInsertOperation : SimpleBulkInsertOperation
	{
		private const string TableName = "Functions";

		public FunctionBulkInsertOperation(string server, string database) : base(server, database, TableName)
		{
		}

		public FunctionBulkInsertOperation(string server, string database, int timeout) : base(server, database, TableName, timeout)
		{
		}

		public FunctionBulkInsertOperation(string connectionString) : base(connectionString, TableName)
		{
		}

		public FunctionBulkInsertOperation(string connectionString, int timeout) : base(connectionString, TableName, timeout)
		{
		}

		public FunctionBulkInsertOperation(SqlConnectionStringBuilder connectionStringBuilder) : base(connectionStringBuilder, TableName)
		{
		}

		public FunctionBulkInsertOperation(SqlConnectionStringBuilder connectionStringBuilder, int timeout) : base(connectionStringBuilder, TableName, timeout)
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
		}
	}
}
