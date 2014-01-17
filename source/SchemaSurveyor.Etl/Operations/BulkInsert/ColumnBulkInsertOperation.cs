using System.Data.SqlClient;

namespace SchemaSurveyor.Etl.Operations.BulkInsert
{
	public class ColumnBulkInsertOperation : SimpleBulkInsertOperation
	{
		private const string TableName = "Columns";

		public ColumnBulkInsertOperation(string server, string database) : base(server, database, TableName)
		{
		}

		public ColumnBulkInsertOperation(string server, string database, int timeout) : base(server, database, TableName, timeout)
		{
		}

		public ColumnBulkInsertOperation(string connectionString) : base(connectionString, TableName)
		{
		}

		public ColumnBulkInsertOperation(string connectionString, int timeout) : base(connectionString, TableName, timeout)
		{
		}

		public ColumnBulkInsertOperation(SqlConnectionStringBuilder connectionStringBuilder) : base(connectionStringBuilder, TableName)
		{
		}

		public ColumnBulkInsertOperation(SqlConnectionStringBuilder connectionStringBuilder, int timeout) : base(connectionStringBuilder, TableName, timeout)
		{
		}

		protected override void PrepareSchema()
		{
			Schema["database_survey"] = typeof (int);
			Schema["object_id"] = typeof (int);
			Schema["name"] = typeof (string);
			Schema["column_id"] = typeof (int);
			Schema["system_type_id"] = typeof (byte);
			Schema["user_type_id"] = typeof (int);
			Schema["max_length"] = typeof (short);
			Schema["precision"] = typeof (byte);
			Schema["scale"] = typeof (byte);
			Schema["collation_name"] = typeof (string);
			Schema["is_nullable"] = typeof (bool?);
			Schema["is_ansi_padded"] = typeof (bool);
			Schema["is_rowguidcol"] = typeof (bool);
			Schema["is_identity"] = typeof (bool);
			Schema["is_computed"] = typeof (bool);
			Schema["is_filestream"] = typeof (bool);
			Schema["is_replicated"] = typeof (bool?);
			Schema["is_non_sql_subscribed"] = typeof (bool?);
			Schema["is_merge_published"] = typeof (bool?);
			Schema["is_dts_replicated"] = typeof (bool?);
			Schema["is_xml_document"] = typeof (bool);
			Schema["xml_collection_id"] = typeof (int);
			Schema["default_object_id"] = typeof (int);
			Schema["rule_object_id"] = typeof (int);
			// TODO: these columns appear in MSSQL2008
			//Schema["is_sparse"] = typeof (bool?);
			//Schema["is_column_set"] = typeof (bool?);
		}
	}
}
