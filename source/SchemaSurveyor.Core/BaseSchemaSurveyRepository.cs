using System.Data;
using System.Data.SqlClient;

namespace SchemaSurveyor.Core
{
	public abstract class BaseSchemaSurveyRepository
	{
		private const string SchemaSurveyConnectionStringName = "SchemaSurvey";

		protected SqlConnectionStringBuilder GetSchemaSurveyConnectionString()
		{
			var connectionString = ConnectionStrings.GetNamedConnectionString(SchemaSurveyConnectionStringName);

			return connectionString;
		}

		protected IDbConnection GetSchemaSurveyConnection()
		{
			var connectionString = GetSchemaSurveyConnectionString();

			var connection = new SqlConnection(connectionString.ToString());

			return connection;
		}
	}
}
