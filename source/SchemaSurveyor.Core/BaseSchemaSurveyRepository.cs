using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchemaSurveyor.Core
{
	public abstract class BaseSchemaSurveyRepository
	{
		private const string SchemaSurveyConnectionStringName = "SchemaSurvey";

		protected SqlConnection GetSchemaSurveyConnection()
		{
			var connectionString = ConnectionStrings.GetNamedConnectionString(SchemaSurveyConnectionStringName);

			var connection = new SqlConnection(connectionString.ToString());

			return connection;
		}
	}
}
