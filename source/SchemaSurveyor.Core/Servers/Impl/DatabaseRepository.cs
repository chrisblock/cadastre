using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SchemaSurveyor.Core.Servers.Impl
{
	public class DatabaseRepository : IDatabaseRepository
	{
		public IEnumerable<string> GetDatabases(string serverName)
		{
			var sqlConnectionStringBuilder = new SqlConnectionStringBuilder
			{
				DataSource = serverName,
				IntegratedSecurity = true
			};

			var result = new List<string>();

			using (var connection = new SqlConnection(sqlConnectionStringBuilder.ToString()))
			{
				connection.Open();

				using (var command = connection.CreateCommand("SELECT [name] FROM [sys].[databases] WHERE [name] NOT IN ('master', 'tempdb', 'model', 'msdb')"))
				{
					using (var reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							var database = String.Format("{0}", reader["name"]);

							// TODO: check database name for null???

							result.Add(database);
						}

						reader.Close();
					}
				}

				connection.Close();
			}

			return result;
		}
	}
}
