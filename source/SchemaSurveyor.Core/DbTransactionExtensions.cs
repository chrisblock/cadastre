using System.Data;

namespace SchemaSurveyor.Core
{
	public static class DbTransactionExtensions
	{
		public static IDbCommand CreateCommand(this IDbTransaction transaction, string sql)
		{
			var connection = transaction.Connection;

			var command = connection.CreateCommand(sql);

			return command;
		}
	}
}
