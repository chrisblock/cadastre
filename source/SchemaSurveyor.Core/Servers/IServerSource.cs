using System.Collections.Generic;

namespace SchemaSurveyor.Core.Servers
{
	public interface IServerSource
	{
		IEnumerable<string> Servers { get; }
	}
}
