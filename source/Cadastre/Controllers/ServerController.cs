using System.Collections.Generic;
using System.Web.Http;

using SchemaSurveyor.Core.Servers;

namespace Cadastre.Controllers
{
	public class ServerController : ApiController
	{
		private readonly IDatabaseRepository _databaseRepository;

		public ServerController(IDatabaseRepository databaseRepository)
		{
			_databaseRepository = databaseRepository;
		}

		public IEnumerable<Server> Get()
		{
			var result = _databaseRepository.GetServers();

			return result;
		}

		public string Get(int id)
		{
			return "value";
		}
	}
}
