using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading;

namespace SchemaSurveyor.Etl.Operations.Input
{
	public class ColumnInputOperation : SimpleInputCommandOperation
	{
		private static readonly Lazy<string> LazySelectSql = new Lazy<string>(BuildSelectSql, LazyThreadSafetyMode.ExecutionAndPublication);
		private static string SelectSql { get { return LazySelectSql.Value; } }

		private static string BuildSelectSql()
		{
			var stringBuilder = new StringBuilder();

			stringBuilder.AppendLine("SELECT");
			stringBuilder.AppendLine("	  @@SERVERNAME AS [server]");
			stringBuilder.AppendLine("	, DB_NAME() as [database]");
			stringBuilder.AppendLine("	, [object_id]");
			stringBuilder.AppendLine("	, [name]");
			stringBuilder.AppendLine("	, [column_id]");
			stringBuilder.AppendLine("	, [system_type_id]");
			stringBuilder.AppendLine("	, [user_type_id]");
			stringBuilder.AppendLine("	, [max_length]");
			stringBuilder.AppendLine("	, [precision]");
			stringBuilder.AppendLine("	, [scale]");
			stringBuilder.AppendLine("	, [collation_name]");
			stringBuilder.AppendLine("	, [is_nullable]");
			stringBuilder.AppendLine("	, [is_ansi_padded]");
			stringBuilder.AppendLine("	, [is_rowguidcol]");
			stringBuilder.AppendLine("	, [is_identity]");
			stringBuilder.AppendLine("	, [is_computed]");
			stringBuilder.AppendLine("	, [is_filestream]");
			stringBuilder.AppendLine("	, [is_replicated]");
			stringBuilder.AppendLine("	, [is_non_sql_subscribed]");
			stringBuilder.AppendLine("	, [is_merge_published]");
			stringBuilder.AppendLine("	, [is_dts_replicated]");
			stringBuilder.AppendLine("	, [is_xml_document]");
			stringBuilder.AppendLine("	, [xml_collection_id]");
			stringBuilder.AppendLine("	, [default_object_id]");
			stringBuilder.AppendLine("	, [rule_object_id]");
			// TODO: these columns appear in MSSQL2008
			//stringBuilder.AppendLine("	, [is_sparse]");
			//stringBuilder.AppendLine("	, [is_column_set]");
			stringBuilder.AppendLine("FROM [sys].[columns]");

			return stringBuilder.ToString();
		}

		public ColumnInputOperation(string connectionString) : base(connectionString)
		{
		}

		public ColumnInputOperation(string server, string database) : base(server, database)
		{
		}

		public ColumnInputOperation(SqlConnectionStringBuilder connectionStringBuilder) : base(connectionStringBuilder)
		{
		}

		protected override void PrepareCommand(IDbCommand cmd)
		{
			cmd.CommandText = SelectSql;
		}
	}
}
