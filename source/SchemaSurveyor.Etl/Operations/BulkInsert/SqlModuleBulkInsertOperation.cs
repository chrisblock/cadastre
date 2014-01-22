using System.Data.SqlClient;

namespace SchemaSurveyor.Etl.Operations.BulkInsert
{
	public class SqlModuleBulkInsertOperation : SimpleBulkInsertOperation
	{
		private const string TableName = "SqlModules";

		public SqlModuleBulkInsertOperation(string server, string database) : base(server, database, TableName)
		{
		}

		public SqlModuleBulkInsertOperation(string server, string database, int timeout) : base(server, database, TableName, timeout)
		{
		}

		public SqlModuleBulkInsertOperation(string connectionString) : base(connectionString, TableName)
		{
		}

		public SqlModuleBulkInsertOperation(string connectionString, int timeout) : base(connectionString, TableName, timeout)
		{
		}

		public SqlModuleBulkInsertOperation(SqlConnectionStringBuilder connectionStringBuilder) : base(connectionStringBuilder, TableName)
		{
		}

		public SqlModuleBulkInsertOperation(SqlConnectionStringBuilder connectionStringBuilder, int timeout) : base(connectionStringBuilder, TableName, timeout)
		{
		}

		protected override void PrepareSchema()
		{
			Schema["database_survey"] = typeof (int);
			Schema["object_id"] = typeof (int);
			Schema["definition"] = typeof (string);
			Schema["uses_database_collation"] = typeof (bool?);
			Schema["uses_ansi_nulls"] = typeof (bool?);
			Schema["uses_quoted_identifier"] = typeof (bool?);
			Schema["is_schema_bound"] = typeof (bool?);
			Schema["uses_database_collation"] = typeof (bool?);
			Schema["is_recompiled"] = typeof (bool?);
			Schema["null_on_null_input"] = typeof (bool?);
			Schema["execute_as_principal_id"] = typeof (int?);
		}
	}
}
