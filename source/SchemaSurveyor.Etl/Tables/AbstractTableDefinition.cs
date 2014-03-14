using System;
using System.Collections.Generic;

namespace SchemaSurveyor.Etl.Tables
{
	public abstract class AbstractTableDefinition : ITableDefinition
	{
		private readonly IDictionary<string, Type> _columnTypes = new Dictionary<string, Type>();

		public string Name { get; private set; }

		protected AbstractTableDefinition(string tableName)
		{
			Name = tableName;

			RegisterColumn("database_survey", typeof (int));
		}

		public abstract string GetSelectStatement();

		public void SetSchema(IDictionary<string, Type> schema)
		{
			foreach (var columnType in _columnTypes)
			{
				schema[columnType.Key] = columnType.Value;
			}
		}

		protected void RegisterColumn<T>(string columnName)
		{
			RegisterColumn(columnName, typeof (T));
		}

		protected void RegisterColumn(string columnName, Type columnType)
		{
			_columnTypes[columnName] = columnType;
		}
	}
}
