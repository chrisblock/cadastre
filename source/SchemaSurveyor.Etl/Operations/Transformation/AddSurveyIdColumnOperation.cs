using System.Collections.Generic;

using Rhino.Etl.Core;
using Rhino.Etl.Core.Operations;

namespace SchemaSurveyor.Etl.Operations.Transformation
{
	public class AddSurveyIdColumnOperation : AbstractOperation
	{
		private readonly int _surveyId;

		public AddSurveyIdColumnOperation(int surveyId)
		{
			_surveyId = surveyId;
		}

		public override IEnumerable<Row> Execute(IEnumerable<Row> rows)
		{
			foreach (var row in rows)
			{
				row["survey"] = _surveyId;

				yield return row;
			}
		}
	}
}
