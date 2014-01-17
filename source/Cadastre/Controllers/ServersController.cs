using System.Web.Mvc;

using SchemaSurveyor.Core.Servers;

namespace Cadastre.Controllers
{
	public class ServersController : Controller
	{
		private readonly IDatabaseRepository _databaseRepository;

		public ServersController(IDatabaseRepository databaseRepository)
		{
			_databaseRepository = databaseRepository;
		}

		[HttpGet]
		public JsonResult Index()
		{
			var servers = _databaseRepository.GetServers();

			return Json(servers, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		public JsonResult Databases(string serverName)
		{
			var databases = _databaseRepository.GetDatabases(serverName);

			return Json(databases, JsonRequestBehavior.AllowGet);
		}
	}
}
