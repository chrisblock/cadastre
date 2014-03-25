using System.Linq;

namespace SchemaSurveyor.Core.MissingObjects
{
	public interface IMissingObjectRepository
	{
		MissingObjectCollection GetMissingObjects(int surveyId, int databaseSurveyId);

		IQueryable<MissingObject> GetMissingObjects(int surveyId, int databaseSurveyId, ObjectType type);
	}
}
