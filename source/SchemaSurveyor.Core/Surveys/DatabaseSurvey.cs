using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace SchemaSurveyor.Core.Surveys
{
	public class DatabaseSurvey
	{
		public int Id { get; set; }
		public int SurveyId { get; set; }
		public string Server { get; set; }
		public string Database { get; set; }
		public bool IsReferenceSchema { get; set; }
		public bool? HadConnectionError { get; set; }
		public bool? HadEtlError
		{
			get
			{
				bool? result = null;

				if (Errors != null)
				{
					result = Errors.Any();
				}

				return result;
			}
		}
		public long? Duration { get; set; }
		public IEnumerable<Exception> Errors { get; set; }

		public SqlConnectionStringBuilder GetConnectionStringBuilder()
		{
			var result = new SqlConnectionStringBuilder
			{
				DataSource = Server,
				InitialCatalog = Database,
				IntegratedSecurity = true
			};

			return result;
		}

		public string GetConnectionString()
		{
			var builder = GetConnectionStringBuilder();

			return builder.ToString();
		}
	}
}
