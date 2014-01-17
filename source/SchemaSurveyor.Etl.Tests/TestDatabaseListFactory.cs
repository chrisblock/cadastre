using System.Collections.Generic;

using SchemaSurveyor.Core.Surveys;
using SchemaSurveyor.Etl.Surveying;

namespace SchemaSurveyor.Etl.Tests
{
	public class TestDatabaseListFactory : IDatabaseListFactory
	{
		public IEnumerable<DatabaseSurvey> BuildDatabaseList()
		{
			var result = new List<DatabaseSurvey>(2)
			{
				new DatabaseSurvey
				{
					Server = "(local)",
					Database = "ReferenceDatabase",
					IsReferenceSchema = true
				},
				new DatabaseSurvey
				{
					Server = "(local)",
					Database = "OtherDatabase",
					IsReferenceSchema = false
				}
			};

			return result;
		}
	}
}
