using System.Data;

namespace SchemaSurveyor.Core
{
	public static class DbConnectionExtensions
	{
		public static IDbCommand CreateCommand(this IDbConnection connection, string sql)
		{
			var command = connection.CreateCommand();

			command.CommandText = sql;

			return command;
		}
	}
}
