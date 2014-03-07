using System.Data.SqlClient;

namespace SchemaSurveyor.Etl.Operations.BulkInsert
{
	public class SqlModuleDefinitionBulkInsertOperation : SimpleBulkInsertOperation
	{
		private const string TableName = "SqlModuleDefinitions";

		public SqlModuleDefinitionBulkInsertOperation(string server, string database) : base(server, database, TableName)
		{
		}

		public SqlModuleDefinitionBulkInsertOperation(string server, string database, int timeout) : base(server, database, TableName, timeout)
		{
		}

		public SqlModuleDefinitionBulkInsertOperation(string connectionString) : base(connectionString, TableName)
		{
		}

		public SqlModuleDefinitionBulkInsertOperation(string connectionString, int timeout) : base(connectionString, TableName, timeout)
		{
		}

		public SqlModuleDefinitionBulkInsertOperation(SqlConnectionStringBuilder connectionStringBuilder) : base(connectionStringBuilder, TableName)
		{
		}

		public SqlModuleDefinitionBulkInsertOperation(SqlConnectionStringBuilder connectionStringBuilder, int timeout) : base(connectionStringBuilder, TableName, timeout)
		{
		}

		protected override void PrepareSchema()
		{
			Schema["database_survey"] = typeof(int);
			Schema["object_id"] = typeof(int);
			Schema["definition"] = typeof(string);
		}
	}
}
