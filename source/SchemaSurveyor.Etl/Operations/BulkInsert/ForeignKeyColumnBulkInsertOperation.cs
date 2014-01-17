using System.Data.SqlClient;

namespace SchemaSurveyor.Etl.Operations.BulkInsert
{
	public class ForeignKeyColumnBulkInsertOperation : SimpleBulkInsertOperation
	{
		private const string TableName = "ForeignKeyColumns";

		public ForeignKeyColumnBulkInsertOperation(string server, string database) : base(server, database, TableName)
		{
		}

		public ForeignKeyColumnBulkInsertOperation(string server, string database, int timeout) : base(server, database, TableName, timeout)
		{
		}

		public ForeignKeyColumnBulkInsertOperation(string connectionString) : base(connectionString, TableName)
		{
		}

		public ForeignKeyColumnBulkInsertOperation(string connectionString, int timeout) : base(connectionString, TableName, timeout)
		{
		}

		public ForeignKeyColumnBulkInsertOperation(SqlConnectionStringBuilder connectionStringBuilder) : base(connectionStringBuilder, TableName)
		{
		}

		public ForeignKeyColumnBulkInsertOperation(SqlConnectionStringBuilder connectionStringBuilder, int timeout) : base(connectionStringBuilder, TableName, timeout)
		{
		}

		protected override void PrepareSchema()
		{
			Schema["database_survey"] = typeof (int);
			Schema["constraint_object_id"] = typeof (int);
			Schema["constraint_column_id"] = typeof (int);
			Schema["parent_object_id"] = typeof (int);
			Schema["parent_column_id"] = typeof (int);
			Schema["referenced_object_id"] = typeof (int);
			Schema["referenced_column_id"] = typeof (int);
		}
	}
}
