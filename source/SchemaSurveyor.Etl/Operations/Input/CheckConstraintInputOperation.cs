﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading;

namespace SchemaSurveyor.Etl.Operations.Input
{
	public class CheckConstraintInputOperation : SimpleInputCommandOperation
	{
		private static readonly Lazy<string> LazySelectSql = new Lazy<string>(BuildSelectSql, LazyThreadSafetyMode.ExecutionAndPublication);
		private static string SelectSql { get { return LazySelectSql.Value; } }

		private static string BuildSelectSql()
		{
			var stringBuilder = new StringBuilder();

			stringBuilder.AppendLine("SELECT");
			stringBuilder.AppendLine("	  @@SERVERNAME AS [server]");
			stringBuilder.AppendLine("	, DB_NAME() as [database]");
			stringBuilder.AppendLine("	, [name]");
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
			stringBuilder.AppendLine("	, [is_disabled]");
			stringBuilder.AppendLine("	, [is_not_for_replication]");
			stringBuilder.AppendLine("	, [is_not_trusted]");
			stringBuilder.AppendLine("	, [parent_column_id]");
			stringBuilder.AppendLine("	, [definition]");
			stringBuilder.AppendLine("	, [uses_database_collation]");
			stringBuilder.AppendLine("	, [is_system_named]");
			stringBuilder.AppendLine("FROM [sys].[check_constraints]");

			return stringBuilder.ToString();
		}

		public CheckConstraintInputOperation(string connectionString) : base(connectionString)
		{
		}

		public CheckConstraintInputOperation(string server, string database) : base(server, database)
		{
		}

		public CheckConstraintInputOperation(SqlConnectionStringBuilder connectionStringBuilder) : base(connectionStringBuilder)
		{
		}

		protected override void PrepareCommand(IDbCommand cmd)
		{
			cmd.CommandText = SelectSql;
		}
	}
}
