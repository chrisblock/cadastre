using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using Rhino.Etl.Core;
using Rhino.Etl.Core.Operations;

namespace SchemaSurveyor.Etl.Operations.Input
{
	public abstract class SimpleInputCommandOperation : InputCommandOperation
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

		protected SimpleInputCommandOperation(string connectionString) : this(new SqlConnectionStringBuilder(connectionString))
		{
		}

		protected SimpleInputCommandOperation(string server, string database) : base(BuildConnectionStringSettings(server, database))
		{
		}

		protected SimpleInputCommandOperation(SqlConnectionStringBuilder connectionStringBuilder) : base(BuildConnectionStringSettings(connectionStringBuilder))
		{
		}

		protected override Row CreateRowFromReader(IDataReader reader)
		{
			return Row.FromReader(reader);
		}
	}
}
