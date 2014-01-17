﻿using System;
using System.Data.SqlClient;

namespace SchemaSurveyor.Etl.Operations.BulkInsert
{
	public class ViewBulkInsertOperation : SimpleBulkInsertOperation
	{
		private const string TableName = "Views";

		public ViewBulkInsertOperation(string server, string database) : base(server, database, TableName)
		{
		}

		public ViewBulkInsertOperation(string server, string database, int timeout) : base(server, database, TableName, timeout)
		{
		}

		public ViewBulkInsertOperation(string connectionString) : base(connectionString, TableName)
		{
		}

		public ViewBulkInsertOperation(string connectionString, int timeout) : base(connectionString, TableName, timeout)
		{
		}

		public ViewBulkInsertOperation(SqlConnectionStringBuilder connectionStringBuilder) : base(connectionStringBuilder, TableName)
		{
		}

		public ViewBulkInsertOperation(SqlConnectionStringBuilder connectionStringBuilder, int timeout) : base(connectionStringBuilder, TableName, timeout)
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
			Schema["is_replicated"] = typeof (bool?);
			Schema["has_replication_filter"] = typeof (bool?);
			Schema["has_opaque_metadata"] = typeof (bool);
			Schema["has_unchecked_assembly_data"] = typeof (bool);
			Schema["with_check_option"] = typeof (bool);
			Schema["is_date_correlation_view"] = typeof (bool);
			// TODO: this columns is added by MSSQL2008
			//Schema["is_tracked_by_cdc"] = typeof (bool?);
		}
	}
}
