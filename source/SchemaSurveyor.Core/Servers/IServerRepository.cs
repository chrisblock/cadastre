using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchemaSurveyor.Core.Servers
{
	public interface IServerRepository
	{
		IEnumerable<string> GetServers();
	}
}
