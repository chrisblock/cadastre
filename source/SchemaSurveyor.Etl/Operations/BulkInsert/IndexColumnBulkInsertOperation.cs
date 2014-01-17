using System.Data.SqlClient;

namespace SchemaSurveyor.Etl.Operations.BulkInsert
{
	public class IndexColumnBulkInsertOperation : SimpleBulkInsertOperation
	{
		private const string TableName = "IndexColumns";

		public IndexColumnBulkInsertOperation(string server, string database) : base(server, database, TableName)
		{
		}

		public IndexColumnBulkInsertOperation(string server, string database, int timeout) : base(server, database, TableName, timeout)
		{
		}

		public IndexColumnBulkInsertOperation(string connectionString) : base(connectionString, TableName)
		{
		}

		public IndexColumnBulkInsertOperation(string connectionString, int timeout) : base(connectionString, TableName, timeout)
		{
		}

		public IndexColumnBulkInsertOperation(SqlConnectionStringBuilder connectionStringBuilder) : base(connectionStringBuilder, TableName)
		{
		}

		public IndexColumnBulkInsertOperation(SqlConnectionStringBuilder connectionStringBuilder, int timeout) : base(connectionStringBuilder, TableName, timeout)
		{
		}

		protected override void PrepareSchema()
		{
			Schema["database_survey"] = typeof (int);
			Schema["object_id"] = typeof (int);
			Schema["index_id"] = typeof (int);
			Schema["index_column_id"] = typeof (int);
			Schema["column_id"] = typeof (int);
			Schema["key_ordinal"] = typeof (byte);
			Schema["partition_ordinal"] = typeof (byte);
			Schema["is_descending_key"] = typeof (bool?);
			Schema["is_included_column"] = typeof (bool?);
		}
	}
}
