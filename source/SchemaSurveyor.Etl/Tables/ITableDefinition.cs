using System;
using System.Collections.Generic;

namespace SchemaSurveyor.Etl.Tables
{
	public interface ITableDefinition
	{
		string Name { get; }
		string GetSelectStatement();
		void SetSchema(IDictionary<string, Type> schema);
	}
}
