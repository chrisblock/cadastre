using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading;

namespace SchemaSurveyor.Etl.Operations.Input
{
	public class ParameterInputOperation : SimpleInputCommandOperation
	{
		private static readonly Lazy<string> LazySelectSql = new Lazy<string>(BuildSelectSql, LazyThreadSafetyMode.ExecutionAndPublication);
		private static string SelectSql { get { return LazySelectSql.Value; } }

		private static string BuildSelectSql()
		{
			var stringBuilder = new StringBuilder();

			stringBuilder.AppendLine("SELECT");
			stringBuilder.AppendLine("	  @@SERVERNAME AS [server]");
			stringBuilder.AppendLine("	, DB_NAME() AS [database]");
			stringBuilder.AppendLine("	, [object_id]");
			stringBuilder.AppendLine("	, [name]");
			stringBuilder.AppendLine("	, [parameter_id]");
			stringBuilder.AppendLine("	, [system_type_id]");
			stringBuilder.AppendLine("	, [user_type_id]");
			stringBuilder.AppendLine("	, [max_length]");
			stringBuilder.AppendLine("	, [precision]");
			stringBuilder.AppendLine("	, [scale]");
			stringBuilder.AppendLine("	, [is_output]");
			stringBuilder.AppendLine("	, [is_cursor_ref]");
			stringBuilder.AppendLine("	, [has_default_value]");
			stringBuilder.AppendLine("	, [is_xml_document]");
			stringBuilder.AppendLine("	, [default_value]");
			stringBuilder.AppendLine("	, [xml_collection_id]");
			// TODO: this column is added in MSSQL2008
			//stringBuilder.AppendLine("	, [is_readonly]");
			stringBuilder.AppendLine("FROM [sys].[parameters]");

			return stringBuilder.ToString();
		}

		public ParameterInputOperation(string connectionString) : base(connectionString)
		{
		}

		public ParameterInputOperation(string server, string database) : base(server, database)
		{
		}

		public ParameterInputOperation(SqlConnectionStringBuilder connectionStringBuilder) : base(connectionStringBuilder)
		{
		}

		protected override void PrepareCommand(IDbCommand cmd)
		{
			cmd.CommandText = SelectSql;
		}
	}
}
