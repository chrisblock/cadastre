using System;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

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

					using (var command = connection.CreateCommand("SELECT @@VERSION"))
					{
						var version = command.ExecuteScalar() as string;

						ParseVersion(version);
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

		private static void ParseVersion(string version)
		{
			var regex = new Regex(@"Microsoft SQL Server (\d{4}) - (\d+\.\d+\.\d+\.\d+) \(([^\)]+)\)");

			var match = regex.Match(version);

			// TODO: parse strings like this:
			/*
			Microsoft SQL Server 2012 (SP1) - 11.0.3128.0 (X64) 
			Dec 28 2012 20:23:12 
			Copyright (c) Microsoft Corporation
			Developer Edition (64-bit) on Windows NT 6.1 <X64> (Build 7601: Service Pack 1) (Hypervisor)
			 */
		}
	}
}
