using System.Configuration;
using System.Data.SqlClient;

using Rhino.Etl.Core.Operations;

namespace SchemaSurveyor.Etl.Operations.BulkInsert
{
	public abstract class SimpleBulkInsertOperation : SqlBulkInsertOperation
	{
		private static ConnectionStringSettings BuildConnectionStringSettings(string server, string database)
		{
			var connectionStringBuilder = new SqlConnectionStringBuilder
				{
					DataSource = server,
					InitialCatalog = database,
					IntegratedSecurity = true
				};

			return BuildConnectionStringSettings(connectionStringBuilder);
		}

		private static ConnectionStringSettings BuildConnectionStringSettings(SqlConnectionStringBuilder connectionStringBuilder)
		{
			var settings = new ConnectionStringSettings("ConnectionStringName", connectionStringBuilder.ToString(), typeof (SqlConnection).AssemblyQualifiedName);

			return settings;
		}

		protected SimpleBulkInsertOperation(string server, string database, string tableName) : base(BuildConnectionStringSettings(server, database), tableName)
		{
		}

		protected SimpleBulkInsertOperation(string server, string database, string tableName, int timeout) : base(BuildConnectionStringSettings(server, database), tableName, timeout)
		{
		}

		protected SimpleBulkInsertOperation(string connectionString, string tableName) : this(new SqlConnectionStringBuilder(connectionString), tableName)
		{
		}

		protected SimpleBulkInsertOperation(string connectionString, string tableName, int timeout) : this(new SqlConnectionStringBuilder(connectionString), tableName, timeout)
		{
		}

		protected SimpleBulkInsertOperation(SqlConnectionStringBuilder connectionStringBuilder, string tableName) : base(BuildConnectionStringSettings(connectionStringBuilder), tableName)
		{
		}

		protected SimpleBulkInsertOperation(SqlConnectionStringBuilder connectionStringBuilder, string tableName, int timeout) : base(BuildConnectionStringSettings(connectionStringBuilder), tableName, timeout)
		{
		}
	}
}
