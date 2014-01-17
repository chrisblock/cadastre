using System;
using System.Configuration;
using System.Data.SqlClient;

namespace SchemaSurveyor.Core
{
	public static class ConnectionStrings
	{
		public static SqlConnectionStringBuilder GetNamedConnectionString(string connectionStringName)
		{
			if (String.IsNullOrWhiteSpace(connectionStringName))
			{
				throw new ArgumentException(String.Format("Connection string name '{0}' is invalid.", connectionStringName), "connectionStringName");
			}

			var connectionStringSettings = ConfigurationManager.ConnectionStrings[connectionStringName];

			if (connectionStringSettings == null)
			{
				throw new ArgumentException(String.Format("Connection string named '{0}' not found.", connectionStringName));
			}

			var connectionString = connectionStringSettings.ConnectionString;

			if (String.IsNullOrWhiteSpace(connectionString))
			{
				throw new ArgumentException(String.Format("Connection string named '{0}' ('{1}') is invalid.", connectionStringName, connectionString));
			}

			var result = new SqlConnectionStringBuilder(connectionString);

			return result;
		}
	}
}
