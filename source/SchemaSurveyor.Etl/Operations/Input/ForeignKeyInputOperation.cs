using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading;

namespace SchemaSurveyor.Etl.Operations.Input
{
	public class ForeignKeyInputOperation : SimpleInputCommandOperation
	{
		private static readonly Lazy<string> LazySelectSql = new Lazy<string>(BuildSelectSql, LazyThreadSafetyMode.ExecutionAndPublication);
		private static string SelectSql { get { return LazySelectSql.Value; } }

		private static string BuildSelectSql()
		{
			var stringBuilder = new StringBuilder();

			stringBuilder.AppendLine("SELECT");
			stringBuilder.AppendLine("	  [name]");
			stringBuilder.AppendLine("	, [object_id]");
			stringBuilder.AppendLine("	, [principal_id]");
			stringBuilder.AppendLine("	, [schema_id]");
			stringBuilder.AppendLine("	, [parent_object_id]");
			stringBuilder.AppendLine("	, [type]");
			stringBuilder.AppendLine("	, [type_desc]");
			stringBuilder.AppendLine("	, [create_date]");
			stringBuilder.AppendLine("	, [modify_date]");
			stringBuilder.AppendLine("	, [is_ms_shipped]");
			stringBuilder.AppendLine("	, [is_published]");
			stringBuilder.AppendLine("	, [is_schema_published]");
			stringBuilder.AppendLine("	, [referenced_object_id]");
			stringBuilder.AppendLine("	, [key_index_id]");
			stringBuilder.AppendLine("	, [is_disabled]");
			stringBuilder.AppendLine("	, [is_not_for_replication]");
			stringBuilder.AppendLine("	, [is_not_trusted]");
			stringBuilder.AppendLine("	, [delete_referential_action]");
			stringBuilder.AppendLine("	, [delete_referential_action_desc]");
			stringBuilder.AppendLine("	, [update_referential_action]");
			stringBuilder.AppendLine("	, [update_referential_action_desc]");
			stringBuilder.AppendLine("	, [is_system_named]");
			stringBuilder.AppendLine("FROM [sys].[foreign_keys]");

			return stringBuilder.ToString();
		}

		public ForeignKeyInputOperation(string connectionString) : base(connectionString)
		{
		}

		public ForeignKeyInputOperation(string server, string database) : base(server, database)
		{
		}

		public ForeignKeyInputOperation(SqlConnectionStringBuilder connectionStringBuilder) : base(connectionStringBuilder)
		{
		}

		protected override void PrepareCommand(IDbCommand cmd)
		{
			cmd.CommandText = SelectSql;
		}
	}
}
