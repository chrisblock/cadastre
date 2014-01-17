using System;
using System.Data.SqlClient;

namespace SchemaSurveyor.Core
{
	public static class ConnectionTester
	{
		public static bool CanConnect(string server, string database)
		{
			var connectionStringBuilder = new SqlConnectionStringBuilder
			{
				DataSource = server,
				InitialCatalog = database,
				IntegratedSecurity = true
			};

			return CanConnect(connectionStringBuilder);
		}

		public static bool CanConnect(SqlConnectionStringBuilder connectionStringBuilder)
		{
			return CanConnect(connectionStringBuilder.ToString());
		}

		public static bool CanConnect(string connectionString)
		{
			var result = false;

			try
			{
				using (var connection = new SqlConnection(connectionString))
				{
					connection.Open();

					result = true;

					connection.Close();
				}
			}
			catch (Exception e)
			{
			}

			return result;
		}
	}
}
