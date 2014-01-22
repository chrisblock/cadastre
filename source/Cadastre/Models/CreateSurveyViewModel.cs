using System.Collections.Generic;
using System.Web.Mvc;

namespace Cadastre.Models
{
	public class CreateSurveyViewModel
	{
		public string Server { get; set; }
		public string Database { get; set; }

		public IEnumerable<SelectListItem> AvailableServers { get; set; }
		public IEnumerable<SelectListItem> AvailableDatabases { get; set; }

		public CreateSurveyViewModel()
		{
			AvailableServers = new List<SelectListItem>();
			AvailableDatabases = new List<SelectListItem>();
		}
	}
}
