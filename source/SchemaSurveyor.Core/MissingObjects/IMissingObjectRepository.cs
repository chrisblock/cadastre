using System.Linq;

namespace SchemaSurveyor.Core.MissingObjects
{
	public interface IMissingObjectRepository
	{
		IQueryable<MissingObject> GetMissingObjects(int surveyId, int databaseSurveyId, ObjectType type);
	}
}
