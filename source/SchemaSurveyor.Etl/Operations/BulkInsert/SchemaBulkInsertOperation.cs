using System.Data.SqlClient;

namespace SchemaSurveyor.Etl.Operations.BulkInsert
{
	public class SchemaBulkInsertOperation : SimpleBulkInsertOperation
	{
		private const string TableName = "Schemas";

		public SchemaBulkInsertOperation(string server, string database) : base(server, database, TableName)
		{
		}

		public SchemaBulkInsertOperation(string server, string database, int timeout) : base(server, database, TableName, timeout)
		{
		}

		public SchemaBulkInsertOperation(string connectionString) : base(connectionString, TableName)
		{
		}

		public SchemaBulkInsertOperation(string connectionString, int timeout) : base(connectionString, TableName, timeout)
		{
		}

		public SchemaBulkInsertOperation(SqlConnectionStringBuilder connectionStringBuilder) : base(connectionStringBuilder, TableName)
		{
		}

		public SchemaBulkInsertOperation(SqlConnectionStringBuilder connectionStringBuilder, int timeout) : base(connectionStringBuilder, TableName, timeout)
		{
		}

		protected override void PrepareSchema()
		{
			Schema["database_survey"] = typeof (int);
			Schema["name"] = typeof (string);
			Schema["schema_id"] = typeof (int);
			Schema["principal_id"] = typeof (int?);
		}
	}
}
