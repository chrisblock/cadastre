using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;

namespace SchemaSurveyor.Core.Servers.Impl
{
	public class DatabaseRepository : IDatabaseRepository
	{
		private static readonly Lazy<IEnumerable<string>> LazyServerList = new Lazy<IEnumerable<string>>(CreateServerList, LazyThreadSafetyMode.ExecutionAndPublication);

		private static IEnumerable<string> ServerList { get { return LazyServerList.Value; } }

		private static IEnumerable<string> CreateServerList()
		{
			var result = SqlDataSourceEnumerator.Instance.GetDataSources().Rows
				.Cast<DataRow>()
				.Select(BuildServerName)
				.OrderBy(x => x);

			return result;
		}

		private static string BuildServerName(DataRow x)
		{
			var serverName = String.Format("{0}", x["ServerName"]);
			var instanceName = String.Format("{0}", x["InstanceName"]);

			if (String.IsNullOrWhiteSpace(instanceName) == false)
			{
				serverName = String.Format("{0}\\{1}", serverName, instanceName);
			}

			return serverName;
		}

		public IEnumerable<string> GetServers()
		{
			return ServerList;
		}

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
					}
				}

				connection.Close();
			}

			return result;
		}
	}
}
