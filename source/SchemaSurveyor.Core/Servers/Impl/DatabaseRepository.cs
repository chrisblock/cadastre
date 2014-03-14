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
		private static readonly Lazy<IEnumerable<Server>> LazyServerList = new Lazy<IEnumerable<Server>>(CreateServerList, LazyThreadSafetyMode.ExecutionAndPublication);

		private static IEnumerable<Server> ServerList { get { return LazyServerList.Value; } }

		private static IEnumerable<Server> CreateServerList()
		{
			var result = SqlDataSourceEnumerator.Instance.GetDataSources().Rows
				.Cast<DataRow>()
				.Select(CreateServer)
				.OrderBy(x => x.Name);

			return result;
		}

		private static Server CreateServer(DataRow x)
		{
			var serverName = String.Format("{0}", x["ServerName"]);
			var instanceName = String.Format("{0}", x["InstanceName"]);

			if (String.IsNullOrWhiteSpace(instanceName) == false)
			{
				serverName = String.Format("{0}\\{1}", serverName, instanceName);
			}

			var result = new Server
			{
				Name = serverName,
			};

			return result;
		}

		public IEnumerable<Server> GetServers()
		{
			return ServerList;
		}

		public IEnumerable<Database> GetDatabases(string serverName)
		{
			var sqlConnectionStringBuilder = new SqlConnectionStringBuilder
			{
				DataSource = serverName,
				IntegratedSecurity = true
			};

			var result = new List<Database>();

			using (var connection = new SqlConnection(sqlConnectionStringBuilder.ToString()))
			{
				connection.Open();

				using (var command = connection.CreateCommand())
				{
					command.CommandText = "SELECT [name] FROM [sys].[databases] WHERE [name] NOT IN ('master', 'tempdb', 'model', 'msdb')";

					using (var reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							var database = new Database
							{
								Name = String.Format("{0}", reader["name"])
							};

							result.Add(database);
						}
					}
				}

				connection.Close();
			}

			return result;
		}

		public IEnumerable<Database> GetDatabases(Server server)
		{
			return GetDatabases(server.Name);
		}
	}
}
