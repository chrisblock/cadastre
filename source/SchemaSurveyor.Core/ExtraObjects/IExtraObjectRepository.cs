using System.Linq;

namespace SchemaSurveyor.Core.ExtraObjects
{
	public interface IExtraObjectRepository
	{
		IQueryable<ExtraObject> GetExtraObjects(int surveyId, int databaseSurveyId, ObjectType type);
	}
}
