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

					//using (var command = connection.CreateCommand("SELECT @@VERSION"))
					using (var command = connection.CreateCommand("SELECT SERVERPROPERTY('Edition') AS [Edition], SERVERPROPERTY('ProductVersion') AS [ProductVersion], SERVERPROPERTY('ProductLevel') AS [ProductLevel]"))
					{
						using (var reader = command.ExecuteReader())
						{
							if (reader.Read())
							{
								var edition = String.Format("{0}", reader["Edition"]);
								var version = String.Format("{0}", reader["ProductVersion"]);
								var level = String.Format("{0}", reader["ProductLevel"]);


							}

							reader.Close();
						}

						//MicrosoftSqlServerVersion.Parse(version);
					}

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
