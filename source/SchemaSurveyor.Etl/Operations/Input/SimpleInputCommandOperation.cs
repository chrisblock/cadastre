using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using Rhino.Etl.Core;
using Rhino.Etl.Core.Operations;

using SchemaSurveyor.Etl.Tables;

namespace SchemaSurveyor.Etl.Operations.Input
{
	public class SimpleInputCommandOperation<T> : InputCommandOperation
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
			var settings = new ConnectionStringSettings("ConnectionStringName", connectionStringBuilder.ToString(), typeof (SqlConnection).AssemblyQualifiedName);

			return settings;
		}

		public SimpleInputCommandOperation(string connectionString) : this(new SqlConnectionStringBuilder(connectionString))
		{
		}

		public SimpleInputCommandOperation(string server, string database) : base(BuildConnectionStringSettings(server, database))
		{
		}

		public SimpleInputCommandOperation(SqlConnectionStringBuilder connectionStringBuilder) : base(BuildConnectionStringSettings(connectionStringBuilder))
		{
		}

		protected override Row CreateRowFromReader(IDataReader reader)
		{
			return Row.FromReader(reader);
		}

		protected override void PrepareCommand(IDbCommand cmd)
		{
			cmd.CommandText = TableDefinition.GetSelectStatement();
		}
	}
}
