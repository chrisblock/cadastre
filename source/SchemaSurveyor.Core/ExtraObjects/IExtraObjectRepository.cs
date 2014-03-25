using System.Linq;

namespace SchemaSurveyor.Core.ExtraObjects
{
	public interface IExtraObjectRepository
	{
		ExtraObjectCollection GetExtraObjects(int surveyId, int databaseSurveyId);

		IQueryable<ExtraObject> GetExtraObjects(int surveyId, int databaseSurveyId, ObjectType type);
	}
}
