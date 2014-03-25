using System.Collections.Generic;
using System.Web.Http;

using SchemaSurveyor.Core.Servers;

namespace Cadastre.Controllers.Api
{
	public class ServersController : ApiController
	{
		private readonly IDatabaseRepository _databaseRepository;

		public ServersController(IDatabaseRepository databaseRepository)
		{
			_databaseRepository = databaseRepository;
		}

		public IEnumerable<string> Get()
		{
			var result = _databaseRepository.GetServers();

			return result;
		}

		public IEnumerable<string> Get(string serverName)
		{
			var result = _databaseRepository.GetDatabases(serverName);

			return result;
		}
	}
}
