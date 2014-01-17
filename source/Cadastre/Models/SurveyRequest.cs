using System.Collections.Generic;

namespace Cadastre.Models
{
	public class SurveyRequest
	{
		public string Name { get; set; }

		public IEnumerable<DatabaseSurveyRequest> Databases { get; set; }
	}
}
