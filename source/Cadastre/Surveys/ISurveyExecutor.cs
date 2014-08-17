using System.Collections.Generic;

using Cadastre.Models;

namespace Cadastre.Surveys
{
	public interface ISurveyExecutor
	{
		bool Execute(string surveyName, IEnumerable<DatabaseSurveyRequest> databases);
	}
}
