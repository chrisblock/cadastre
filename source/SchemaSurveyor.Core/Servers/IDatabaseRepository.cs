using System.Collections.Generic;

namespace SchemaSurveyor.Core.Servers
{
	public interface IDatabaseRepository
	{
		IEnumerable<Server> GetServers();
		IEnumerable<Database> GetDatabases(string serverName);
		IEnumerable<Database> GetDatabases(Server server);
	}
}
