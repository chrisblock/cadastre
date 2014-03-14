﻿using System;
using System.Text;
using System.Threading;

namespace SchemaSurveyor.Etl.Tables.Impl
{
	public class KeyConstraintTableDefinition : AbstractTableDefinition
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
			stringBuilder.AppendLine("	, [unique_index_id]");
			stringBuilder.AppendLine("	, [is_system_named]");
			stringBuilder.AppendLine("FROM [sys].[key_constraints]");

			return stringBuilder.ToString();
		}

		public KeyConstraintTableDefinition() : base("KeyConstraints")
		{
			//RegisterColumn("database_survey", typeof (int));
			RegisterColumn("name", typeof (string));
			RegisterColumn("object_id", typeof (int));
			RegisterColumn("principal_id", typeof (int?));
			RegisterColumn("schema_id", typeof (int));
			RegisterColumn("parent_object_id", typeof (int));
			RegisterColumn("type", typeof (string));
			RegisterColumn("type_desc", typeof (string));
			RegisterColumn("create_date", typeof (DateTime));
			RegisterColumn("modify_date", typeof (DateTime));
			RegisterColumn("is_ms_shipped", typeof (bool));
			RegisterColumn("is_published", typeof (bool));
			RegisterColumn("is_schema_published", typeof (bool));
			RegisterColumn("unique_index_id", typeof (int?));
			RegisterColumn("is_system_named", typeof (bool));
		}

		public override string GetSelectStatement()
		{
			return SelectSql;
		}
	}
}
