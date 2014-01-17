using System.Data.SqlClient;

namespace SchemaSurveyor.Etl.Operations.BulkInsert
{
	public class IndexBulkInsertOperation : SimpleBulkInsertOperation
	{
		private const string TableName = "Indexes";

		public IndexBulkInsertOperation(string server, string database) : base(server, database, TableName)
		{
		}

		public IndexBulkInsertOperation(string server, string database, int timeout) : base(server, database, TableName, timeout)
		{
		}

		public IndexBulkInsertOperation(string connectionString) : base(connectionString, TableName)
		{
		}

		public IndexBulkInsertOperation(string connectionString, int timeout) : base(connectionString, TableName, timeout)
		{
		}

		public IndexBulkInsertOperation(SqlConnectionStringBuilder connectionStringBuilder) : base(connectionStringBuilder, TableName)
		{
		}

		public IndexBulkInsertOperation(SqlConnectionStringBuilder connectionStringBuilder, int timeout) : base(connectionStringBuilder, TableName, timeout)
		{
		}

		protected override void PrepareSchema()
		{
			Schema["database_survey"] = typeof (int);
			Schema["object_id"] = typeof (int);
			Schema["name"] = typeof (string);
			Schema["index_id"] = typeof (int);
			Schema["type"] = typeof (byte);
			Schema["type_desc"] = typeof (string);
			Schema["is_unique"] = typeof (bool?);
			Schema["data_space_id"] = typeof (int);
			Schema["ignore_dup_key"] = typeof (bool?);
			Schema["is_primary_key"] = typeof (bool?);
			Schema["is_unique_constraint"] = typeof (bool?);
			Schema["fill_factor"] = typeof (byte);
			Schema["is_padded"] = typeof (bool?);
			Schema["is_disabled"] = typeof (bool?);
			Schema["is_hypothetical"] = typeof (bool?);
			Schema["allow_row_locks"] = typeof (bool?);
			Schema["allow_page_locks"] = typeof (bool?);
			// TODO: these are added in MSSQL2008
			//Schema["has_filter"] = typeof (bool?);
			//Schema["filter_definition"] = typeof (string);
		}
	}
}
