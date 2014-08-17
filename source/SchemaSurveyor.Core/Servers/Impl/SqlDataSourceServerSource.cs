using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Sql;
using System.Linq;
using System.Threading;

namespace SchemaSurveyor.Core.Servers.Impl
{
	public class SqlDataSourceServerSource : IServerSource
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

		public IEnumerable<string> Servers { get { return ServerList; } }
	}
}
