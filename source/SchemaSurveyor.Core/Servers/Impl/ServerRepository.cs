using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchemaSurveyor.Core.Servers.Impl
{
	public class ServerRepository : IServerRepository
	{
		private readonly IEnumerable<IServerSource> _serverSources;

		public ServerRepository(IEnumerable<IServerSource> serverSources)
		{
			_serverSources = serverSources;
		}

		public IEnumerable<string> GetServers()
		{
			return _serverSources
				.SelectMany(x => x.Servers)
				.Distinct(StringComparer.OrdinalIgnoreCase);
		}
	}
}
