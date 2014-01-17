using System.Data.SqlClient;

namespace SchemaSurveyor.Etl.Operations.BulkInsert
{
	public class ParameterBulkInsertOperation : SimpleBulkInsertOperation
	{
		private const string TableName = "Parameters";

		public ParameterBulkInsertOperation(string server, string database) : base(server, database, TableName)
		{
		}

		public ParameterBulkInsertOperation(string server, string database, int timeout) : base(server, database, TableName, timeout)
		{
		}

		public ParameterBulkInsertOperation(string connectionString) : base(connectionString, TableName)
		{
		}

		public ParameterBulkInsertOperation(string connectionString, int timeout) : base(connectionString, TableName, timeout)
		{
		}

		public ParameterBulkInsertOperation(SqlConnectionStringBuilder connectionStringBuilder) : base(connectionStringBuilder, TableName)
		{
		}

		public ParameterBulkInsertOperation(SqlConnectionStringBuilder connectionStringBuilder, int timeout) : base(connectionStringBuilder, TableName, timeout)
		{
		}

		protected override void PrepareSchema()
		{
			Schema["database_survey"] = typeof (int);
			Schema["object_id"] = typeof (int);
			Schema["name"] = typeof (string);
			Schema["parameter_id"] = typeof (int);
			Schema["system_type_id"] = typeof (byte);
			Schema["user_type_id"] = typeof (int);
			Schema["max_length"] = typeof (short);
			Schema["precision"] = typeof (byte);
			Schema["scale"] = typeof (byte);
			Schema["is_output"] = typeof (bool);
			Schema["is_cursor_ref"] = typeof (bool);
			Schema["has_default_value"] = typeof (bool);
			Schema["is_xml_document"] = typeof (bool);
			// TODO: default_value is a sql_variant...i have no idea if this is gonna work
			Schema["default_value"] = typeof (object);
			Schema["xml_collection_id"] = typeof (int);
			// TODO: this columns is added in MSSQL2008
			//Schema["is_readonly"] = typeof (bool);
		}
	}
}
