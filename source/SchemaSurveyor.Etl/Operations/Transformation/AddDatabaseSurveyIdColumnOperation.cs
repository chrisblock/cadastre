using System.Collections.Generic;

using Rhino.Etl.Core;
using Rhino.Etl.Core.Operations;

namespace SchemaSurveyor.Etl.Operations.Transformation
{
	public class AddDatabaseSurveyIdColumnOperation : AbstractOperation
	{
		private readonly int _databaseSurveyId;

		public AddDatabaseSurveyIdColumnOperation(int databaseSurveyId)
		{
			_databaseSurveyId = databaseSurveyId;
		}

		public override IEnumerable<Row> Execute(IEnumerable<Row> rows)
		{
			foreach (var row in rows)
			{
				row["database_survey"] = _databaseSurveyId;

				yield return row;
			}
		}
	}
}
