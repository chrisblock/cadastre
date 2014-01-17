using System;
using System.Data.SqlClient;

namespace SchemaSurveyor.Etl.Operations.BulkInsert
{
	public class TableBulkInsertOperation : SimpleBulkInsertOperation
	{
		private const string TableName = "Tables";

		public TableBulkInsertOperation(string server, string database) : base(server, database, TableName)
		{
		}

		public TableBulkInsertOperation(string server, string database, int timeout) : base(server, database, TableName, timeout)
		{
		}

		public TableBulkInsertOperation(string connectionString) : base(connectionString, TableName)
		{
		}

		public TableBulkInsertOperation(string connectionString, int timeout) : base(connectionString, TableName, timeout)
		{
		}

		public TableBulkInsertOperation(SqlConnectionStringBuilder connectionStringBuilder) : base(connectionStringBuilder, TableName)
		{
		}

		public TableBulkInsertOperation(SqlConnectionStringBuilder connectionStringBuilder, int timeout) : base(connectionStringBuilder, TableName, timeout)
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
			Schema["lob_data_space_id"] = typeof (int?);
			Schema["filestream_data_space_id"] = typeof (int?);
			Schema["max_column_id_used"] = typeof (int);
			Schema["lock_on_bulk_load"] = typeof (bool);
			Schema["uses_ansi_nulls"] = typeof (bool?);
			Schema["is_replicated"] = typeof (bool?);
			Schema["has_replication_filter"] = typeof (bool?);
			Schema["is_merge_published"] = typeof (bool?);
			Schema["is_sync_tran_subscribed"] = typeof (bool?);
			Schema["has_unchecked_assembly_data"] = typeof (bool);
			Schema["text_in_row_limit"] = typeof (int?);
			Schema["large_value_types_out_of_row"] = typeof (bool?);
			// TODO: these columns are added in MSSQL2008
			//Schema["is_tracked_by_cdc"] = typeof (bool?);
			//Schema["lock_escalation"] = typeof (byte?);
			//Schema["lock_escalation_desc"] = typeof (string);
		}
	}
}
