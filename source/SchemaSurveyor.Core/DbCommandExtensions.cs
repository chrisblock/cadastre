﻿using System.Data;

namespace SchemaSurveyor.Core
{
	public static class DbCommandExtensions
	{
		public static IDbDataParameter AddParameter(this IDbCommand command, string parameterName, object parameterValue)
		{
			var parameter = command.CreateParameter();

			parameter.ParameterName = parameterName;
			parameter.Value = parameterValue;

			command.Parameters.Add(parameter);

			return parameter;
		}
	}
}
