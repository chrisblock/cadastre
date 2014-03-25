using System.Collections.Generic;

namespace SchemaSurveyor.Core.Servers
{
	public interface IDatabaseRepository
	{
		IEnumerable<string> GetServers();
		IEnumerable<string> GetDatabases(string serverName);
	}
}
