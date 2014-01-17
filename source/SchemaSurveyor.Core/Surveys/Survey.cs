using System;
using System.Collections.Generic;

namespace SchemaSurveyor.Core.Surveys
{
	public class Survey
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Machine { get; set; }

		public string User { get; set; }

		public DateTime Start { get; set; }

		public DateTime? End { get; set; }

		public IEnumerable<DatabaseSurvey> DatabaseSurveys { get; set; }
	}
}
