using System.Collections.Generic;
using System.Web.Http;

using SchemaSurveyor.Core.Servers;

namespace Cadastre.Controllers.Api
{
	public class ServersController : ApiController
	{
		private readonly IServerRepository _serverRepository;
		private readonly IDatabaseRepository _databaseRepository;

		public ServersController(IServerRepository serverRepository, IDatabaseRepository databaseRepository)
		{
			_serverRepository = serverRepository;
			_databaseRepository = databaseRepository;
		}

		[HttpGet]
		[Route("api/Servers", Name = "ServersApi")]
		public IEnumerable<string> Get()
		{
			var result = _serverRepository.GetServers();

			return result;
		}

		[HttpGet]
		[Route("api/Servers/{serverName}/Databases", Name = "DatabasesApi")]
		public IEnumerable<string> Get(string serverName)
		{
			var result = _databaseRepository.GetDatabases(serverName);

			return result;
		}
	}
}
