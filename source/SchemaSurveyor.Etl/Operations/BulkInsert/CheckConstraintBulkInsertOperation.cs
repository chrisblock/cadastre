﻿using System;
using System.Data.SqlClient;

namespace SchemaSurveyor.Etl.Operations.BulkInsert
{
	public class CheckConstraintBulkInsertOperation : SimpleBulkInsertOperation
	{
		private const string TableName = "CheckConstraints";

		public CheckConstraintBulkInsertOperation(string server, string database) : base(server, database, TableName)
		{
		}

		public CheckConstraintBulkInsertOperation(string server, string database, int timeout) : base(server, database, TableName, timeout)
		{
		}

		public CheckConstraintBulkInsertOperation(string connectionString) : base(connectionString, TableName)
		{
		}

		public CheckConstraintBulkInsertOperation(string connectionString, int timeout) : base(connectionString, TableName, timeout)
		{
		}

		public CheckConstraintBulkInsertOperation(SqlConnectionStringBuilder connectionStringBuilder) : base(connectionStringBuilder, TableName)
		{
		}

		public CheckConstraintBulkInsertOperation(SqlConnectionStringBuilder connectionStringBuilder, int timeout) : base(connectionStringBuilder, TableName, timeout)
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
			Schema["is_disabled"] = typeof (bool);
			Schema["is_not_for_replication"] = typeof (bool);
			Schema["is_not_trusted"] = typeof (bool);
			Schema["parent_column_id"] = typeof (int);
			Schema["definition"] = typeof (string);
			Schema["uses_database_collation"] = typeof (bool?);
			Schema["is_system_named"] = typeof (bool);
		}
	}
}
