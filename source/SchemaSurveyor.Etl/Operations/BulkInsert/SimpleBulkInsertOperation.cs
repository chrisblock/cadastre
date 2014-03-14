using System.Configuration;
using System.Data.SqlClient;

using Rhino.Etl.Core.Operations;

using SchemaSurveyor.Etl.Tables;

namespace SchemaSurveyor.Etl.Operations.BulkInsert
{
	public class SimpleBulkInsertOperation<T> : SqlBulkInsertOperation
		where T : ITableDefinition, new()
	{
		private static readonly T TableDefinition = new T();

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
			var settings = new ConnectionStringSettings("ConnectionStringName", connectionStringBuilder.ToString(), typeof(SqlConnection).AssemblyQualifiedName);

			return settings;
		}

		public SimpleBulkInsertOperation(string server, string database) : base(BuildConnectionStringSettings(server, database), TableDefinition.Name)
		{
		}

		public SimpleBulkInsertOperation(string server, string database, int timeout) : base(BuildConnectionStringSettings(server, database), TableDefinition.Name, timeout)
		{
		}

		public SimpleBulkInsertOperation(string connectionString) : this(new SqlConnectionStringBuilder(connectionString))
		{
		}

		public SimpleBulkInsertOperation(string connectionString, int timeout) : this(new SqlConnectionStringBuilder(connectionString), timeout)
		{
		}

		public SimpleBulkInsertOperation(SqlConnectionStringBuilder connectionStringBuilder) : base(BuildConnectionStringSettings(connectionStringBuilder), TableDefinition.Name)
		{
		}

		public SimpleBulkInsertOperation(SqlConnectionStringBuilder connectionStringBuilder, int timeout) : base(BuildConnectionStringSettings(connectionStringBuilder), TableDefinition.Name, timeout)
		{
		}

		protected override void PrepareSchema()
		{
			TableDefinition.SetSchema(Schema);
		}
	}
}
